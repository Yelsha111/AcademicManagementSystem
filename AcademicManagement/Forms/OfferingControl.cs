using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AcademicManagement.Config;
using AcademicManagement.Managers;
using AcademicManagement.Models;

namespace AcademicManagement.Forms
{
    public partial class OfferingControl : UserControl
    {
        private readonly AcademicManager _manager = new AcademicManager(FirebaseConfig.GetService());
        private List<AcademicOffering> _allOfferings = new List<AcademicOffering>();
        private List<Curriculum> _curriculum = new List<Curriculum>();
        private int _selectedRowIndex = -1;
        private bool _suppressLoad = false; // true while we're programmatically selecting a row

        public OfferingControl()
        {
            InitializeComponent();
            StyleGrid();

            cboStatus.SelectedIndex = -1; // start blank, not pre-selected

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;
            cboSubjectCode.SelectedIndexChanged += (s, e) => RefreshSemesterOptions();

            this.Load += async (s, e) => await RefreshAllAsync();
        }

        private void StyleGrid()
        {
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 230, 230);
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(150, 20, 20);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(150, 20, 20);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 36;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(198, 40, 40);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Measures every item's rendered text width (using GetItemText, which
        // respects DisplayMember for data-bound combos) and widens the DROPDOWN
        // LIST portion to fit the longest entry - the closed box itself keeps
        // its designer width. Prevents long entries (e.g. long Subject
        // descriptions) from getting clipped when the list opens.
        private void WidenDropDownToFitContents(ComboBox combo)
        {
            int maxWidth = combo.DropDownWidth;
            using (Graphics g = combo.CreateGraphics())
            {
                foreach (var item in combo.Items)
                {
                    string text = combo.GetItemText(item);
                    int width = (int)g.MeasureString(text, combo.Font).Width
                        + SystemInformation.VerticalScrollBarWidth + 10;
                    if (width > maxWidth) maxWidth = width;
                }
            }
            combo.DropDownWidth = maxWidth;
        }

        private async System.Threading.Tasks.Task RefreshAllAsync()
        {
            try
            {
                _curriculum = await _manager.GetCurriculumAsync();
                var subjects = await _manager.GetSubjectsAsync();

                var subjectCodesInCurriculum = _curriculum.Select(c => c.SubjectCode).Distinct().ToHashSet();
                var eligibleSubjects = subjects.Where(s => subjectCodesInCurriculum.Contains(s.SubjectCode));

                cboSubjectCode.DataSource = eligibleSubjects
                    .Select(s => new { SubjectCode = s.SubjectCode, Display = $"{s.SubjectCode} - {s.SubjectDescription}" })
                    .ToList();
                cboSubjectCode.SelectedIndex = -1;
                WidenDropDownToFitContents(cboSubjectCode);

                RefreshSemesterOptions();

                _allOfferings = await _manager.GetOfferingsAsync();

                _suppressLoad = true;
                grid.DataSource = null;
                grid.DataSource = _allOfferings;
                grid.ClearSelection();
                grid.CurrentCell = null;
                _suppressLoad = false;
                _selectedRowIndex = -1;

                ClearInputs();
            }
            catch (Exception ex)
            {
                lblStatusMsg.Text = "Could not load data: " + ex.Message;
            }
        }

        // Selects and scrolls the grid to the row matching the given OfferingID,
        // without re-populating the input fields (those stay cleared after Add/Update).
        private void SelectRowByCode(string offeringId)
        {
            if (string.IsNullOrWhiteSpace(offeringId)) return;

            int rowIndex = _allOfferings.FindIndex(o => o.OfferingID == offeringId);
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return;

            _suppressLoad = true;
            grid.ClearSelection();
            grid.Rows[rowIndex].Selected = true;
            grid.CurrentCell = grid.Rows[rowIndex].Cells[0];
            grid.FirstDisplayedScrollingRowIndex = rowIndex;
            _selectedRowIndex = rowIndex;
            _suppressLoad = false;
        }

        // Fills the read-only txtSemester box with the Semester the currently
        // selected Subject actually has in Curriculum. There's no dropdown here
        // anymore because the value isn't a real choice - it's derived straight
        // from the Curriculum record, so showing it as an auto-filled, read-only
        // field (like Offering ID) is more honest than a ComboBox the user can't
        // meaningfully change.
        private void RefreshSemesterOptions()
        {
            var selectedSubject = cboSubjectCode.SelectedValue as string;

            if (string.IsNullOrWhiteSpace(selectedSubject))
            {
                txtSemester.Text = "";
                return;
            }

            var matchedSemester = _curriculum
                .Where(c => c.SubjectCode == selectedSubject)
                .Select(c => c.Semester)
                .Distinct()
                .OrderBy(s => s)
                .FirstOrDefault();

            txtSemester.Text = matchedSemester ?? "";
        }

        private void ApplySearchFilter()
        {
            var keyword = txtSearch.Text.ToLower();
            var filtered = string.IsNullOrWhiteSpace(keyword)
                ? _allOfferings
                : _allOfferings.Where(o => o.OfferingID.ToLower().Contains(keyword)
                                         || o.SubjectCode.ToLower().Contains(keyword)
                                         || o.SchoolYear.ToLower().Contains(keyword)
                                         || o.Status.ToLower().Contains(keyword)).ToList();

            _suppressLoad = true;
            grid.DataSource = null;
            grid.DataSource = filtered;
            grid.ClearSelection();
            grid.CurrentCell = null;
            _suppressLoad = false;
            _selectedRowIndex = -1;
        }

        private void LoadSelectedIntoInputs()
        {
            if (grid.CurrentRow?.DataBoundItem is AcademicOffering selected)
            {
                txtOfferingID.Text = selected.OfferingID;
                cboSubjectCode.SelectedValue = selected.SubjectCode;
                txtSchoolYear.Text = selected.SchoolYear;
                txtSemester.Text = selected.Semester;
                cboStatus.SelectedItem = selected.Status;
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.RowIndex == _selectedRowIndex)
            {
                grid.ClearSelection();
                grid.CurrentCell = null;
                _selectedRowIndex = -1;
                ClearInputs();
            }
            else
            {
                _selectedRowIndex = e.RowIndex;
            }
        }

        private void ClearInputs()
        {
            txtOfferingID.Clear();
            cboSubjectCode.SelectedIndex = -1;
            txtSchoolYear.Text = "2025-2026";
            RefreshSemesterOptions();
            cboStatus.SelectedIndex = -1;
            lblStatusMsg.Text = "";
        }

        private async System.Threading.Tasks.Task AddAsync()
        {
            if (cboSubjectCode.SelectedValue == null || string.IsNullOrWhiteSpace(txtSchoolYear.Text)
                || string.IsNullOrWhiteSpace(txtSemester.Text))
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = "Subject and School Year are required.";
                return;
            }

            if (cboStatus.SelectedItem == null)
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = "Status is required.";
                return;
            }

            var (success, error, newId) = await _manager.AddOfferingAsync(
                cboSubjectCode.SelectedValue as string,
                txtSchoolYear.Text.Trim(),
                txtSemester.Text,
                cboStatus.SelectedItem as string);

            if (!success)
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = error;
                return;
            }

            lblStatusMsg.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatusMsg.Text = $"Added successfully as {newId}.";
            ClearInputs();
            await RefreshAllAsync();
            SelectRowByCode(newId);
        }

        private async System.Threading.Tasks.Task UpdateAsync()
        {
            if (string.IsNullOrWhiteSpace(txtOfferingID.Text))
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = "Select a row from the table first before updating.";
                return;
            }

            var updatedId = txtOfferingID.Text.Trim();

            var offering = new AcademicOffering
            {
                OfferingID = updatedId,
                SubjectCode = cboSubjectCode.SelectedValue as string,
                SchoolYear = txtSchoolYear.Text.Trim(),
                Semester = txtSemester.Text,
                Status = cboStatus.SelectedItem as string
            };

            var (success, error) = await _manager.UpdateOfferingAsync(offering);
            if (!success)
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = error;
                return;
            }

            lblStatusMsg.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatusMsg.Text = "Updated successfully.";
            ClearInputs();
            await RefreshAllAsync();
            SelectRowByCode(updatedId);
        }

        private async System.Threading.Tasks.Task DeleteAsync()
        {
            if (string.IsNullOrWhiteSpace(txtOfferingID.Text))
            {
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = "Select a row from the table first before deleting.";
                return;
            }

            var confirm = MessageBox.Show($"Delete Academic Offering '{txtOfferingID.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteOfferingAsync(txtOfferingID.Text.Trim());
            ClearInputs();
            await RefreshAllAsync();
        }

        private void panelTopCard_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
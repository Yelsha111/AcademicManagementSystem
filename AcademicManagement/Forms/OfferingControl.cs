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
            lblFilterStatus.BringToFront();
            cboFilterStatus.BringToFront();
            lblFilterStatus.Left = cboFilterStatus.Left - lblFilterStatus.PreferredWidth - 8;
            StyleGrid();

            cboStatus.SelectedIndex = -1; // start blank, not pre-selected

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cboFilterStatus.SelectedIndexChanged += (s, e) => ApplyFilters();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;
            cboSubjectCode.SelectedIndexChanged += (s, e) => RefreshSemesterOptions();
            txtSchoolYear.KeyPress += TxtSchoolYear_KeyPress;

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

            var screenLocation = combo.PointToScreen(Point.Empty);
            var workingArea = Screen.FromControl(combo).WorkingArea;
            int maxAvailable = workingArea.Right - screenLocation.X - 10;

            combo.DropDownWidth = Math.Min(maxWidth, Math.Max(maxAvailable, combo.Width));
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

                // cboFilterStatus (list filter) - ONLY Status values that
                // actually appear among the current Offerings right now
                // (distinct Status from _allOfferings), not the full fixed
                // Open/Closed/Cancelled list. Grows automatically as soon as
                // the first Offering with a given Status is added.
                var filterOptions = _allOfferings
                    .Select(o => o.Status)
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(s => s)
                    .Select(s => new { Status = s, Display = s })
                    .ToList();
                filterOptions.Insert(0, new { Status = (string)null, Display = "All Status" });

                cboFilterStatus.DataSource = null;
                cboFilterStatus.DisplayMember = "Display";
                cboFilterStatus.ValueMember = "Status";
                cboFilterStatus.DataSource = filterOptions;
                cboFilterStatus.SelectedIndex = 0;
                WidenDropDownToFitContents(cboFilterStatus);

                ApplyFilters();
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

        private void TxtSchoolYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isDash = e.KeyChar == '-';
            bool isControl = char.IsControl(e.KeyChar);

            if (!isDigit && !isDash && !isControl)
            {
                e.Handled = true;
                lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatusMsg.Text = "School Year can only contain numbers and a dash (e.g. 2025-2026).";
            }
        }

        // Combines the text Search box with the Status filter dropdown above
        // the grid - both can be used at the same time.
        private void ApplyFilters()
        {
            var keyword = txtSearch.Text.ToLower();
            var filtered = string.IsNullOrWhiteSpace(keyword)
        ? _allOfferings
        : _allOfferings.Where(o => o.OfferingID.ToLower().Contains(keyword)
                                 || o.SubjectCode.ToLower().Contains(keyword)
                                 || o.SchoolYear.ToLower().Contains(keyword)
                                 || o.Status.ToLower().Contains(keyword)
                                 || (o.Semester ?? "").ToLower().Contains(keyword)).ToList();   // ⬅️ bagong linya

            var filterStatus = cboFilterStatus.SelectedValue as string;
            if (!string.IsNullOrEmpty(filterStatus))
                filtered = filtered.Where(o => string.Equals(o.Status, filterStatus, StringComparison.OrdinalIgnoreCase)).ToList();

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

            var isDuplicate = await _manager.IsDuplicateOfferingAsync(
                cboSubjectCode.SelectedValue as string,
                txtSchoolYear.Text.Trim(),
                txtSemester.Text);

            if (isDuplicate)
            {
                var confirm = MessageBox.Show(
                    "An Academic Offering with this exact Subject, School Year, and Semester already exists. Add it anyway?",
                    "Possible Duplicate",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;
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

            var isDuplicate = await _manager.IsDuplicateOfferingAsync(
                cboSubjectCode.SelectedValue as string,
                txtSchoolYear.Text.Trim(),
                txtSemester.Text,
                excludeId: updatedId);

            if (isDuplicate)
            {
                var confirm = MessageBox.Show(
                    "An Academic Offering with this exact Subject, School Year, and Semester already exists. Update anyway?",
                    "Possible Duplicate",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;
            }

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

        private void lblListTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
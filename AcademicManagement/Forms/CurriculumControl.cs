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
    public partial class CurriculumControl : UserControl
    {
        private readonly AcademicManager _manager = new AcademicManager(FirebaseConfig.GetService());
        private List<Curriculum> _allCurriculum = new List<Curriculum>();
        private int _selectedRowIndex = -1;
        private bool _suppressLoad = false; // true while we're programmatically selecting a row

        public CurriculumControl()
        {
            InitializeComponent();
            StyleGrid();

            cboSemester.SelectedIndex = -1; // start blank, not pre-selected

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;

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

        // Measures every item text in a bound ComboBox and widens ONLY the
        // open dropdown list (not the closed box itself) so long entries like
        // "PRG010 - BS Business Administration - Marketing Management" are
        // never cut off, even for boxes that can't be widened further because
        // another control sits right next to them on the same row.
        private static void WidenDropDownToFitContents(ComboBox combo)
        {
            int widest = combo.Width;
            using (var g = combo.CreateGraphics())
            {
                foreach (var item in combo.Items)
                {
                    string text = item?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(combo.DisplayMember) && item != null)
                    {
                        var prop = item.GetType().GetProperty(combo.DisplayMember);
                        if (prop != null) text = prop.GetValue(item)?.ToString() ?? text;
                    }

                    int textWidth = (int)g.MeasureString(text, combo.Font).Width + 30; // padding for scrollbar/margins
                    if (textWidth > widest) widest = textWidth;
                }
            }
            combo.DropDownWidth = widest;
        }

        private async System.Threading.Tasks.Task RefreshAllAsync()
        {
            try
            {
                var programs = await _manager.GetProgramsAsync();
                cboProgramId.DataSource = programs
                    .Select(p => new { ProgramId = p.ProgramId, Display = $"{p.ProgramId} - {p.ProgramName}" })
                    .ToList();
                cboProgramId.SelectedIndex = -1;
                WidenDropDownToFitContents(cboProgramId);

                var subjects = await _manager.GetSubjectsAsync();
                cboSubjectCode.DataSource = subjects
                    .Select(s => new { SubjectCode = s.SubjectCode, Display = $"{s.SubjectCode} - {s.SubjectDescription}" })
                    .ToList();
                cboSubjectCode.SelectedIndex = -1;
                WidenDropDownToFitContents(cboSubjectCode);

                _allCurriculum = await _manager.GetCurriculumAsync();

                _suppressLoad = true;
                grid.DataSource = null;
                grid.DataSource = _allCurriculum;
                grid.ClearSelection();
                grid.CurrentCell = null;
                _suppressLoad = false;
                _selectedRowIndex = -1;

                ClearInputs();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load data: " + ex.Message;
            }
        }

        // Selects and scrolls the grid to the row matching the given CurriculumID,
        // without re-populating the input fields (those stay cleared after Add/Update).
        private void SelectRowByCode(string curriculumId)
        {
            if (string.IsNullOrWhiteSpace(curriculumId)) return;

            int rowIndex = _allCurriculum.FindIndex(c => c.CurriculumID == curriculumId);
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return;

            _suppressLoad = true;
            grid.ClearSelection();
            grid.Rows[rowIndex].Selected = true;
            grid.CurrentCell = grid.Rows[rowIndex].Cells[0];
            grid.FirstDisplayedScrollingRowIndex = rowIndex;
            _selectedRowIndex = rowIndex;
            _suppressLoad = false;
        }

        private void ApplySearchFilter()
        {
            var keyword = txtSearch.Text.ToLower();
            var filtered = string.IsNullOrWhiteSpace(keyword)
                ? _allCurriculum
                : _allCurriculum.Where(c => c.CurriculumID.ToLower().Contains(keyword)
                                          || c.ProgramId.ToLower().Contains(keyword)
                                          || c.SubjectCode.ToLower().Contains(keyword)).ToList();

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
            if (grid.CurrentRow?.DataBoundItem is Curriculum selected)
            {
                txtCurriculumID.Text = selected.CurriculumID;
                cboProgramId.SelectedValue = selected.ProgramId;
                cboSubjectCode.SelectedValue = selected.SubjectCode;
                numYearLevel.Value = selected.YearLevel;
                cboSemester.SelectedItem = selected.Semester;
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
            txtCurriculumID.Clear();
            cboProgramId.SelectedIndex = -1;
            cboSubjectCode.SelectedIndex = -1;
            numYearLevel.Value = 1;
            cboSemester.SelectedIndex = -1;
            lblStatus.Text = "";
        }

        private async System.Threading.Tasks.Task AddAsync()
        {
            if (cboProgramId.SelectedValue == null || cboSubjectCode.SelectedValue == null)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Program and Subject are required.";
                return;
            }

            if (cboSemester.SelectedItem == null)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Semester is required.";
                return;
            }

            var (success, error, newId) = await _manager.AddCurriculumAsync(
                cboProgramId.SelectedValue as string,
                cboSubjectCode.SelectedValue as string,
                (int)numYearLevel.Value,
                cboSemester.SelectedItem as string);

            if (!success)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = error;
                return;
            }

            lblStatus.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatus.Text = $"Added successfully as {newId}.";
            ClearInputs();
            await RefreshAllAsync();
            SelectRowByCode(newId);
        }

        private async System.Threading.Tasks.Task UpdateAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCurriculumID.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before updating.";
                return;
            }

            var updatedId = txtCurriculumID.Text.Trim();

            var curriculum = new Curriculum
            {
                CurriculumID = updatedId,
                ProgramId = cboProgramId.SelectedValue as string,
                SubjectCode = cboSubjectCode.SelectedValue as string,
                YearLevel = (int)numYearLevel.Value,
                Semester = cboSemester.SelectedItem as string
            };

            var (success, error) = await _manager.UpdateCurriculumAsync(curriculum);
            if (!success)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = error;
                return;
            }

            lblStatus.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatus.Text = "Updated successfully.";
            ClearInputs();
            await RefreshAllAsync();
            SelectRowByCode(updatedId);
        }

        private async System.Threading.Tasks.Task DeleteAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCurriculumID.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before deleting.";
                return;
            }

            var confirm = MessageBox.Show($"Delete Curriculum entry '{txtCurriculumID.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteCurriculumAsync(txtCurriculumID.Text.Trim());
            ClearInputs();
            await RefreshAllAsync();
        }
    }
}
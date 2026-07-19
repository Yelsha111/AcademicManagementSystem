using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AcademicManagement.Config;
using AcademicManagement.Managers;
using AcademicManagement.Models;

namespace AcademicManagement.Forms
{
    public partial class CollegeControl : UserControl
    {
        private readonly AcademicManager _manager = new AcademicManager(FirebaseConfig.GetService());
        private List<College> _allColleges = new List<College>();
        private int _selectedRowIndex = -1;
        private bool _suppressLoad = false; // true while we're programmatically selecting a row

        public CollegeControl()
        {
            InitializeComponent();
            StyleGrid();

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;
            txtCollegeName.KeyPress += RestrictToLettersSpaceDash;

            this.Load += async (s, e) => await RefreshGridAsync();
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

        // Live-blocks any keystroke that isn't a letter, space, or dash - used for
        // Name/Description-style fields. Backspace, delete, arrow keys, etc. still
        // work normally since those are control characters, not printable ones.
        private void RestrictToLettersSpaceDash(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetter(e.KeyChar) || e.KeyChar == ' ' || e.KeyChar == '-') return;

            e.Handled = true; // swallow the keystroke - it never reaches the textbox
            lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatus.Text = "Only letters, spaces, and dashes (-) are allowed here.";
        }

        private async System.Threading.Tasks.Task RefreshGridAsync()
        {
            try
            {
                _allColleges = await _manager.GetCollegesAsync();

                _suppressLoad = true;
                grid.DataSource = null;
                grid.DataSource = _allColleges;
                grid.ClearSelection();
                grid.CurrentCell = null;
                _suppressLoad = false;
                _selectedRowIndex = -1;

                ClearInputs();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load Colleges: " + ex.Message;
            }
        }



        // Selects and scrolls the grid to the row matching the given CollegeId,
        // without re-populating the input fields (those stay cleared after Add/Update).
        private void SelectRowByCode(string collegeId)
        {
            if (string.IsNullOrWhiteSpace(collegeId)) return;

            int rowIndex = _allColleges.FindIndex(c => c.CollegeId == collegeId);
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
            var filtered = _manager.SearchColleges(_allColleges, txtSearch.Text);

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
            if (grid.CurrentRow?.DataBoundItem is College selected)
            {
                txtCollegeId.Text = selected.CollegeId;
                txtCollegeName.Text = selected.CollegeName;
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
            txtCollegeId.Clear();
            txtCollegeName.Clear();
            lblStatus.Text = "";
        }

        private async System.Threading.Tasks.Task AddAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCollegeName.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "CollegeName is required.";
                return;
            }

            var (success, error, newId) = await _manager.AddCollegeAsync(txtCollegeName.Text.Trim());
            if (!success)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = error;
                return;
            }

            lblStatus.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatus.Text = $"Added successfully as {newId}.";
            ClearInputs();
            await RefreshGridAsync();
            SelectRowByCode(newId);
        }

        private async System.Threading.Tasks.Task UpdateAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCollegeId.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before updating.";
                return;
            }

            var updatedId = txtCollegeId.Text.Trim();

            var college = new College { CollegeId = updatedId, CollegeName = txtCollegeName.Text.Trim() };
            var (success, error) = await _manager.UpdateCollegeAsync(college);
            if (!success)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = error;
                return;
            }

            lblStatus.ForeColor = Color.FromArgb(30, 130, 76);
            lblStatus.Text = "Updated successfully.";
            ClearInputs();
            await RefreshGridAsync();
            SelectRowByCode(updatedId);
        }

        private async System.Threading.Tasks.Task DeleteAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCollegeId.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before deleting.";
                return;
            }

            var collegeId = txtCollegeId.Text.Trim();
            var (programCount, curriculumCount) = await _manager.GetCollegeDependencyCountsAsync(collegeId);

            string message = programCount > 0
                ? $"This College has {programCount} Program(s) and {curriculumCount} Curriculum entry/entries linked to it.\n\n" +
                  $"Deleting it will also delete all of these.\n\n" +
                  $"Subjects and Academic Offerings will NOT be affected.\n\n" +
                  $"Continue?"
                : $"Delete College '{collegeId}'?";

            var confirm = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteCollegeCascadeAsync(collegeId);
            ClearInputs();
            await RefreshGridAsync();
        }
    }
}
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
    public partial class ProgramControl : UserControl
    {
        private readonly AcademicManager _manager = new AcademicManager(FirebaseConfig.GetService());
        private List<Models.Program> _allPrograms = new List<Models.Program>();
        private List<College> _colleges = new List<College>();
        private int _selectedRowIndex = -1;
        private bool _suppressLoad = false; // true while we're programmatically selecting a row

        public ProgramControl()
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
        // "BAFM — BS Business Administration - Financial Management" are never
        // cut off. Safe to call any time after the DataSource is set.
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
                _colleges = await _manager.GetCollegesAsync();
                cboCollegeId.DataSource = _colleges
                    .Select(c => new { CollegeId = c.CollegeId, Display = $"{c.CollegeId} - {c.CollegeName}" })
                    .ToList();
                cboCollegeId.SelectedIndex = -1;

                // The closed box can only show ~630px of text, but some College
                // names are much longer (e.g. "BS Business Administration -
                // Marketing Management"). Widen just the OPEN dropdown list so
                // nothing gets cut off there, without resizing the closed control.
                WidenDropDownToFitContents(cboCollegeId);

                _allPrograms = await _manager.GetProgramsAsync();

                _suppressLoad = true;
                grid.DataSource = null;
                grid.DataSource = _allPrograms;

                if (grid.Columns["Abbreviation"] != null)
                    grid.Columns["Abbreviation"].Visible = false;

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


        // Selects and scrolls the grid to the row matching the given ProgramId,
        // without re-populating the input fields (those stay cleared after Add/Update).
        private void SelectRowByCode(string programId)
        {
            if (string.IsNullOrWhiteSpace(programId)) return;

            int rowIndex = _allPrograms.FindIndex(p => p.ProgramId == programId);
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
            var filtered = _manager.SearchPrograms(_allPrograms, txtSearch.Text);

            _suppressLoad = true;
            grid.DataSource = null;
            grid.DataSource = filtered;

            if (grid.Columns["Abbreviation"] != null)
                grid.Columns["Abbreviation"].Visible = false;

            grid.ClearSelection();
            grid.CurrentCell = null;
            _suppressLoad = false;
            _selectedRowIndex = -1;
        }

        private void LoadSelectedIntoInputs()
        {
            if (grid.CurrentRow?.DataBoundItem is Models.Program selected)
            {
                txtProgramId.Text = selected.ProgramId;
                txtProgramName.Text = selected.ProgramName;
                cboCollegeId.SelectedValue = selected.CollegeId;
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
            txtProgramId.Clear();
            txtProgramName.Clear();
            cboCollegeId.SelectedIndex = -1;
            lblStatus.Text = "";
        }

        private async System.Threading.Tasks.Task AddAsync()
        {
            if (string.IsNullOrWhiteSpace(txtProgramName.Text) || cboCollegeId.SelectedValue == null)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "ProgramName and College are required.";
                return;
            }

            var (success, error, newId) = await _manager.AddProgramAsync(txtProgramName.Text.Trim(), cboCollegeId.SelectedValue as string);
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
            if (string.IsNullOrWhiteSpace(txtProgramId.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before updating.";
                return;
            }

            var updatedId = txtProgramId.Text.Trim();

            var program = new Models.Program
            {
                ProgramId = updatedId,
                ProgramName = txtProgramName.Text.Trim(),
                CollegeId = cboCollegeId.SelectedValue as string
            };

            var (success, error) = await _manager.UpdateProgramAsync(program);
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
            if (string.IsNullOrWhiteSpace(txtProgramId.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before deleting.";
                return;
            }

            var confirm = MessageBox.Show($"Delete Program '{txtProgramId.Text}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteProgramAsync(txtProgramId.Text.Trim());
            ClearInputs();
            await RefreshAllAsync();
        }
    }
}
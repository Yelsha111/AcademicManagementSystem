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
            lblFilterCollege.BringToFront();
            cboFilterCollege.BringToFront();
            lblFilterCollege.Left = cboFilterCollege.Left - lblFilterCollege.PreferredWidth - 8;
            StyleGrid();

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cboFilterCollege.SelectedIndexChanged += (s, e) => ApplyFilters();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;
            txtProgramName.KeyPress += RestrictToLettersSpaceDash;

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

        // Live-blocks any keystroke that isn't a letter, space, or dash - used for
        // Name/Description-style fields. Backspace, delete, arrow keys, etc. still
        // work normally since those are control characters, not printable ones.
        private void RestrictToLettersSpaceDash(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetter(e.KeyChar) || e.KeyChar == ' ' || e.KeyChar == '-') return;

            e.Handled = true;
            lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatus.Text = "Only letters, spaces, and dashes (-) are allowed here.";
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

                    int textWidth = (int)g.MeasureString(text, combo.Font).Width + 30;
                    if (textWidth > widest) widest = textWidth;
                }
            }

            // Cap the width so the dropdown never spills past the right edge of the
            // screen - it's a top-level popup, not confined by the form, so without
            // this cap a wide list can hang off the edge of the monitor and get
            // visually cut off there instead of showing the full text.
            var screenLocation = combo.PointToScreen(Point.Empty);
            var workingArea = Screen.FromControl(combo).WorkingArea;
            int maxAvailable = workingArea.Right - screenLocation.X - 10;

            combo.DropDownWidth = Math.Min(widest, Math.Max(maxAvailable, combo.Width));
        }

        private async System.Threading.Tasks.Task RefreshAllAsync()
        {
            try
            {
                _colleges = await _manager.GetCollegesAsync();

                // cboCollegeId (Add/Edit form) still shows ALL Colleges - you should
                // be able to assign any College when creating a new Program.
                cboCollegeId.DataSource = _colleges
                    .Select(c => new { CollegeId = c.CollegeId, Display = $"{c.CollegeId} - {c.CollegeName}" })
                    .ToList();
                cboCollegeId.SelectedIndex = -1;
                WidenDropDownToFitContents(cboCollegeId);

                _allPrograms = await _manager.GetProgramsAsync();

                // cboFilterCollege (list filter) - ONLY Colleges that actually have at
                // least one Program right now (distinct CollegeIds from _allPrograms),
                // not the full master College list. Grows automatically as new
                // Programs get added under a College that had none before.
                var collegeLookup = _colleges.ToDictionary(c => c.CollegeId, c => c.CollegeName);

                var filterOptions = _allPrograms
                    .Select(p => p.CollegeId)
                    .Distinct()
                    .Select(cid => new
                    {
                        CollegeId = cid,
                        Display = collegeLookup.TryGetValue(cid, out var name) ? $"{cid} - {name}" : cid
                    })
                    .OrderBy(x => x.CollegeId)
                    .ToList();
                filterOptions.Insert(0, new { CollegeId = (string)null, Display = "All Colleges" });

                cboFilterCollege.DataSource = filterOptions;
                cboFilterCollege.DisplayMember = "Display";
                cboFilterCollege.ValueMember = "CollegeId";
                cboFilterCollege.SelectedIndex = 0;
                WidenDropDownToFitContents(cboFilterCollege);

                ApplyFilters();
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

        // Combines the text Search box with the College filter dropdown above
        // the grid - both can be used at the same time.
        private void ApplyFilters()
        {
            var filtered = _manager.SearchPrograms(_allPrograms, txtSearch.Text);

            var filterCollegeId = cboFilterCollege.SelectedValue as string;
            if (!string.IsNullOrEmpty(filterCollegeId))
                filtered = filtered.Where(p => p.CollegeId == filterCollegeId).ToList();

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

            var programId = txtProgramId.Text.Trim();
            var curriculumCount = await _manager.GetProgramDependencyCountAsync(programId);

            string message = curriculumCount > 0
                ? $"This Program has {curriculumCount} Curriculum entry/entries linked to it.\n\n" +
                  $"Deleting it will also delete all of these.\n\n" +
                  $"The related College, Subjects, and Academic Offerings will NOT be affected.\n\n" +
                  $"Continue?"
                : $"Delete Program '{programId}'?";

            var confirm = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteProgramCascadeAsync(programId);
            ClearInputs();
            await RefreshAllAsync();
        }
    }
}
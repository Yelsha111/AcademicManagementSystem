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
    public partial class SubjectControl : UserControl
    {
        private readonly AcademicManager _manager = new AcademicManager(FirebaseConfig.GetService());
        private Dictionary<string, string> _categoryDisplayLookup;
        private List<Subject> _allSubjects = new List<Subject>();
        private int _selectedRowIndex = -1;
        private bool _suppressLoad = false; // true while we're programmatically selecting a row

        public SubjectControl()
        {
            InitializeComponent();
            lblFilterCategory.BringToFront();
            cboFilterCategory.BringToFront();
            lblFilterCategory.Left = cboFilterCategory.Left - lblFilterCategory.PreferredWidth - 8;
            StyleGrid();

            btnAdd.Click += async (s, e) => await AddAsync();
            btnUpdate.Click += async (s, e) => await UpdateAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClear.Click += (s, e) => ClearInputs();
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cboFilterCategory.SelectedIndexChanged += (s, e) => ApplyFilters();
            grid.SelectionChanged += (s, e) => { if (!_suppressLoad) LoadSelectedIntoInputs(); };
            grid.CellClick += Grid_CellClick;
            txtDescription.KeyPress += RestrictToLettersSpaceDash;

            this.Load += async (s, e) => { await LoadCategoriesAsync(); await RefreshAllAsync(); };
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

        // Live-blocks any keystroke that isn't a letter, digit, space, or dash - used
        // for Name/Description-style fields. Backspace, delete, arrow keys, etc. still
        // work normally since those are control characters, not printable ones.
        private void RestrictToLettersSpaceDash(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == ' ' || e.KeyChar == '-') return;

            e.Handled = true;
            lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatus.Text = "Only letters, numbers, spaces, and dashes (-) are allowed here.";
        }

        // Measures every item text in a bound ComboBox and widens ONLY the
        // open dropdown list (not the closed box itself) so long entries like
        // "BAFM — BS Business Administration - Financial Management" are never
        // cut off, even if the box is later resized narrower than an entry.
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

            // Cap the width so the dropdown never spills past the right edge of the
            // screen - it's a top-level popup, not confined by the form, so without
            // this cap a wide list can hang off the edge of the monitor and get
            // visually cut off there instead of showing the full text.
            var screenLocation = combo.PointToScreen(Point.Empty);
            var workingArea = Screen.FromControl(combo).WorkingArea;
            int maxAvailable = workingArea.Right - screenLocation.X - 10;

            combo.DropDownWidth = Math.Min(widest, Math.Max(maxAvailable, combo.Width));
        }
        // Extracts the leading letters of a SubjectCode - e.g. "IT101" -> "IT",
        // "GE005" -> "GE". Returns null if the code doesn't start with a letter.
        private static string ExtractPrefix(string subjectCode)
        {
            if (string.IsNullOrEmpty(subjectCode)) return null;

            int i = 0;
            while (i < subjectCode.Length && char.IsLetter(subjectCode[i])) i++;
            return i > 0 ? subjectCode.Substring(0, i) : null;
        }

        // Builds the Program-derived category list, used ONLY for cboCategory (the
        // Add/Edit form) - you should be able to pick any known category prefix
        // (IT, CS, GE, etc.) when creating a brand new Subject, even one that has
        // no Subjects under it yet.
        private async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            try
            {
                var programs = await _manager.GetProgramsAsync();

                var withPrefix = programs
                    .Select(p => new
                    {
                        Prefix = string.IsNullOrWhiteSpace(p.Abbreviation)
                            ? ProgramAbbreviations.GetAbbreviation(p.ProgramName)
                            : p.Abbreviation,
                        p.ProgramName
                    })
                    .Where(x => !string.IsNullOrWhiteSpace(x.Prefix));

                var categories = withPrefix
                    .GroupBy(x => x.Prefix, StringComparer.OrdinalIgnoreCase)
                    .Select(g => new
                    {
                        Prefix = g.Key,
                        Display = $"{g.Key} — {string.Join(" / ", g.Select(x => x.ProgramName).Distinct())}"
                    })
                    .OrderBy(c => c.Prefix)
                    .ToList();

                if (!categories.Any(c => string.Equals(c.Prefix, "GE", StringComparison.OrdinalIgnoreCase)))
                    categories.Insert(0, new { Prefix = "GE", Display = "GE — General Education" });

                // Keep a lookup for nicer display text ("IT — BS Information Technology")
                // when we build the filter dropdown later in RefreshAllAsync.
                _categoryDisplayLookup = categories.ToDictionary(c => c.Prefix, c => c.Display, StringComparer.OrdinalIgnoreCase);

                cboCategory.DataSource = null;
                cboCategory.DisplayMember = "Display";
                cboCategory.ValueMember = "Prefix";
                cboCategory.DataSource = categories;
                cboCategory.SelectedIndex = -1; // start blank, not pre-selected
                WidenDropDownToFitContents(cboCategory);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load Categories: " + ex.Message;
            }
        }

        private async System.Threading.Tasks.Task RefreshAllAsync()
        {
            try
            {
                _allSubjects = await _manager.GetSubjectsAsync();

                // cboFilterCategory (list filter) - ONLY prefixes that actually appear
                // in _allSubjects right now (distinct prefix extracted from each real
                // SubjectCode), not the full Program-derived category list. Grows
                // automatically as soon as the first Subject under a new prefix is added.
                var filterOptions = _allSubjects
                    .Select(s => ExtractPrefix(s.SubjectCode))
                    .Where(p => !string.IsNullOrEmpty(p))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Select(prefix => new
                    {
                        Prefix = prefix,
                        Display = _categoryDisplayLookup != null && _categoryDisplayLookup.TryGetValue(prefix, out var d) ? d : prefix
                    })
                    .OrderBy(x => x.Prefix)
                    .ToList();
                filterOptions.Insert(0, new { Prefix = (string)null, Display = "All Categories" });

                cboFilterCategory.DataSource = null;
                cboFilterCategory.DisplayMember = "Display";
                cboFilterCategory.ValueMember = "Prefix";
                cboFilterCategory.DataSource = filterOptions;
                cboFilterCategory.SelectedIndex = 0;
                WidenDropDownToFitContents(cboFilterCategory);

                ApplyFilters();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load Subjects: " + ex.Message;
            }
        }
        // Selects and scrolls the grid to the row matching the given SubjectCode,
        // without re-populating the input fields (those stay cleared after Add/Update).
        private void SelectRowByCode(string subjectCode)
        {
            if (string.IsNullOrWhiteSpace(subjectCode)) return;

            int rowIndex = _allSubjects.FindIndex(s => s.SubjectCode == subjectCode);
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return;

            _suppressLoad = true;
            grid.ClearSelection();
            grid.Rows[rowIndex].Selected = true;
            grid.CurrentCell = grid.Rows[rowIndex].Cells[0];
            grid.FirstDisplayedScrollingRowIndex = rowIndex;
            _selectedRowIndex = rowIndex;
            _suppressLoad = false;
        }

        // Combines the text Search box with the Category/Prefix filter dropdown
        // above the grid - both can be used at the same time. Picking "IT" shows
        // every SubjectCode that STARTS WITH "IT" (IT101, IT102, IT201, ...), not
        // just one exact code.
        private void ApplyFilters()
        {
            var filtered = _manager.SearchSubjects(_allSubjects, txtSearch.Text);

            var filterPrefix = cboFilterCategory.SelectedValue as string;
            if (!string.IsNullOrEmpty(filterPrefix))
                filtered = filtered
                    .Where(s => s.SubjectCode != null &&
                                s.SubjectCode.StartsWith(filterPrefix, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            _suppressLoad = true;
            grid.DataSource = null;
            grid.DataSource = filtered;

            if (grid.Columns.Contains("PrerequisitesDisplay"))
                grid.Columns["PrerequisitesDisplay"].HeaderText = "Prerequisites";

            grid.ClearSelection();
            grid.CurrentCell = null;
            _suppressLoad = false;
            _selectedRowIndex = -1;
        }

        private List<string> GetPrerequisites()
        {
            return txtPrerequisites.Text
                .Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();
        }

        private void LoadSelectedIntoInputs()
        {
            if (grid.CurrentRow?.DataBoundItem is Subject selected)
            {
                txtSubjectCode.Text = selected.SubjectCode;
                txtDescription.Text = selected.SubjectDescription;
                numUnits.Value = selected.Units;
                txtPrerequisites.Text = selected.Prerequisites != null
                    ? string.Join(", ", selected.Prerequisites)
                    : "";
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
            txtSubjectCode.Clear();
            cboCategory.SelectedIndex = -1;
            txtDescription.Clear();
            numUnits.Value = 3;
            txtPrerequisites.Clear();
            lblStatus.Text = "";
        }

        private async System.Threading.Tasks.Task AddAsync()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Description is required.";
                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Category is required.";
                return;
            }

            var prefix = cboCategory.SelectedValue as string;
            var (success, error, newId) = await _manager.AddSubjectAsync(
                prefix, txtDescription.Text.Trim(), (int)numUnits.Value, GetPrerequisites());

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
            if (string.IsNullOrWhiteSpace(txtSubjectCode.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before updating.";
                return;
            }

            var updatedCode = txtSubjectCode.Text.Trim();

            var subject = new Subject
            {
                SubjectCode = updatedCode,
                SubjectDescription = txtDescription.Text.Trim(),
                Units = (int)numUnits.Value,
                Prerequisites = GetPrerequisites()
            };

            var (success, error) = await _manager.UpdateSubjectAsync(subject);
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
            SelectRowByCode(updatedCode);
        }

        private async System.Threading.Tasks.Task DeleteAsync()
        {
            if (string.IsNullOrWhiteSpace(txtSubjectCode.Text))
            {
                lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
                lblStatus.Text = "Select a row from the table first before deleting.";
                return;
            }

            var subjectCode = txtSubjectCode.Text.Trim();
            var (curriculumCount, offeringCount) = await _manager.GetSubjectDependencyCountsAsync(subjectCode);

            string message = (curriculumCount > 0 || offeringCount > 0)
                ? $"This Subject has {curriculumCount} Curriculum entry/entries and {offeringCount} Academic Offering(s) linked to it.\n\n" +
                  $"Deleting it will also delete all of these.\n\n" +
                  $"The related Colleges and Programs will NOT be affected.\n\n" +
                  $"Continue?"
                : $"Delete Subject '{subjectCode}'?";

            var confirm = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            await _manager.DeleteSubjectCascadeAsync(subjectCode);
            ClearInputs();
            await RefreshAllAsync();
        }
    }
}
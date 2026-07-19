using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class SubjectControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            panelListCard = new Panel();
            panelGridContainer = new Panel();
            grid = new DataGridView();
            lblListTitle = new Label();
            lblFilterCategory = new Label();
            cboFilterCategory = new BorderedComboBox();
            panelTopCard = new Panel();
            lblStatus = new Label();
            txtSearch = new TextBox();
            lblSearchTitle = new Label();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtPrerequisites = new TextBox();
            lblPrerequisites = new Label();
            numUnits = new NumericUpDown();
            lblUnits = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            cboCategory = new BorderedComboBox();
            lblCategory = new Label();
            txtSubjectCode = new TextBox();
            lblSubjectCode = new Label();
            lblCardTitle = new Label();
            panelMain.SuspendLayout();
            panelListCard.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panelTopCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUnits).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(244, 244, 244);
            panelMain.Controls.Add(panelListCard);
            panelMain.Controls.Add(panelTopCard);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(1140, 900);
            panelMain.TabIndex = 1;
            // 
            // panelListCard
            // 
            panelListCard.BackColor = Color.White;
            panelListCard.BorderStyle = BorderStyle.FixedSingle;
            panelListCard.Controls.Add(panelGridContainer);
            panelListCard.Controls.Add(lblListTitle);
            panelListCard.Controls.Add(lblFilterCategory);
            panelListCard.Controls.Add(cboFilterCategory);
            panelListCard.Dock = DockStyle.Fill;
            panelListCard.Location = new Point(20, 285);
            panelListCard.Margin = new Padding(0, 20, 0, 0);
            panelListCard.Name = "panelListCard";
            panelListCard.Padding = new Padding(0, 20, 0, 0);
            panelListCard.Size = new Size(1100, 595);
            panelListCard.TabIndex = 1;
            // 
            // panelGridContainer
            // 
            panelGridContainer.Controls.Add(grid);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 60);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(20, 0, 20, 0);
            panelGridContainer.Size = new Size(1098, 533);
            panelGridContainer.TabIndex = 1;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersHeight = 29;
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(235, 235, 235);
            grid.Location = new Point(20, 0);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowHeadersWidth = 51;
            grid.RowTemplate.Height = 34;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(1058, 533);
            grid.TabIndex = 0;
            // 
            // lblListTitle
            // 
            lblListTitle.Dock = DockStyle.Top;
            lblListTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblListTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblListTitle.Location = new Point(0, 20);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Padding = new Padding(20, 0, 0, 0);
            lblListTitle.Size = new Size(1098, 40);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "▧  Course/Subject List";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFilterCategory
            // 
            lblFilterCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFilterCategory.AutoSize = true;
            lblFilterCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFilterCategory.ForeColor = Color.FromArgb(150, 20, 20);
            lblFilterCategory.Location = new Point(723, 32);
            lblFilterCategory.Name = "lblFilterCategory";
            lblFilterCategory.Size = new Size(150, 21);
            lblFilterCategory.TabIndex = 2;
            lblFilterCategory.Text = "Filter by Category:";
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboFilterCategory.BackColor = Color.FromArgb(250, 250, 250);
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Font = new Font("Segoe UI", 9.5F);
            cboFilterCategory.Location = new Point(883, 26);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(195, 29);
            cboFilterCategory.TabIndex = 3;
            // 
            // panelTopCard
            // 
            panelTopCard.BackColor = Color.White;
            panelTopCard.BorderStyle = BorderStyle.FixedSingle;
            panelTopCard.Controls.Add(lblStatus);
            panelTopCard.Controls.Add(txtSearch);
            panelTopCard.Controls.Add(lblSearchTitle);
            panelTopCard.Controls.Add(btnClear);
            panelTopCard.Controls.Add(btnDelete);
            panelTopCard.Controls.Add(btnUpdate);
            panelTopCard.Controls.Add(btnAdd);
            panelTopCard.Controls.Add(txtPrerequisites);
            panelTopCard.Controls.Add(lblPrerequisites);
            panelTopCard.Controls.Add(numUnits);
            panelTopCard.Controls.Add(lblUnits);
            panelTopCard.Controls.Add(txtDescription);
            panelTopCard.Controls.Add(lblDescription);
            panelTopCard.Controls.Add(cboCategory);
            panelTopCard.Controls.Add(lblCategory);
            panelTopCard.Controls.Add(txtSubjectCode);
            panelTopCard.Controls.Add(lblSubjectCode);
            panelTopCard.Controls.Add(lblCardTitle);
            panelTopCard.Dock = DockStyle.Top;
            panelTopCard.Location = new Point(20, 20);
            panelTopCard.Name = "panelTopCard";
            panelTopCard.Size = new Size(1100, 265);
            panelTopCard.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatus.Location = new Point(806, 168);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(272, 44);
            lblStatus.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(250, 250, 250);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(28, 220);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search subjects...";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 15;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(28, 192);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(65, 21);
            lblSearchTitle.TabIndex = 14;
            lblSearchTitle.Text = "Search:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.Location = new Point(966, 90);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 40);
            btnClear.TabIndex = 13;
            btnClear.Text = "↺  Clear";
            btnClear.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.BackColor = Color.FromArgb(255, 192, 192);
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(198, 40, 40);
            btnDelete.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDelete.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5F);
            btnDelete.ForeColor = Color.FromArgb(198, 40, 40);
            btnDelete.Location = new Point(838, 90);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 40);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "🗑  Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUpdate.BackColor = Color.SkyBlue;
            btnUpdate.FlatAppearance.BorderColor = Color.Navy;
            btnUpdate.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUpdate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F);
            btnUpdate.ForeColor = Color.Navy;
            btnUpdate.Location = new Point(709, 90);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(110, 40);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "✎  Update";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.Green;
            btnAdd.FlatAppearance.BorderColor = Color.DarkGreen;
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 64, 0);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 64, 0);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(965, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "+  Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtPrerequisites
            // 
            txtPrerequisites.BackColor = Color.FromArgb(250, 250, 250);
            txtPrerequisites.BorderStyle = BorderStyle.FixedSingle;
            txtPrerequisites.Font = new Font("Segoe UI", 9.5F);
            txtPrerequisites.Location = new Point(360, 156);
            txtPrerequisites.Name = "txtPrerequisites";
            txtPrerequisites.PlaceholderText = "e.g. IT101, IT201";
            txtPrerequisites.Size = new Size(267, 29);
            txtPrerequisites.TabIndex = 9;
            // 
            // lblPrerequisites
            // 
            lblPrerequisites.AutoSize = true;
            lblPrerequisites.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrerequisites.Location = new Point(360, 131);
            lblPrerequisites.Name = "lblPrerequisites";
            lblPrerequisites.Size = new Size(267, 21);
            lblPrerequisites.TabIndex = 8;
            lblPrerequisites.Text = "Prerequisites (comma-separated):";
            // 
            // numUnits
            // 
            numUnits.BackColor = Color.FromArgb(250, 250, 250);
            numUnits.BorderStyle = BorderStyle.FixedSingle;
            numUnits.Font = new Font("Segoe UI", 9.5F);
            numUnits.Location = new Point(364, 220);
            numUnits.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numUnits.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numUnits.Name = "numUnits";
            numUnits.Size = new Size(80, 29);
            numUnits.TabIndex = 7;
            numUnits.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblUnits
            // 
            lblUnits.AutoSize = true;
            lblUnits.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUnits.Location = new Point(364, 196);
            lblUnits.Name = "lblUnits";
            lblUnits.Size = new Size(54, 21);
            lblUnits.TabIndex = 6;
            lblUnits.Text = "Units:";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(250, 250, 250);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 9.5F);
            txtDescription.Location = new Point(30, 156);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(298, 29);
            txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDescription.Location = new Point(30, 130);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(102, 21);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Description:";
            // 
            // cboCategory
            // 
            cboCategory.BackColor = Color.FromArgb(250, 250, 250);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 9.5F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Items.AddRange(new object[] { "IT", "CS", "GE" });
            cboCategory.Location = new Point(360, 95);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(289, 29);
            cboCategory.TabIndex = 3;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCategory.Location = new Point(360, 71);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(84, 21);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Category:";
            // 
            // txtSubjectCode
            // 
            txtSubjectCode.BackColor = Color.FromArgb(250, 250, 250);
            txtSubjectCode.BorderStyle = BorderStyle.FixedSingle;
            txtSubjectCode.Font = new Font("Segoe UI", 9.5F);
            txtSubjectCode.Location = new Point(30, 95);
            txtSubjectCode.Name = "txtSubjectCode";
            txtSubjectCode.PlaceholderText = "Auto-generated";
            txtSubjectCode.ReadOnly = true;
            txtSubjectCode.Size = new Size(300, 29);
            txtSubjectCode.TabIndex = 1;
            // 
            // lblSubjectCode
            // 
            lblSubjectCode.AutoSize = true;
            lblSubjectCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSubjectCode.Location = new Point(30, 71);
            lblSubjectCode.Name = "lblSubjectCode";
            lblSubjectCode.Size = new Size(165, 21);
            lblSubjectCode.TabIndex = 0;
            lblSubjectCode.Text = "Subject Code (auto):";
            // 
            // lblCardTitle
            // 
            lblCardTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblCardTitle.Location = new Point(30, 15);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(400, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "▧  Course/Subject Information";
            // 
            // SubjectControl
            // 
            Controls.Add(panelMain);
            Name = "SubjectControl";
            Size = new Size(1140, 900);
            panelMain.ResumeLayout(false);
            panelListCard.ResumeLayout(false);
            panelListCard.PerformLayout();
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panelTopCard.ResumeLayout(false);
            panelTopCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUnits).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelListCard;
        private System.Windows.Forms.Panel panelGridContainer;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblFilterCategory;
        private BorderedComboBox cboFilterCategory;
        private System.Windows.Forms.Panel panelTopCard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtPrerequisites;
        private System.Windows.Forms.Label lblPrerequisites;
        private System.Windows.Forms.NumericUpDown numUnits;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private BorderedComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtSubjectCode;
        private System.Windows.Forms.Label lblSubjectCode;
        private System.Windows.Forms.Label lblCardTitle;
    }
}
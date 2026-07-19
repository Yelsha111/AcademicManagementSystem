using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class ProgramControl
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
            lblFilterCollege = new Label();
            cboFilterCollege = new BorderedComboBox();
            panelTopCard = new Panel();
            lblStatus = new Label();
            txtSearch = new TextBox();
            lblSearchTitle = new Label();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            cboCollegeId = new BorderedComboBox();
            lblCollege = new Label();
            txtProgramName = new TextBox();
            lblProgramName = new Label();
            txtProgramId = new TextBox();
            lblProgramId = new Label();
            lblCardTitle = new Label();
            panelMain.SuspendLayout();
            panelListCard.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panelTopCard.SuspendLayout();
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
            panelListCard.Controls.Add(lblFilterCollege);
            panelListCard.Controls.Add(cboFilterCollege);
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
            lblListTitle.Text = "☰  Program List";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFilterCollege
            // 
            lblFilterCollege.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFilterCollege.AutoSize = true;
            lblFilterCollege.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFilterCollege.ForeColor = Color.FromArgb(150, 20, 20);
            lblFilterCollege.Location = new Point(728, 32);
            lblFilterCollege.Name = "lblFilterCollege";
            lblFilterCollege.Size = new Size(130, 21);
            lblFilterCollege.TabIndex = 2;
            lblFilterCollege.Text = "Filter by College:";
            // 
            // cboFilterCollege
            // 
            cboFilterCollege.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboFilterCollege.BackColor = Color.FromArgb(250, 250, 250);
            cboFilterCollege.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCollege.Font = new Font("Segoe UI", 9.5F);
            cboFilterCollege.Location = new Point(883, 26);
            cboFilterCollege.Name = "cboFilterCollege";
            cboFilterCollege.Size = new Size(195, 29);
            cboFilterCollege.TabIndex = 3;
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
            panelTopCard.Controls.Add(cboCollegeId);
            panelTopCard.Controls.Add(lblCollege);
            panelTopCard.Controls.Add(txtProgramName);
            panelTopCard.Controls.Add(lblProgramName);
            panelTopCard.Controls.Add(txtProgramId);
            panelTopCard.Controls.Add(lblProgramId);
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
            txtSearch.Location = new Point(28, 218);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search programs...";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 12;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(28, 189);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(65, 21);
            lblSearchTitle.TabIndex = 11;
            lblSearchTitle.Text = "Search:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.Location = new Point(965, 90);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 40);
            btnClear.TabIndex = 8;
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
            btnDelete.Location = new Point(839, 90);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 40);
            btnDelete.TabIndex = 7;
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
            btnUpdate.Location = new Point(714, 90);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(110, 40);
            btnUpdate.TabIndex = 6;
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
            btnAdd.TabIndex = 5;
            btnAdd.Text = "+  Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // cboCollegeId
            // 
            cboCollegeId.BackColor = Color.FromArgb(250, 250, 250);
            cboCollegeId.DisplayMember = "Display";
            cboCollegeId.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCollegeId.Font = new Font("Segoe UI", 9.5F);
            cboCollegeId.Location = new Point(30, 153);
            cboCollegeId.Name = "cboCollegeId";
            cboCollegeId.Size = new Size(630, 29);
            cboCollegeId.TabIndex = 10;
            cboCollegeId.ValueMember = "CollegeId";
            // 
            // lblCollege
            // 
            lblCollege.AutoSize = true;
            lblCollege.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCollege.Location = new Point(30, 130);
            lblCollege.Name = "lblCollege";
            lblCollege.Size = new Size(72, 21);
            lblCollege.TabIndex = 9;
            lblCollege.Text = "College:";
            // 
            // txtProgramName
            // 
            txtProgramName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProgramName.BackColor = Color.FromArgb(250, 250, 250);
            txtProgramName.BorderStyle = BorderStyle.FixedSingle;
            txtProgramName.Font = new Font("Segoe UI", 9.5F);
            txtProgramName.Location = new Point(360, 95);
            txtProgramName.Name = "txtProgramName";
            txtProgramName.Size = new Size(298, 29);
            txtProgramName.TabIndex = 4;
            // 
            // lblProgramName
            // 
            lblProgramName.AutoSize = true;
            lblProgramName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProgramName.Location = new Point(360, 72);
            lblProgramName.Name = "lblProgramName";
            lblProgramName.Size = new Size(130, 21);
            lblProgramName.TabIndex = 3;
            lblProgramName.Text = "Program Name:";
            // 
            // txtProgramId
            // 
            txtProgramId.BackColor = Color.FromArgb(250, 250, 250);
            txtProgramId.BorderStyle = BorderStyle.FixedSingle;
            txtProgramId.Font = new Font("Segoe UI", 9.5F);
            txtProgramId.Location = new Point(30, 95);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.PlaceholderText = "Auto-generated";
            txtProgramId.ReadOnly = true;
            txtProgramId.Size = new Size(300, 29);
            txtProgramId.TabIndex = 2;
            // 
            // lblProgramId
            // 
            lblProgramId.AutoSize = true;
            lblProgramId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProgramId.Location = new Point(30, 72);
            lblProgramId.Name = "lblProgramId";
            lblProgramId.Size = new Size(152, 21);
            lblProgramId.TabIndex = 1;
            lblProgramId.Text = "Program ID (auto):";
            // 
            // lblCardTitle
            // 
            lblCardTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblCardTitle.Location = new Point(30, 15);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(400, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "▣  Program Information";
            // 
            // ProgramControl
            // 
            Controls.Add(panelMain);
            Name = "ProgramControl";
            Size = new Size(1140, 900);
            panelMain.ResumeLayout(false);
            panelListCard.ResumeLayout(false);
            panelListCard.PerformLayout();
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panelTopCard.ResumeLayout(false);
            panelTopCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelListCard;
        private System.Windows.Forms.Panel panelGridContainer;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblFilterCollege;
        private BorderedComboBox cboFilterCollege;
        private System.Windows.Forms.Panel panelTopCard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private BorderedComboBox cboCollegeId;
        private System.Windows.Forms.Label lblCollege;
        private System.Windows.Forms.TextBox txtProgramName;
        private System.Windows.Forms.Label lblProgramName;
        private System.Windows.Forms.TextBox txtProgramId;
        private System.Windows.Forms.Label lblProgramId;
        private System.Windows.Forms.Label lblCardTitle;
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class CollegeControl
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
            panelTopCard = new Panel();
            lblStatus = new Label();
            txtSearch = new TextBox();
            lblSearchTitle = new Label();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtCollegeName = new TextBox();
            lblCollegeName = new Label();
            txtCollegeId = new TextBox();
            lblCollegeId = new Label();
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
            lblListTitle.Text = "☰  College List";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
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
            panelTopCard.Controls.Add(txtCollegeName);
            panelTopCard.Controls.Add(lblCollegeName);
            panelTopCard.Controls.Add(txtCollegeId);
            panelTopCard.Controls.Add(lblCollegeId);
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
            txtSearch.Location = new Point(28, 168);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search colleges...";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 10;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(28, 144);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(65, 21);
            lblSearchTitle.TabIndex = 9;
            lblSearchTitle.Text = "Search:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.Location = new Point(961, 90);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 40);
            btnClear.TabIndex = 8;
            btnClear.Text = "↺  Clear";
            btnClear.UseVisualStyleBackColor = true;
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
            btnDelete.Location = new Point(833, 90);
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
            btnUpdate.BackgroundImageLayout = ImageLayout.None;
            btnUpdate.FlatAppearance.BorderColor = Color.Navy;
            btnUpdate.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUpdate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F);
            btnUpdate.ForeColor = Color.Navy;
            btnUpdate.Location = new Point(704, 90);
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
            btnAdd.Location = new Point(959, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "+  Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtCollegeName
            // 
            txtCollegeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCollegeName.BackColor = Color.FromArgb(250, 250, 250);
            txtCollegeName.BorderStyle = BorderStyle.FixedSingle;
            txtCollegeName.Font = new Font("Segoe UI", 9.5F);
            txtCollegeName.Location = new Point(360, 95);
            txtCollegeName.Name = "txtCollegeName";
            txtCollegeName.Size = new Size(298, 29);
            txtCollegeName.TabIndex = 4;
            // 
            // lblCollegeName
            // 
            lblCollegeName.AutoSize = true;
            lblCollegeName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCollegeName.Location = new Point(360, 72);
            lblCollegeName.Name = "lblCollegeName";
            lblCollegeName.Size = new Size(122, 21);
            lblCollegeName.TabIndex = 3;
            lblCollegeName.Text = "College Name:";
            // 
            // txtCollegeId
            // 
            txtCollegeId.BackColor = Color.FromArgb(250, 250, 250);
            txtCollegeId.BorderStyle = BorderStyle.FixedSingle;
            txtCollegeId.Font = new Font("Segoe UI", 9.5F);
            txtCollegeId.Location = new Point(30, 95);
            txtCollegeId.Name = "txtCollegeId";
            txtCollegeId.PlaceholderText = "Auto-generated";
            txtCollegeId.ReadOnly = true;
            txtCollegeId.Size = new Size(300, 29);
            txtCollegeId.TabIndex = 2;
            // 
            // lblCollegeId
            // 
            lblCollegeId.AutoSize = true;
            lblCollegeId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCollegeId.Location = new Point(30, 72);
            lblCollegeId.Name = "lblCollegeId";
            lblCollegeId.Size = new Size(144, 21);
            lblCollegeId.TabIndex = 1;
            lblCollegeId.Text = "College ID (auto):";
            // 
            // lblCardTitle
            // 
            lblCardTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblCardTitle.Location = new Point(30, 15);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(400, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "🏫  College Information";
            // 
            // CollegeControl
            // 
            Controls.Add(panelMain);
            Name = "CollegeControl";
            Size = new Size(1140, 900);
            panelMain.ResumeLayout(false);
            panelListCard.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelTopCard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtCollegeName;
        private System.Windows.Forms.Label lblCollegeName;
        private System.Windows.Forms.TextBox txtCollegeId;
        private System.Windows.Forms.Label lblCollegeId;
        private System.Windows.Forms.Label lblCardTitle;
    }
}
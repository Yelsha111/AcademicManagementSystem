using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class OfferingControl
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
            lblFilterStatus = new Label();
            cboFilterStatus = new BorderedComboBox();
            panelTopCard = new Panel();
            lblStatusMsg = new Label();
            txtSearch = new TextBox();
            lblSearchTitle = new Label();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            cboStatus = new BorderedComboBox();
            lblStatus = new Label();
            txtSemester = new TextBox();
            lblSemester = new Label();
            txtSchoolYear = new TextBox();
            lblSchoolYear = new Label();
            cboSubjectCode = new BorderedComboBox();
            lblSubject = new Label();
            txtOfferingID = new TextBox();
            lblOfferingID = new Label();
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
            panelListCard.Controls.Add(lblFilterStatus);
            panelListCard.Controls.Add(cboFilterStatus);
            panelListCard.Dock = DockStyle.Fill;
            panelListCard.Location = new Point(20, 292);
            panelListCard.Margin = new Padding(0, 20, 0, 0);
            panelListCard.Name = "panelListCard";
            panelListCard.Padding = new Padding(0, 20, 0, 0);
            panelListCard.Size = new Size(1100, 588);
            panelListCard.TabIndex = 1;
            // 
            // panelGridContainer
            // 
            panelGridContainer.Controls.Add(grid);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 60);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(20, 0, 20, 0);
            panelGridContainer.Size = new Size(1098, 526);
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
            grid.Size = new Size(1058, 526);
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
            lblListTitle.Text = "▦  Academic Offering List";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblListTitle.Click += lblListTitle_Click;
            // 
            // lblFilterStatus
            // 
            lblFilterStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFilterStatus.AutoSize = true;
            lblFilterStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFilterStatus.ForeColor = Color.FromArgb(150, 20, 20);
            lblFilterStatus.Location = new Point(743, 32);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new Size(127, 21);
            lblFilterStatus.TabIndex = 2;
            lblFilterStatus.Text = "Filter by Status:";
            // 
            // cboFilterStatus
            // 
            cboFilterStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboFilterStatus.BackColor = Color.FromArgb(250, 250, 250);
            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.FlatStyle = FlatStyle.Flat;
            cboFilterStatus.Font = new Font("Segoe UI", 9.5F);
            cboFilterStatus.Location = new Point(883, 26);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(195, 29);
            cboFilterStatus.TabIndex = 3;
            // 
            // panelTopCard
            // 
            panelTopCard.BackColor = Color.White;
            panelTopCard.BorderStyle = BorderStyle.FixedSingle;
            panelTopCard.Controls.Add(lblStatusMsg);
            panelTopCard.Controls.Add(txtSearch);
            panelTopCard.Controls.Add(lblSearchTitle);
            panelTopCard.Controls.Add(btnClear);
            panelTopCard.Controls.Add(btnDelete);
            panelTopCard.Controls.Add(btnUpdate);
            panelTopCard.Controls.Add(btnAdd);
            panelTopCard.Controls.Add(cboStatus);
            panelTopCard.Controls.Add(lblStatus);
            panelTopCard.Controls.Add(txtSemester);
            panelTopCard.Controls.Add(lblSemester);
            panelTopCard.Controls.Add(txtSchoolYear);
            panelTopCard.Controls.Add(lblSchoolYear);
            panelTopCard.Controls.Add(cboSubjectCode);
            panelTopCard.Controls.Add(lblSubject);
            panelTopCard.Controls.Add(txtOfferingID);
            panelTopCard.Controls.Add(lblOfferingID);
            panelTopCard.Controls.Add(lblCardTitle);
            panelTopCard.Dock = DockStyle.Top;
            panelTopCard.Location = new Point(20, 20);
            panelTopCard.Name = "panelTopCard";
            panelTopCard.Size = new Size(1100, 272);
            panelTopCard.TabIndex = 0;
            panelTopCard.Paint += panelTopCard_Paint;
            // 
            // lblStatusMsg
            // 
            lblStatusMsg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatusMsg.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblStatusMsg.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatusMsg.Location = new Point(806, 168);
            lblStatusMsg.Name = "lblStatusMsg";
            lblStatusMsg.Size = new Size(272, 75);
            lblStatusMsg.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(250, 250, 250);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(28, 214);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search offerings...";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 16;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(28, 188);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(65, 21);
            lblSearchTitle.TabIndex = 15;
            lblSearchTitle.Text = "Search:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.Location = new Point(968, 90);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 40);
            btnClear.TabIndex = 14;
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
            btnDelete.Location = new Point(841, 90);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 40);
            btnDelete.TabIndex = 13;
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
            btnUpdate.Location = new Point(712, 90);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(110, 40);
            btnUpdate.TabIndex = 12;
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
            btnAdd.Location = new Point(966, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "+  Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // cboStatus
            // 
            cboStatus.BackColor = Color.FromArgb(250, 250, 250);
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FlatStyle = FlatStyle.Flat;
            cboStatus.Font = new Font("Segoe UI", 9.5F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Items.AddRange(new object[] { "Open", "Closed", "Cancelled" });
            cboStatus.Location = new Point(360, 215);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(150, 29);
            cboStatus.TabIndex = 9;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.Location = new Point(360, 189);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(61, 21);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status:";
            // 
            // txtSemester
            // 
            txtSemester.BackColor = Color.FromArgb(250, 250, 250);
            txtSemester.BorderStyle = BorderStyle.FixedSingle;
            txtSemester.Font = new Font("Segoe UI", 9.5F);
            txtSemester.Location = new Point(360, 153);
            txtSemester.Name = "txtSemester";
            txtSemester.PlaceholderText = "Select a Subject first";
            txtSemester.ReadOnly = true;
            txtSemester.Size = new Size(150, 29);
            txtSemester.TabIndex = 7;
            // 
            // lblSemester
            // 
            lblSemester.AutoSize = true;
            lblSemester.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSemester.Location = new Point(360, 129);
            lblSemester.Name = "lblSemester";
            lblSemester.Size = new Size(135, 21);
            lblSemester.TabIndex = 6;
            lblSemester.Text = "Semester (auto):";
            // 
            // txtSchoolYear
            // 
            txtSchoolYear.BackColor = Color.FromArgb(250, 250, 250);
            txtSchoolYear.BorderStyle = BorderStyle.FixedSingle;
            txtSchoolYear.Font = new Font("Segoe UI", 9.5F);
            txtSchoolYear.Location = new Point(30, 153);
            txtSchoolYear.Name = "txtSchoolYear";
            txtSchoolYear.Size = new Size(300, 29);
            txtSchoolYear.TabIndex = 5;
            txtSchoolYear.Text = "2025-2026";
            // 
            // lblSchoolYear
            // 
            lblSchoolYear.AutoSize = true;
            lblSchoolYear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSchoolYear.Location = new Point(30, 129);
            lblSchoolYear.Name = "lblSchoolYear";
            lblSchoolYear.Size = new Size(104, 21);
            lblSchoolYear.TabIndex = 4;
            lblSchoolYear.Text = "School Year:";
            // 
            // cboSubjectCode
            // 
            cboSubjectCode.BackColor = Color.FromArgb(250, 250, 250);
            cboSubjectCode.DisplayMember = "Display";
            cboSubjectCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSubjectCode.FlatStyle = FlatStyle.Flat;
            cboSubjectCode.Font = new Font("Segoe UI", 9.5F);
            cboSubjectCode.FormattingEnabled = true;
            cboSubjectCode.Location = new Point(360, 95);
            cboSubjectCode.Name = "cboSubjectCode";
            cboSubjectCode.Size = new Size(275, 29);
            cboSubjectCode.TabIndex = 3;
            cboSubjectCode.ValueMember = "SubjectCode";
            // 
            // lblSubject
            // 
            lblSubject.AutoSize = true;
            lblSubject.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSubject.Location = new Point(360, 72);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new Size(71, 21);
            lblSubject.TabIndex = 2;
            lblSubject.Text = "Subject:";
            // 
            // txtOfferingID
            // 
            txtOfferingID.BackColor = Color.FromArgb(250, 250, 250);
            txtOfferingID.BorderStyle = BorderStyle.FixedSingle;
            txtOfferingID.Font = new Font("Segoe UI", 9.5F);
            txtOfferingID.Location = new Point(30, 95);
            txtOfferingID.Name = "txtOfferingID";
            txtOfferingID.PlaceholderText = "Auto-generated";
            txtOfferingID.ReadOnly = true;
            txtOfferingID.Size = new Size(300, 29);
            txtOfferingID.TabIndex = 1;
            // 
            // lblOfferingID
            // 
            lblOfferingID.AutoSize = true;
            lblOfferingID.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOfferingID.Location = new Point(30, 72);
            lblOfferingID.Name = "lblOfferingID";
            lblOfferingID.Size = new Size(150, 21);
            lblOfferingID.TabIndex = 0;
            lblOfferingID.Text = "Offering ID (auto):";
            // 
            // lblCardTitle
            // 
            lblCardTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblCardTitle.Location = new Point(30, 15);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(400, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "▦  Academic Offering Information";
            // 
            // OfferingControl
            // 
            Controls.Add(panelMain);
            Name = "OfferingControl";
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
        private System.Windows.Forms.Label lblFilterStatus;
        private BorderedComboBox cboFilterStatus;
        private System.Windows.Forms.Panel panelTopCard;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private BorderedComboBox cboStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSemester;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.TextBox txtSchoolYear;
        private System.Windows.Forms.Label lblSchoolYear;
        private BorderedComboBox cboSubjectCode;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtOfferingID;
        private System.Windows.Forms.Label lblOfferingID;
        private System.Windows.Forms.Label lblCardTitle;
    }
}
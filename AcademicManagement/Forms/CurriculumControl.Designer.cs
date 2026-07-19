using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class CurriculumControl
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
            numYearLevel = new NumericUpDown();
            lblYearLevel = new Label();
            cboSemester = new BorderedComboBox();
            lblSemester = new Label();
            cboSubjectCode = new BorderedComboBox();
            lblSubject = new Label();
            cboProgramId = new BorderedComboBox();
            lblProgram = new Label();
            txtCurriculumID = new TextBox();
            lblCurriculumID = new Label();
            lblCardTitle = new Label();
            panelMain.SuspendLayout();
            panelListCard.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panelTopCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numYearLevel).BeginInit();
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
            panelListCard.Location = new Point(20, 290);
            panelListCard.Margin = new Padding(0, 20, 0, 0);
            panelListCard.Name = "panelListCard";
            panelListCard.Padding = new Padding(0, 20, 0, 0);
            panelListCard.Size = new Size(1100, 590);
            panelListCard.TabIndex = 1;
            // 
            // panelGridContainer
            // 
            panelGridContainer.Controls.Add(grid);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 60);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(20, 0, 20, 0);
            panelGridContainer.Size = new Size(1098, 528);
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
            grid.Size = new Size(1058, 528);
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
            lblListTitle.Text = "▥  Curriculum List";
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
            panelTopCard.Controls.Add(numYearLevel);
            panelTopCard.Controls.Add(lblYearLevel);
            panelTopCard.Controls.Add(cboSemester);
            panelTopCard.Controls.Add(lblSemester);
            panelTopCard.Controls.Add(cboSubjectCode);
            panelTopCard.Controls.Add(lblSubject);
            panelTopCard.Controls.Add(cboProgramId);
            panelTopCard.Controls.Add(lblProgram);
            panelTopCard.Controls.Add(txtCurriculumID);
            panelTopCard.Controls.Add(lblCurriculumID);
            panelTopCard.Controls.Add(lblCardTitle);
            panelTopCard.Dock = DockStyle.Top;
            panelTopCard.Location = new Point(20, 20);
            panelTopCard.Name = "panelTopCard";
            panelTopCard.Size = new Size(1100, 270);
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
            txtSearch.Location = new Point(32, 220);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search curriculum...";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 15;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearchTitle.Location = new Point(28, 194);
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
            btnClear.Location = new Point(969, 90);
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
            btnDelete.Location = new Point(842, 90);
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
            btnUpdate.Location = new Point(714, 90);
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
            btnAdd.Location = new Point(967, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "+  Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // numYearLevel
            // 
            numYearLevel.BackColor = Color.FromArgb(250, 250, 250);
            numYearLevel.Font = new Font("Segoe UI", 9.5F);
            numYearLevel.Location = new Point(360, 220);
            numYearLevel.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numYearLevel.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numYearLevel.Name = "numYearLevel";
            numYearLevel.Size = new Size(80, 29);
            numYearLevel.TabIndex = 9;
            numYearLevel.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblYearLevel
            // 
            lblYearLevel.AutoSize = true;
            lblYearLevel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblYearLevel.Location = new Point(360, 194);
            lblYearLevel.Name = "lblYearLevel";
            lblYearLevel.Size = new Size(91, 21);
            lblYearLevel.TabIndex = 8;
            lblYearLevel.Text = "Year Level:";
            // 
            // cboSemester
            // 
            cboSemester.BackColor = Color.FromArgb(250, 250, 250);
            cboSemester.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSemester.Font = new Font("Segoe UI", 9.5F);
            cboSemester.FormattingEnabled = true;
            cboSemester.Items.AddRange(new object[] { "1st", "2nd", "Summer" });
            cboSemester.Location = new Point(360, 153);
            cboSemester.Name = "cboSemester";
            cboSemester.Size = new Size(150, 29);
            cboSemester.TabIndex = 7;
            // 
            // lblSemester
            // 
            lblSemester.AutoSize = true;
            lblSemester.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSemester.Location = new Point(360, 130);
            lblSemester.Name = "lblSemester";
            lblSemester.Size = new Size(85, 21);
            lblSemester.TabIndex = 6;
            lblSemester.Text = "Semester:";
            // 
            // cboSubjectCode
            // 
            cboSubjectCode.BackColor = Color.FromArgb(250, 250, 250);
            cboSubjectCode.DisplayMember = "Display";
            cboSubjectCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSubjectCode.Font = new Font("Segoe UI", 9.5F);
            cboSubjectCode.FormattingEnabled = true;
            cboSubjectCode.Location = new Point(30, 153);
            cboSubjectCode.Name = "cboSubjectCode";
            cboSubjectCode.Size = new Size(300, 29);
            cboSubjectCode.TabIndex = 5;
            cboSubjectCode.ValueMember = "SubjectCode";
            // 
            // lblSubject
            // 
            lblSubject.AutoSize = true;
            lblSubject.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSubject.Location = new Point(30, 130);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new Size(71, 21);
            lblSubject.TabIndex = 4;
            lblSubject.Text = "Subject:";
            // 
            // cboProgramId
            // 
            cboProgramId.BackColor = Color.FromArgb(250, 250, 250);
            cboProgramId.DisplayMember = "Display";
            cboProgramId.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProgramId.Font = new Font("Segoe UI", 9.5F);
            cboProgramId.FormattingEnabled = true;
            cboProgramId.Location = new Point(360, 95);
            cboProgramId.Name = "cboProgramId";
            cboProgramId.Size = new Size(289, 29);
            cboProgramId.TabIndex = 3;
            cboProgramId.ValueMember = "ProgramId";
            // 
            // lblProgram
            // 
            lblProgram.AutoSize = true;
            lblProgram.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProgram.Location = new Point(360, 72);
            lblProgram.Name = "lblProgram";
            lblProgram.Size = new Size(80, 21);
            lblProgram.TabIndex = 2;
            lblProgram.Text = "Program:";
            // 
            // txtCurriculumID
            // 
            txtCurriculumID.BackColor = Color.FromArgb(250, 250, 250);
            txtCurriculumID.BorderStyle = BorderStyle.FixedSingle;
            txtCurriculumID.Font = new Font("Segoe UI", 9.5F);
            txtCurriculumID.Location = new Point(30, 95);
            txtCurriculumID.Name = "txtCurriculumID";
            txtCurriculumID.PlaceholderText = "Auto-generated";
            txtCurriculumID.ReadOnly = true;
            txtCurriculumID.Size = new Size(300, 29);
            txtCurriculumID.TabIndex = 1;
            // 
            // lblCurriculumID
            // 
            lblCurriculumID.AutoSize = true;
            lblCurriculumID.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCurriculumID.Location = new Point(30, 72);
            lblCurriculumID.Name = "lblCurriculumID";
            lblCurriculumID.Size = new Size(171, 21);
            lblCurriculumID.TabIndex = 0;
            lblCurriculumID.Text = "Curriculum ID (auto):";
            // 
            // lblCardTitle
            // 
            lblCardTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(150, 20, 20);
            lblCardTitle.Location = new Point(30, 15);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(400, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "▥  Curriculum Information";
            // 
            // CurriculumControl
            // 
            Controls.Add(panelMain);
            Name = "CurriculumControl";
            Size = new Size(1140, 900);
            panelMain.ResumeLayout(false);
            panelListCard.ResumeLayout(false);
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panelTopCard.ResumeLayout(false);
            panelTopCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numYearLevel).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTopCard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numYearLevel;
        private BorderedComboBox cboSemester;
        private System.Windows.Forms.Label lblSemester;
        private BorderedComboBox cboSubjectCode;
        private System.Windows.Forms.Label lblSubject;
        private BorderedComboBox cboProgramId;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.TextBox txtCurriculumID;
        private System.Windows.Forms.Label lblCurriculumID;
        private System.Windows.Forms.Label lblCardTitle;
        private Panel panelListCard;
        private Panel panelGridContainer;
        private DataGridView grid;
        private Label lblListTitle;
        private Label lblYearLevel;
    }
}
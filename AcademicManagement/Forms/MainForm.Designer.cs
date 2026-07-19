using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    partial class MainForm
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
            panelSidebar = new Panel();
            btnNavOfferings = new Button();
            btnNavCurriculum = new Button();
            btnNavSubjects = new Button();
            btnNavPrograms = new Button();
            btnNavColleges = new Button();
            lblLogo = new Label();
            panelContent = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(70, 10, 10);
            panelSidebar.Controls.Add(btnNavOfferings);
            panelSidebar.Controls.Add(btnNavCurriculum);
            panelSidebar.Controls.Add(btnNavSubjects);
            panelSidebar.Controls.Add(btnNavPrograms);
            panelSidebar.Controls.Add(btnNavColleges);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(260, 900);
            panelSidebar.TabIndex = 0;
            // 
            // btnNavOfferings
            // 
            btnNavOfferings.FlatAppearance.BorderSize = 0;
            btnNavOfferings.FlatStyle = FlatStyle.Flat;
            btnNavOfferings.Font = new Font("Segoe UI", 10F);
            btnNavOfferings.ForeColor = Color.FromArgb(230, 200, 200);
            btnNavOfferings.Location = new Point(10, 340);
            btnNavOfferings.Name = "btnNavOfferings";
            btnNavOfferings.Size = new Size(240, 45);
            btnNavOfferings.TabIndex = 5;
            btnNavOfferings.Text = "▦  Academic Offerings";
            btnNavOfferings.TextAlign = ContentAlignment.MiddleLeft;
            btnNavOfferings.UseVisualStyleBackColor = false;
            // 
            // btnNavCurriculum
            // 
            btnNavCurriculum.FlatAppearance.BorderSize = 0;
            btnNavCurriculum.FlatStyle = FlatStyle.Flat;
            btnNavCurriculum.Font = new Font("Segoe UI", 10F);
            btnNavCurriculum.ForeColor = Color.FromArgb(230, 200, 200);
            btnNavCurriculum.Location = new Point(10, 290);
            btnNavCurriculum.Name = "btnNavCurriculum";
            btnNavCurriculum.Size = new Size(240, 45);
            btnNavCurriculum.TabIndex = 4;
            btnNavCurriculum.Text = "▥  Curriculum";
            btnNavCurriculum.TextAlign = ContentAlignment.MiddleLeft;
            btnNavCurriculum.UseVisualStyleBackColor = false;
            // 
            // btnNavSubjects
            // 
            btnNavSubjects.FlatAppearance.BorderSize = 0;
            btnNavSubjects.FlatStyle = FlatStyle.Flat;
            btnNavSubjects.Font = new Font("Segoe UI", 10F);
            btnNavSubjects.ForeColor = Color.FromArgb(230, 200, 200);
            btnNavSubjects.Location = new Point(10, 240);
            btnNavSubjects.Name = "btnNavSubjects";
            btnNavSubjects.Size = new Size(240, 45);
            btnNavSubjects.TabIndex = 3;
            btnNavSubjects.Text = "▧  Courses/Subjects";
            btnNavSubjects.TextAlign = ContentAlignment.MiddleLeft;
            btnNavSubjects.UseVisualStyleBackColor = false;
            // 
            // btnNavPrograms
            // 
            btnNavPrograms.FlatAppearance.BorderSize = 0;
            btnNavPrograms.FlatStyle = FlatStyle.Flat;
            btnNavPrograms.Font = new Font("Segoe UI", 10F);
            btnNavPrograms.ForeColor = Color.FromArgb(230, 200, 200);
            btnNavPrograms.Location = new Point(10, 190);
            btnNavPrograms.Name = "btnNavPrograms";
            btnNavPrograms.Size = new Size(240, 45);
            btnNavPrograms.TabIndex = 2;
            btnNavPrograms.Text = "▣  Programs";
            btnNavPrograms.TextAlign = ContentAlignment.MiddleLeft;
            btnNavPrograms.UseVisualStyleBackColor = false;
            // 
            // btnNavColleges
            // 
            btnNavColleges.FlatAppearance.BorderSize = 0;
            btnNavColleges.FlatStyle = FlatStyle.Flat;
            btnNavColleges.Font = new Font("Segoe UI", 10F);
            btnNavColleges.ForeColor = Color.FromArgb(230, 200, 200);
            btnNavColleges.Location = new Point(10, 140);
            btnNavColleges.Name = "btnNavColleges";
            btnNavColleges.Size = new Size(240, 45);
            btnNavColleges.TabIndex = 1;
            btnNavColleges.Text = "🏫  Colleges";
            btnNavColleges.TextAlign = ContentAlignment.MiddleLeft;
            btnNavColleges.UseVisualStyleBackColor = false;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(20, 25);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(230, 70);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🎓  Academic\r\n     Management";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(244, 244, 244);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(260, 0);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 900);
            panelContent.TabIndex = 1;
            // 
            // MainForm
            // 
            ClientSize = new Size(1400, 900);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1100, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Academic Management";
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnNavOfferings;
        private System.Windows.Forms.Button btnNavCurriculum;
        private System.Windows.Forms.Button btnNavSubjects;
        private System.Windows.Forms.Button btnNavPrograms;
        private System.Windows.Forms.Button btnNavColleges;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel panelContent;
    }
}

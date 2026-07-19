using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    public partial class MainForm : Form
    {
        private static readonly Color ActiveBackColor = Color.FromArgb(198, 40, 40);
        private static readonly Color InactiveForeColor = Color.FromArgb(230, 200, 200);
        private static readonly Color SidebarColor = Color.FromArgb(70, 10, 10);

        public MainForm()
        {
            InitializeComponent();

            btnNavColleges.Click += (s, e) => LoadModule(new CollegeControl(), btnNavColleges);
            btnNavPrograms.Click += (s, e) => LoadModule(new ProgramControl(), btnNavPrograms);
            btnNavSubjects.Click += (s, e) => LoadModule(new SubjectControl(), btnNavSubjects);
            btnNavCurriculum.Click += (s, e) => LoadModule(new CurriculumControl(), btnNavCurriculum);
            btnNavOfferings.Click += (s, e) => LoadModule(new OfferingControl(), btnNavOfferings);

            // Colleges loads by default when the app starts.
            LoadModule(new CollegeControl(), btnNavColleges);
        }

        // Swaps whatever is inside panelContent for the requested module.
        // No new window is created and no Hide()/Show() happens, so there's
        // no window creation/animation "jump" when switching modules.
        private void LoadModule(UserControl control, Button activeButton)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);

            foreach (Control c in panelSidebar.Controls)
            {
                if (c is Button btn)
                {
                    bool isActive = btn == activeButton;
                    btn.BackColor = isActive ? ActiveBackColor : SidebarColor;
                    btn.ForeColor = isActive ? Color.White : InactiveForeColor;
                    btn.Font = new Font("Segoe UI", 10F, isActive ? FontStyle.Bold : FontStyle.Regular);
                }
            }
        }
    }
}

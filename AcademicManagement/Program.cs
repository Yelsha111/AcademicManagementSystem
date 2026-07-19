using System;
using System.Windows.Forms;

namespace AcademicManagement
{
    // NOTE: This class is named "Program" but lives in the AcademicManagement
    // namespace (not AcademicManagement.Models), so it never conflicts with the
    // Models.Program class used for academic programs (BSIT, BSCS, etc.).
    // This file does not "using AcademicManagement.Models;", so "Program" here
    // always resolves to this entry-point class.
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MainForm());
        }
    }
}
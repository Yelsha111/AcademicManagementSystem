using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcademicManagement.Forms
{
    // A drop-in replacement for ComboBox that reliably shows a solid colored
    // border.
    //
    // Why this exists: a plain ComboBox draws its border in the "non-client"
    // area of the window (the frame Windows itself is responsible for), not
    // in the normal client area that the Paint event covers. That's why a
    // border drawn from a Paint handler would flash in briefly, then get
    // wiped out again the moment the control gained focus, was hovered, or
    // the window was resized - the OS was simply repainting its own border
    // right over it.
    //
    // This subclass intercepts WM_PAINT and WM_NCPAINT directly and redraws
    // the border every time Windows does, so it never gets overwritten.
    public class BorderedComboBox : ComboBox
    {
        // The [DesignerSerializationVisibility(Visible)] attribute tells the
        // WinForms designer to serialize this property as a plain
        // "control.BorderColor = Color.X;" assignment. Without it, the
        // designer sometimes infers "Content" visibility for non-trivial
        // property types and then fails with "Property 'BorderColor' does
        // not configure the code serialization for its property content" -
        // it doesn't know how to serialize Color as a set of sub-properties,
        // which is what Content visibility expects.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "173, 173, 173")]
        public Color BorderColor { get; set; } = Color.FromArgb(173, 173, 173);

        private const int WM_PAINT = 0x000F;
        private const int WM_NCPAINT = 0x0085;

        public BorderedComboBox()
        {
            // Standard (the default) draws the OS's own themed border/bevel
            // on top of the client area, which is what was making our
            // custom-drawn border look lighter/inconsistent next to a plain
            // FixedSingle TextBox. Flat removes that OS chrome entirely so
            // our WM_NCPAINT/WM_PAINT border is the only border drawn.
            FlatStyle = FlatStyle.Flat;

            // FlatStyle.Flat defaults to SystemColors.Control (light gray)
            // as its background instead of white, which is what made the
            // box look shaded/disabled next to a white TextBox. Force it
            // white here so every dropdown, in every module, matches
            // without needing to set BackColor per-designer-file.
            BackColor = Color.White;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT || m.Msg == WM_NCPAINT)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    ControlPaint.DrawBorder(g, new Rectangle(0, 0, Width, Height),
                        BorderColor, 1, ButtonBorderStyle.Solid,
                        BorderColor, 1, ButtonBorderStyle.Solid,
                        BorderColor, 1, ButtonBorderStyle.Solid,
                        BorderColor, 1, ButtonBorderStyle.Solid);
                }
            }
        }
    }
}
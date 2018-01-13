using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class ToolStripProgressBarEx : ToolStripProgressBar
    {
        //private MyProgressBarControl _progbar = new MyProgressBarControl();

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text { get => base.Text; set => base.Text = value; }


        public ToolStripProgressBarEx()
        {
            Paint += (s_, e_) =>
            {
                e_.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(Padding.Left, Padding.Right));
            };
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //}

        //private class MyProgressBarControl : ProgressBar
        //{

        //    protected override void OnPaint(PaintEventArgs e)
        //    {
        //        base.OnPaint(e);
        //        e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(Padding.Left, Padding.Right));
        //    }
        //}
    }
}

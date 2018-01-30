using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class FancyToolStripSeparator : ToolStripSeparator
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                using (var tempToolStrip = new ToolStrip())
                {
                    Type t = typeof(ToolStrip).Assembly.GetType("System.Windows.Forms.ToolStripGrip");
                    var ctor = t.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
                    using (var fakeGrip = (ToolStripButton)ctor.Invoke(new object[] { }))
                    {
                        typeof(ToolStripItem).GetField("bounds", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(fakeGrip, e.ClipRectangle);
                        tempToolStrip.GetType().GetField("toolStripGrip", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tempToolStrip, fakeGrip);
                        var args = new ToolStripGripRenderEventArgs(e.Graphics, tempToolStrip);
                        Owner.Renderer.DrawGrip(args);
                    }
                }
            }
            catch
            {
                base.OnPaint(e);
            }
        }
    }
}

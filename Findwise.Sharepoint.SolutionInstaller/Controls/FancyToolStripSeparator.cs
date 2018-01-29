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
                //using (var fakeGrip = (ToolStripButton)Activator.CreateInstance(typeof(ToolStrip).GetType().Assembly.FullName, "System.Windows.Forms.ToolStripGrip").Unwrap())
                {
                    Type t = typeof(ToolStrip).Assembly.GetType("System.Windows.Forms.ToolStripGrip");
                    var ctor = t.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
                    using (var fakeGrip = (ToolStripButton)ctor.Invoke(new object[] { }))
                    {
                        typeof(ToolStripItem).GetField("bounds", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(fakeGrip, Bounds);
                        tempToolStrip.GetType().GetField("toolStripGrip", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tempToolStrip, fakeGrip);
                        var args = new ToolStripGripRenderEventArgs(e.Graphics, tempToolStrip);
                        new MyRenderer().DrawGrip(args);
                    }
                }
            }
            catch
            {
                base.OnPaint(e);
            }
        }

        private class MyRenderer : ToolStripRenderer
        {
            protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
            {
                base.OnRenderGrip(e);
            }
        }
        private class MyToolStripGripRenderEventArgs : ToolStripGripRenderEventArgs
        {
            public MyToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip) : base(g, toolStrip)
            {
            }
        }
    }
}

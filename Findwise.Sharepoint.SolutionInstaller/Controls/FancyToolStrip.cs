using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class FancyToolStrip : ToolStrip
    {
        public Image SpecialBackgroundImage { get; set; }

        public Color BackgroundGradientColor { get; set; }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (BackgroundGradientColor != Color.Empty)
            {
                RectangleF backrect = ClientRectangle;
                var lastElementRect = Items.Cast<ToolStripItem>().Last(item => item.Alignment == ToolStripItemAlignment.Left).Bounds;
                var borderline = lastElementRect.Left + lastElementRect.Width;
                backrect.Width -= borderline;
                backrect.Offset(borderline, 0);
                e.Graphics.FillRectangle(new LinearGradientBrush(backrect, BackColor, BackgroundGradientColor, LinearGradientMode.Horizontal), backrect);
                e.Graphics.FillRectangle(new SolidBrush(BackColor), new RectangleF(borderline - 1, 0, 2, backrect.Height));
            }

            if (SpecialBackgroundImage != null)
            {
                var factor = (float)Height / (float)SpecialBackgroundImage.Height;
                var destSize = new SizeF(SpecialBackgroundImage.Width * factor, SpecialBackgroundImage.Height * factor);
                var destRect = new RectangleF(new Point(Width - (int)destSize.Width - Padding.Right, 0), destSize);
                e.Graphics.DrawImage(SpecialBackgroundImage, destRect);
            }
        }
    }
}

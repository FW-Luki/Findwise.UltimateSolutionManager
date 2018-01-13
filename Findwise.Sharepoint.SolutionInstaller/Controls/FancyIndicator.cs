using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class FancyIndicator : ToolStripItem
    {
        private Timer _timer;
        private Queue<double> _buffer = new Queue<double>();

        private Image __frameImage = null;
        private Image _frameImage
        {
            get
            {
                if (__frameImage == null)
                {
                    using (var stream = new System.IO.MemoryStream(Convert.FromBase64String("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")))
                    {
                        __frameImage = Image.FromStream(stream);
                    }
                }
                return __frameImage;
            }
        }

        private int _bufferSize = 24;
        public int BufferSize
        {
            get { return _bufferSize; }
            set
            {
                _bufferSize = value;
                _timer.Interval = _bufferSize * 2;
            }
        }

        public Color ActiveColor { get; set; }
        public Color IdleColor { get; set; }

        public bool Active { get; set; }

        [DefaultValue(false)]
        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (DesignMode == false)
                    _timer.Enabled = value;
                else
                    _timer.Enabled = false;
            }
        }


        public FancyIndicator()
        {
            _timer = new Timer();
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var rnd = new Random();
            var F_A = Enumerable.Range(1, BufferSize / 2).ToDictionary(f => f, f => (rnd.NextDouble() - 0.5) * ((Active ? 1 : 0.1) / (BufferSize / 6.4)));
            for (int t = 0; t < BufferSize; t++)
            {
                //_buffer.Enqueue((rnd.NextDouble() - 0.5) * (Active ? 1 : 0.1));
                _buffer.Enqueue(F_A.Select(fa => fa.Value * Sin(2 * PI * fa.Key * ((double)t / BufferSize))).Sum());
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var points = GetPoints((Width - BufferSize) / 2);
            e.Graphics.DrawImage(_frameImage, ContentRectangle);
            if (points.Any())
            {
                var color = (Active ? ActiveColor : IdleColor);
                RectangleF rect = ContentRectangle;
                var h = rect.Height;
                rect.Inflate(0, 0.8f * h);
                rect.Offset(0, -0.1f * h);
                e.Graphics.DrawLines(new Pen(new LinearGradientBrush(ContentRectangle, ControlPaint.Light(color), ControlPaint.Dark(color), LinearGradientMode.Vertical), 1.59f), points.ToArray());
            }
            //else
            //{
            //    e.Graphics.DrawEllipse(new Pen(new SolidBrush(ForeColor)), ContentRectangle);
            //}
        }

        private PointF[] GetPoints(int offset)
        {
            var points = new List<PointF>();
            var x = 0;
            var N = _buffer.Count();
            while (_buffer.Any())
            {
                var h = 0.4f * Height;
                points.Add(new PointF(x + offset, (float)(_buffer.Dequeue() * Coefficient(x, N) * h) + h));
                x++;
            }
            return points.ToArray();
        }
        private static double Coefficient(int n, int N)
        {
            return 0.5 * (1 - Cos((2 * PI * n) / (N - 1)));
        }
    }
}

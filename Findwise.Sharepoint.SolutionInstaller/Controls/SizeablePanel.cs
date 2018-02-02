using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class SizeablePanel : Panel
    {
        #region WinForms Designer
        private Splitter splitter1;
        private GroupBox groupBox1;
        private Panel panel1;

        private void InitializeComponent()
        {
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            this.splitter1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter1_MouseDown);
            this.splitter1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter1_MouseMove);
            this.splitter1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.ResumeLayout(false);

        }
        #endregion

        private bool _dragging = false;
        private Point _startingPoint = Point.Empty;
        private Size _startingSize = Size.Empty;

        private DockStyle _gripPosition = DockStyle.Left;
        public DockStyle GripPosition
        {
            get { return _gripPosition; }
            set
            {
                if (new[] { DockStyle.Fill, DockStyle.None }.Contains(value)) throw new ArgumentException("Selected value is not valid for this property.");
                _gripPosition = value;
                splitter1.Dock = _gripPosition;
            }
        }

        public string Caption { get => groupBox1.Text; set => groupBox1.Text = value; }

        public new ControlCollection Controls => panel1.Controls;

        public SizeablePanel()
        {
            InitializeComponent();
            groupBox1.Text = Name;
            base.Controls.Add(groupBox1);
            base.Controls.Add(splitter1);
            groupBox1.Controls.Add(panel1);
            //ControlAdded += (s_, e_) => panel1.Controls.Add(e_.Control);
        }

        private void splitter1_MouseDown(object sender, MouseEventArgs e)
        {
            _startingPoint = MousePosition;
            _startingSize = Size;
            _dragging = true;
        }

        private void splitter1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void splitter1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                switch (GripPosition)
                {
                    case DockStyle.Top:
                        Height = _startingSize.Height - (MousePosition.Y - _startingPoint.Y);
                        break;
                    case DockStyle.Bottom:
                        break;
                    case DockStyle.Left:
                        Width = _startingSize.Width - (MousePosition.X - _startingPoint.X);
                        break;
                    case DockStyle.Right:
                        Width = _startingSize.Width + (MousePosition.X - _startingPoint.X);
                        break;
                }
            }
        }
    }
}

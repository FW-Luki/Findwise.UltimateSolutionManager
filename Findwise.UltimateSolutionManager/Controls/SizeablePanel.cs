using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.SolutionManager.Controls
{
    public class SizeablePanel : Panel
    {
        #region WinForms Designer
        private Splitter splitter1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private NoFocusButton CollapseButton;
        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private Panel panel1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CollapseButton = new Findwise.SolutionManager.Controls.NoFocusButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 100);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CollapseButton
            // 
            this.CollapseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CollapseButton.Location = new System.Drawing.Point(0, 0);
            this.CollapseButton.Name = "CollapseButton";
            this.CollapseButton.Size = new System.Drawing.Size(21, 21);
            this.CollapseButton.TabIndex = 0;
            this.CollapseButton.Text = "📌";
            this.CollapseButton.UseVisualStyleBackColor = true;
            this.CollapseButton.Click += new System.EventHandler(this.CollapseButton_Click);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private const string WhitePin = "📌";
        private const string BlackPin = "🖈";

        private int TabMargin => DefaultMargin.Top;

        private bool _dragging = false;
        private Point _startingPoint = Point.Empty;
        private Size _startingSize = Size.Empty;
        private Size _desiredSize = Size.Empty;

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

        public string Caption
        {
            get { return tabPage1.Text; }
            set
            {
                tabPage1.Text = value;
                SetCollapseButtonTooltip();
            }
        }

        public string CollapseButtonCaption { get; set; } = "Collapse";
        public string ExpandButtonCaption { get; set; } = "Expand";

        private bool _collapsed;
        public bool Collapsed
        {
            get { return _collapsed; }
            set
            {
                _collapsed = value;
                if (_desiredSize == Size.Empty && value) _desiredSize = Size;
                switch (GripPosition)
                {
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        Height = _collapsed ? tabControl1.ItemSize.Height + 2 * TabMargin : _desiredSize.Height;
                        break;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        Width = _collapsed ? tabControl1.ItemSize.Height + 2 * TabMargin : _desiredSize.Width;
                        tabControl1.Alignment = _collapsed ? GripPosition == DockStyle.Right ? TabAlignment.Right : TabAlignment.Left : TabAlignment.Top;
                        //Padding = new Padding(Padding.Left, _collapsed ? tabControl1.ItemSize.Height + 2 * TabMargin : 3, Padding.Right, Padding.Bottom);
                        break;
                }
                splitter1.Visible = !_collapsed;
                SetCollapseButtonTooltip();
                CollapsedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public new ControlCollection Controls => panel1.Controls;

        public event EventHandler CollapsedChanged;

        public SizeablePanel()
        {
            InitializeComponent();
            tabPage1.Text = Name;
            base.Controls.Add(tabControl1);
            base.Controls.Add(splitter1);
            base.Controls.Add(CollapseButton);            
            tabPage1.Controls.Add(panel1);
            //ControlAdded += (s_, e_) => panel1.Controls.Add(e_.Control);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            var size = tabControl1.Alignment == TabAlignment.Top ? tabControl1.ItemSize.Height : tabControl1.ItemSize.Width;
            CollapseButton.Size = new Size(size + TabMargin, size + TabMargin);
            CollapseButton.Location = new Point(tabControl1.Right - CollapseButton.Width - 1, Collapsed && (tabControl1.Alignment == TabAlignment.Left || tabControl1.Alignment == TabAlignment.Right) ? tabControl1.ItemSize.Height + TabMargin : tabControl1.Top - 1);
            CollapseButton.BringToFront();
        }

        private void splitter1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Collapsed) {
                _startingPoint = MousePosition;
                _startingSize = Size;
                _dragging = true;
            }
        }

        private void SetCollapseButtonTooltip()
        {
            toolTip1.SetToolTip(CollapseButton, $"{(Collapsed ? ExpandButtonCaption : CollapseButtonCaption)} {Caption} panel");
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
                _desiredSize = Size;
            }
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            Collapsed = !Collapsed;
        }
    }
}

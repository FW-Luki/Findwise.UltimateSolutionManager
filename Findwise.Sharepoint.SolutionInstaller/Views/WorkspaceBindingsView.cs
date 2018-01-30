using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using Findwise.Sharepoint.SolutionInstaller.Properties;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class WorkspaceBindingsViewDesigner : UserControl
    {
        public WorkspaceBindingsViewDesigner()
        {
            InitializeComponent();
            DataGridView1.AutoGenerateColumns = true;
            ToolStrip.Visible = false;
        }
    }

    public class WorkspaceBindingsView : WorkspaceViewBase
    {
        private readonly Image defaultImage;

        private readonly WorkspaceBindingsViewDesigner designer = new WorkspaceBindingsViewDesigner();
        protected override IDisposable DisposableDesigner => designer;


        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler AddRequested;


        public WorkspaceBindingsView()
        {
            Title = "Data Bindings";
            ToolBoxAvailable = false;
            Order = 1;

            defaultImage = new Bitmap(Resources.if_text_x_generic_15420);
            using (var g = Graphics.FromImage(defaultImage))
            {
                var size = new Size(16, 16);
                g.DrawImage(Resources.if_Lock_65762, new Rectangle(defaultImage.Width - size.Width, defaultImage.Height / 2 - size.Height / 2, size.Width, size.Height));
            }

            designer.AddToolStripButton.Click += (s_, e_) => AddRequested?.Invoke(this, EventArgs.Empty);
        }

        #region IMainView Support
        public override string SelectedObjectTitle => null;

        public override object[] SelectedObjects { get => base.SelectedObjects; set => base.SelectedObjects = value; }
        #endregion

        #region IComponentView Support
        public override Control Control => designer.TableLayoutPanel;
        #endregion

        #region IMainView Support
        public override Image Icon
        {
            get { return UseDefaultIcon ? defaultImage : base.Icon; }
            set { base.Icon = value; }
        }
        public bool UseDefaultIcon { get; set; }

        public override object DataSource
        {
            get { return base.DataSource; }
            set
            {
                base.DataSource = value;
                designer.DataGridView1.DataSource = value;
            }
        }

        public override ToolStrip ToolStrip => designer.ToolStrip;


        public override void RefreshView()
        {
            RefreshDataGridView(designer.DataGridView1);
        }
        #endregion
    }
}

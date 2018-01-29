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
    public partial class WorkspaceBindingsMainViewDesigner : UserControl
    {
        public WorkspaceBindingsMainViewDesigner()
        {
            InitializeComponent();
            DataGridView1.AutoGenerateColumns = false;
            ToolStrip.Visible = false;
        }
    }

    public class WorkspaceBindingsMainView : IComponentView, IMainView
    {
        private readonly ILog logger;

        private readonly WorkspaceBindingsMainViewDesigner designer = new WorkspaceBindingsMainViewDesigner();

        private readonly Image defaultImage;


        private const string ToolStripButtonEventsCategoryName = "Commands";

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler AddRequested;


        public WorkspaceBindingsMainView()
        {
            logger = LogManager.GetLogger(GetType());

            defaultImage = new Bitmap(Resources.if_text_x_generic_15420);
            using (var g = Graphics.FromImage(defaultImage))
            {
                var size = new Size(16, 16);
                g.DrawImage(Resources.if_Lock_65762, new Rectangle(defaultImage.Width - size.Width, defaultImage.Height / 2 - size.Height / 2, size.Width, size.Height));
            }

            designer.AddToolStripButton.Click += (s_, e_) => AddRequested?.Invoke(this, EventArgs.Empty);
        }


        #region IComponentView Support
        public Control Control => designer.TableLayoutPanel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();
        #endregion

        #region IMainView Support
        public string Title { get; set; } = "Data Bindings";

        private Image _icon;
        public Image Icon
        {
            get { return UseDefaultIcon ? defaultImage : _icon; }
            set { _icon = value; }
        }
        public bool UseDefaultIcon { get; set; }

        public object DataSource { get; set; }

        public string SelectedObjectTitle { get; }

        public object[] SelectedObjects { get; set; }

        public ToolStrip ToolStrip => designer.ToolStrip;

        public bool ToolBoxAvailable { get; set; } = false;
        public int Order { get; set; } = 1;


        public event EventHandler SelectionChanged;


        public void RefreshView()
        {
            //throw new NotImplementedException();
        }
        #endregion


        #region IComponent Support
        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    designer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MainToolboxView() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}

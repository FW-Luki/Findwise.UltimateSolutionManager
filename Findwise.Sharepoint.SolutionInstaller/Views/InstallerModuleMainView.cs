using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controllers;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class InstallerModuleMainViewDesigner : UserControl
    {
        public InstallerModuleMainViewDesigner()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            ToolStrip.Visible = false;
        }
    }


    public class InstallerModuleMainView : IComponentView, IMainView
    {
        private readonly InstallerModuleMainViewDesigner designer = new InstallerModuleMainViewDesigner();

        public Control Control => designer.TableLayoutPanel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();


        public string Title { get; set; } = "Installer Modules";

        public Image Icon { get; set; } = null;

        //#error Podepnij datasource do DatagridView
        private object _datasource;
        public object DataSource
        {
            get { return _datasource; }
            set
            {
                _datasource = value;
                designer.dataGridView1.DataSource = _datasource;
            }
        }

        public string SelectedObjectTitle => null;

        public object[] SelectedObjects { get; set; }

        public ToolStrip ToolStrip => designer.ToolStrip;

        public bool ToolBoxAvailable => true;

        public int Order { get; set; }

        public event EventHandler SelectionChanged;

        public void RefreshView()
        {
            throw new NotImplementedException();
        }


        private const string ToolStripButtonEventsCategoryName = "Commands";

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler DuplicateRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler DeleteRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler MoveUpRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler MoveDownRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler RefreshRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler ProceedRequested;


        public InstallerModuleMainView()
        {
            designer.DuplicateToolStripButton.Click += (s_, e_) => DuplicateRequested?.Invoke(this, EventArgs.Empty);
            designer.DeleteToolStripButton.Click += (s_, e_) => DeleteRequested?.Invoke(this, EventArgs.Empty);
            designer.MoveUpToolStripButton.Click += (s_, e_) => MoveUpRequested?.Invoke(this, EventArgs.Empty);
            designer.MoveDownToolStripButton.Click += (s_, e_) => MoveDownRequested?.Invoke(this, EventArgs.Empty);
            designer.RefreshToolStripButton.Click += (s_, e_) => RefreshRequested?.Invoke(this, EventArgs.Empty);
            designer.InstallAllToolStripButton.Click += (s_, e_) => ProceedRequested?.Invoke(this, EventArgs.Empty);
        }


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

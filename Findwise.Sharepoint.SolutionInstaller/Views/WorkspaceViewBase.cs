using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    public class WorkspaceViewBase : IComponentView, IMainView
    {
        protected const string ToolStripButtonEventsCategoryName = "Commands";

        protected readonly ILog Logger;


        public WorkspaceViewBase()
        {
            Logger = LogManager.GetLogger(GetType());

        }


        #region Helpers
        protected static void RefreshDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new MethodInvoker(() => RefreshDataGridView(dataGridView)));
            }
            else
            {
                dataGridView.Refresh();
            }
        }
        #endregion

        #region IComponentView Support
        public virtual Control Control => throw new NotImplementedException();
        public virtual Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();
        #endregion

        #region IMainView Support
        public string Title { get; set; }

        public virtual Image Icon { get; set; }

        public virtual object DataSource { get; set; }

        public virtual string SelectedObjectTitle => throw new NotImplementedException();

        public virtual object[] SelectedObjects { get; set; }

        public virtual ToolStrip ToolStrip => throw new NotImplementedException();

        public bool ToolBoxAvailable { get; set; } = true;
        public int Order { get; set; } = 0;


        public event EventHandler SelectionChanged;
        protected void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }


        public virtual void RefreshView()
        {
        }
        #endregion


        #region IComponent Support
        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #region IDisposable Support
        /// <summary>
        /// This throws <see cref="NotImplementedException"/> by design. If you have no control to dispose of, override this property and return null.
        /// </summary>
        protected virtual IDisposable DisposableDesigner => throw new NotImplementedException();
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    DisposableDesigner?.Dispose();
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

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
    public partial class MainToolStripViewDesigner : UserControl
    {
        public MainToolStripViewDesigner()
        {
            InitializeComponent();
        }
    }

    public class MainToolStripView : IComponentView
    {
        private MainToolStripViewDesigner designer = new MainToolStripViewDesigner();

        public Control Control => designer.PrimaryToolStrip;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();


        public MainToolStripView()
        {
            designer.NewToolStripButton.Click += (s_, e_) => Controller.GetController<ProjectManager>(Controllers).Saver.New();
            designer.OpenToolStripButton.Click += (s_, e_) => Controller.GetController<ProjectManager>(Controllers).Saver.Load();
            designer.SaveToolStripButton.Click += (s_, e_) => Controller.GetController<ProjectManager>(Controllers).Saver.Save();
            //designer.CancelToolStripButton.Click += (s_, e_) => CancellationController or sth...
        }


        private ToolStrip _mergedToolstrip = null;
        public void MergeToolStrip(ToolStrip toolStrip)
        {
            //if (toolStrip == null) throw new ArgumentNullException(nameof(toolStrip));
            UnmergeToolStrip();
            ToolStripManager.Merge(toolStrip, designer.PrimaryToolStrip);
            ToolStripManager.Merge(designer.SecondaryToolStrip, designer.PrimaryToolStrip);
            _mergedToolstrip = toolStrip;
        }
        public void UnmergeToolStrip()
        {
            if (_mergedToolstrip != null)
            {
                ToolStripManager.RevertMerge(designer.PrimaryToolStrip, designer.SecondaryToolStrip);
                ToolStripManager.RevertMerge(designer.PrimaryToolStrip, _mergedToolstrip);
            }
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
        // ~MainToolStripView() {
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

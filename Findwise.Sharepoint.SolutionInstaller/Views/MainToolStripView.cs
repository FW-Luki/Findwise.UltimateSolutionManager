using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class MainToolStripViewDesigner : UserControl
    {
        public MainToolStripViewDesigner()
        {
            InitializeComponent();
        }

        internal ToolStrip PrimaryToolStrip => toolStrip1;
        internal ToolStrip SecondaryToolStrip => toolStrip2;

        internal event EventHandler NewProjectRequested;
        internal event EventHandler LoadProjectRequested;
        internal event EventHandler SaveProjectRequested;
        internal event EventHandler CancelRequested;

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            NewProjectRequested?.Invoke(this, EventArgs.Empty);
        }
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            LoadProjectRequested?.Invoke(this, EventArgs.Empty);
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveProjectRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    public class MainToolStripView : IComponentView
    {
        private MainToolStripViewDesigner designer = new MainToolStripViewDesigner();

        public Control Control => designer.PrimaryToolStrip;

        public TableLayout Layout { get; set; } = new TableLayout();


        public event EventHandler NewProjectRequested;
        public event EventHandler LoadProjectRequested;
        public event EventHandler SaveProjectRequested;
        public event EventHandler CancelRequested;


        public MainToolStripView()
        {
            designer.NewProjectRequested += (s_, e_) => NewProjectRequested?.Invoke(this, EventArgs.Empty);
            designer.LoadProjectRequested += (s_, e_) => LoadProjectRequested?.Invoke(this, EventArgs.Empty);
            designer.SaveProjectRequested += (s_, e_) => SaveProjectRequested?.Invoke(this, EventArgs.Empty);
            designer.CancelRequested += (s_, e_) => CancelRequested?.Invoke(this, EventArgs.Empty);
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

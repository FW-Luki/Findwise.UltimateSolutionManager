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
    public partial class MainPropertyGridViewDesigner : UserControl
    {
        public MainPropertyGridViewDesigner()
        {
            InitializeComponent();
        }

        internal Panel Panel => sizeablePanel1;

        internal string Title { get => SelectedObjectToolStrip.Text; set => SelectedObjectToolStrip.Text = value; }

        internal object[] SelectedObjects { get => propertyGrid1.SelectedObjects; set => propertyGrid1.SelectedObjects = value; }
    }

    public class MainPropertyGridView : IComponentView
    {
        private MainPropertyGridViewDesigner designer = new MainPropertyGridViewDesigner();

        public Control Control => designer.Panel;

        public TableLayout Layout { get; set; } = new TableLayout();


        [Browsable(false)]
        public string SelectedObjectName { get => designer.Title; set => designer.Title = value; }

        [Browsable(false)]
        public object[] SelectedObjects { get => designer.SelectedObjects; set => designer.SelectedObjects = value; }

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

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
    public partial class MainLogViewDesigner : UserControl
    {
        public MainLogViewDesigner()
        {
            InitializeComponent();
        }

        internal Panel Panel => LogPanel;
        internal RichTextBox TextBox => richTextBox1;

        private void ClearLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }


    public class MainLogView : IComponentView
    {
        private MainLogViewDesigner designer = new MainLogViewDesigner();

        public Control Control => designer.Panel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();

        private string _loggerName;
        public string LoggerName
        {
            get { return _loggerName; }
            set
            {
                _loggerName = value;
                LogRichTextBoxAppender.Configure(value, designer.TextBox);
            }
        }

        public void PreviewKeyDown(PreviewKeyDownEventArgs pkdevent)
        {
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

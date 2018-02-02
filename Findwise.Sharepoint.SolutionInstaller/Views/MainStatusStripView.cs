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
    public partial class MainStatusStripViewDesigner : UserControl
    {
        public MainStatusStripViewDesigner()
        {
            InitializeComponent();
        }

        internal StatusStrip StatusStrip => statusStrip1;
        internal bool Active { get => Indicator.Active; set => Indicator.Active = value; }
        internal string Message { get => toolStripStatusLabel1.Text; set => toolStripStatusLabel1.Text = value; }
        internal int Value { get => toolStripProgressBar1.Value; set => toolStripProgressBar1.Value = value; }
        internal ProgressBarStyle Style { get => toolStripProgressBar1.Style; set => toolStripProgressBar1.Style = value; }
    }

    public class MainStatusStripView : IComponentView, IProgressRepresentative
    {
        private MainStatusStripViewDesigner designer = new MainStatusStripViewDesigner();

        public Control Control => designer.StatusStrip;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();

        public void SetProgress(ReportProgressEventArgs rpevent)
        {
            //if (Control.InvokeRequired)
            //{
            //    Control.BeginInvoke(new MethodInvoker(() => SetProgress(rpevent)));
            //}
            //else
            Control.BeginInvoke(new MethodInvoker(() =>
            {
                designer.Active = rpevent.Tags.HasFlag(OperationTag.Active);
                designer.Message = rpevent.Message;
                if (rpevent.Percentage == StatusName.MarqueeProgressBarStyle)
                {
                    designer.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    designer.Style = ProgressBarStyle.Blocks;
                    designer.Value = rpevent.Percentage;
                }
            }));
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

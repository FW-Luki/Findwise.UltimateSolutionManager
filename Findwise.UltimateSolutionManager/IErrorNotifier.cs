using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.SolutionManager
{
    interface IErrorNotifier
    {
        event EventHandler<ErrorOccuredEventArgs> ErrorOccured;
    }


    class ErrorOccuredEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public string CustomMessage { get; }
        public MessageBoxButtons MessageBoxButtons { get; } = MessageBoxButtons.OK;
        public MessageBoxIcon MessageBoxIcon { get; } = MessageBoxIcon.Error;

        public DialogResult Result { get; set; } = DialogResult.None;

        public ErrorOccuredEventArgs(Exception ex, string customMessage)
        {
            Exception = ex;
            CustomMessage = customMessage;
        }
        public ErrorOccuredEventArgs(Exception ex, string customMessage, MessageBoxButtons buttons, MessageBoxIcon icon)
            : this(ex, customMessage)
        {
            MessageBoxButtons = buttons;
            MessageBoxIcon = icon;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    interface ICancellable
    {
        event EventHandler<CancellationTokenRequestedEventArgs> CancellationTokenRequested;
        event EventHandler CancelRequested;
    }


    public class CancellationTokenRequestedEventArgs : EventArgs
    {
        public CancellationToken Token { get; set; }
    }
}

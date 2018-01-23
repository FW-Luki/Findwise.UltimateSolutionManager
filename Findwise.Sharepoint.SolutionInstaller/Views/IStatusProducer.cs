using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Sharepoint.SolutionInstaller.Models;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    interface IStatusProducer
    {
        event EventHandler<StatusChangedEventArgs> StatusChanged;
    }

    class StatusChangedEventArgs : EventArgs
    {
        public ApplicationStatus Status { get; }
        public StatusChangedEventArgs(ApplicationStatus status)
        {
            Status = status;
        }
    }
}

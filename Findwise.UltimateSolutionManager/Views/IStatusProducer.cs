using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionManager.Models;

namespace Findwise.SolutionManager.Views
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

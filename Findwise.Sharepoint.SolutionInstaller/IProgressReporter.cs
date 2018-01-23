using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    interface IProgressReporter
    {
        event EventHandler<ReportProgressEventArgs> ReportProgress;
    }


    public class ReportProgressEventArgs
    {
        public int Percentage { get; }
        public string Message { get; }
        public OperationTag Tags { get; }
        public ReportProgressEventArgs(int percentage, string message, OperationTag tags)
        {
            Percentage = percentage;
            Message = message;
            Tags = tags;
        }
        public ReportProgressEventArgs(int num, int qty, string message, OperationTag tags)
            : this(Math.Max(Math.Min((int)(((double)num / qty) * 100), 100), 0), message, tags)
        {
        }
    }

    [Flags]
    public enum OperationTag
    {
        None = 0,
        Active = 1 << 0,
        Cancellable = 1 << 1
    }
}

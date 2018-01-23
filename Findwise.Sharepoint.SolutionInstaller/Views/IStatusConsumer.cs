using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Sharepoint.SolutionInstaller.Models;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    interface IStatusConsumer
    {
        void SetStatus(ApplicationStatus status);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionManager.Models;

namespace Findwise.SolutionManager.Views
{
    interface IStatusConsumer
    {
        void SetStatus(ApplicationStatus status);
    }
}

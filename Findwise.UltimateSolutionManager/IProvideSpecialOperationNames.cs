using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager
{
    public interface IProvideSpecialOperationNames
    {
        string InstallOperationName { get; }
        string UninstallOperationName { get; }
    }
}

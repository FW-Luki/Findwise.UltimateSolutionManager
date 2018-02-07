using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.InstallerModule
{
    [Obsolete("It is most likely that this interface won't be used in the future.")]
    public interface ISharepointInstallerModuleConfiguration
    {
        string SearchApplicationName { get; set; }
    }
}

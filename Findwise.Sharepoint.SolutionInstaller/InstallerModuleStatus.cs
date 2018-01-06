using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public enum InstallerModuleStatus
    {
        Unknown,
        NotInstalled,
        InstallationPending,
        Installing,
        Installed,
        UninstallationPending,
        Uninstalling,
        Error
    }
}

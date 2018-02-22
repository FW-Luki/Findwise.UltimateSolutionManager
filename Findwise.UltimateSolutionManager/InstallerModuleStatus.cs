using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager
{
    /// <summary>
    /// Defines <see cref="IInstallerModule"/> statuses.
    /// </summary>
    public enum InstallerModuleStatus
    {
        /// <summary>
        /// Default state - the module is uninitialized and its state is not known.
        /// </summary>
        Unknown,

        /// <summary>
        /// State indicating that the module appears to be not installed.
        /// </summary>
        NotInstalled,

        /// <summary>
        /// State set in the <see cref="IInstallerModule.PrepareInstall"/> method. Indicates that the module is queued for install.
        /// </summary>
        InstallationPending,

        /// <summary>
        /// State intended to be set at the beginning of the <see cref="IInstallerModule.Install"/> method. Indicates that the module is being installed.
        /// </summary>
        Installing,

        /// <summary>
        /// State indicating that the module appears to be installed.
        /// </summary>
        Installed,

        /// <summary>
        /// State set in the <see cref="IInstallerModule.PrepareUninstall"/> method. Indicates that the module is queued for uninstall.
        /// </summary>
        UninstallationPending,

        /// <summary>
        /// State intended to be set at the beginning of the <see cref="IInstallerModule.Uninstall"/> method. Indicates that the module is being uninstalled.
        /// </summary>
        Uninstalling,

        /// <summary>
        /// State indicating that an error occured in one of the module operations.
        /// </summary>
        Error,

        /// <summary>
        /// State intended to be set at the beginning of the <see cref="IInstallerModule.CheckStatus"/> method. Indicates that the status of the module is being checked.
        /// </summary>
        Refreshing
    }
}

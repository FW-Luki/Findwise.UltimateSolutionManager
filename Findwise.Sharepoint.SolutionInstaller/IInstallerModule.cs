using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller
{
    /// <summary>
    /// Defines an installer module.
    /// </summary>
    public interface IInstallerModule
    {
        /// <summary>
        /// Gets teh module type name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the module name assigned by the user.
        /// </summary>
        string FriendlyName { get; set; }

        /// <summary>
        /// Gets the picture displayed in the toolbox button and on the module list.
        /// </summary>
        Image Icon { get; }


        /// <summary>
        /// Gets the module current status.
        /// </summary>
        InstallerModuleStatus Status { get; }

        /// <summary>
        /// Gets or sets the configuration of the module.
        /// </summary>
        ConfigurationBase Configuration { get; set; }


        /// <summary>
        /// Occurs after the module status is changed.
        /// </summary>
        event EventHandler StatusChanged;


        /// <summary>
        /// Performs operation that checks for current module status and updates the <see cref="Status"/> property.
        /// </summary>
        void CheckStatus();

        /// <summary>
        /// Performs install module operation.
        /// </summary>
        void Install();

        /// <summary>
        /// Performs uninstall module operation.
        /// </summary>
        void Uninstall();

        /// <summary>
        /// Performs any install module preparations.
        /// </summary>
        void PrepareInstall();

        /// <summary>
        /// Performs any uninstall module preparations.
        /// </summary>
        void PrepareUninstall();
    }
}

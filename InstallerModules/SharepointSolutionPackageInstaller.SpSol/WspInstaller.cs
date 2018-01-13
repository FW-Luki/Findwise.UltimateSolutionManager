using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.Sharepoint.SolutionInstaller;
using SharepointSolutionPackageInstaller.Properties;
using Findwise.InstallerModule;

namespace SharepointSolutionPackageInstaller
{
    public class WspInstaller : InstallerModuleBase
    {
        public override string Name => "Sharepoint Solution Package Installer";

        public override Image Icon => Resources.if_package_x_generic_15417;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Unknown;
        }

        public override void Install()
        {
            throw new NotImplementedException();
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}

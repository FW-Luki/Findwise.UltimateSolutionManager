using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharepointSolutionPackageInstaller.Properties;

namespace SharepointSolutionPackageInstaller
{
    public class BcsConnectorInstaller : WspInstaller
    {
        public override string Name => "BCS Connector Installer";

        public override Image Icon => Resources.if_network_wired_15388;

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }
    }
}

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
using log4net;

namespace SharepointSolutionPackageInstaller
{
    public class WspInstaller : InstallerModuleBase, ISaveLoadAware
    {
        private readonly ILog logger;
        public WspInstaller()
        {
            logger = LogManager.GetLogger(GetType());
        }


        public override string Name => "Sharepoint Solution Package Installer";

        public override Image Icon => Resources.if_package_x_generic_15417;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            System.Threading.Thread.Sleep(1024);
            Status = _testStatus;
        }

        [Obsolete("For test purposes only!")]
        private InstallerModuleStatus _testStatus = InstallerModuleStatus.NotInstalled;

        public override void Install()
        {
            Status = InstallerModuleStatus.Installing;
            System.Threading.Thread.Sleep(4096);
            _testStatus = InstallerModuleStatus.Installed;
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }


        public virtual void BeforeSave()
        {
            //ToDo: Here building and publishing project.
            
        }

        public virtual void AfterSave()
        {
        }

        public virtual void AfterLoad()
        {
        }
    }
}

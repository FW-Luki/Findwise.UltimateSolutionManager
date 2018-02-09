using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.InstallerModule;
using PowershellScriptExecutor.Properties;

namespace PowershellScriptExecutor
{
    [Category(ModuleCategoryNames.Administration)]
    public class PowershellScriptExecutor : InstallerModuleBase
    {
        public override string Name => "PowerShell Script Executor";

        public override Image Icon => Resources.if_utilities_terminal_15359;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
        }

        public override void Install()
        {
            //using (var powershell = PowerShell.Create())
            //{
            //    powershell.Invoke
            //}
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}

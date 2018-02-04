using Findwise.InstallerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactRulesCreator
{
    public class ImpactRulesCreator : InstallerModuleBase
    {
        public override string Name => "Impact Rules Creator";

        public override System.Drawing.Image Icon => null;

        private Configuration myConfiguration = new Configuration();
        public override Findwise.Configuration.ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            //Status = 
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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Sharepoint.SolutionInstaller;
using Findwise.Configuration;

namespace CrawlRulesCreator
{
    public class CrawlRulesCreator : InstallerModuleBase
    {
        public override string Name => "Crawl Rules Creator";

        public override Image Icon => null;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            throw new NotImplementedException();
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

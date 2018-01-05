using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCreator.Properties;
using Findwise.Configuration;
using Findwise.Sharepoint.SolutionInstaller;

namespace DatabaseCreator
{
    public class DatabaseCreator : IInstallerModule
    {
        public string Name => "Database Creator";

        public Image Icon => Resources.if_server_11124;

        public ConfigurationBase Configuration { get; set; }


        public bool IsInstalled
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}

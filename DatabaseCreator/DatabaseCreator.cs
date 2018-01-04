using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCreator.Properties;
using Findwise.Sharepoint.SolutionInstaller;

namespace DatabaseCreator
{
    public class DatabaseCreator : IInstallerModule
    {
        public string Name => throw new NotImplementedException();

        public Image Icon => Resources.if_server_11124;

        public bool IsInstalled => throw new NotImplementedException();

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

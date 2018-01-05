using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public interface IInstallerModule
    {
        string Name { get; }
        Image Icon { get; }

        bool IsInstalled { get; }

        ConfigurationBase Configuration { get; set; }

        void Install();
        void Uninstall();
    }
}

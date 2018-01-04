using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public interface IInstallerModule
    {
        string Name { get; }
        Image Icon { get; }

        bool IsInstalled { get; }



        void Install();
        void Uninstall();
    }
}

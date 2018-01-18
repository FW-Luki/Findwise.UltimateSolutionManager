using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    internal interface IView
    {
        Control Control { get; }
    }
}

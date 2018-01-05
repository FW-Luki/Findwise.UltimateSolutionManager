using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class NoFocusButton : Button
    {
        public NoFocusButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}

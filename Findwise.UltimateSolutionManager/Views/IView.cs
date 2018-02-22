using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.SolutionManager.Controllers;

namespace Findwise.SolutionManager.Views
{
    public interface IView
    {
        Control Control { get; }
        Controller[] Controllers { set; }

        void PreviewKeyDown(PreviewKeyDownEventArgs pkdevent);
    }
}

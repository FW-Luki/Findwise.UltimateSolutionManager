using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Views
{
    interface IMainViewContainer
    {
        IMainView CurrentView { get; }
        event EventHandler ViewChanged;
        void RefreshAllViews();
    }
}

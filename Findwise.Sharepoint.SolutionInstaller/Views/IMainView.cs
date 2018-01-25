using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    public interface IMainView : IView
    {
        string Title { get; set; }
        Image Icon { get; set; }
        object DataSource { get; set; }
        string SelectedObjectTitle { get; }
        object[] SelectedObjects { get; set; }
        ToolStrip ToolStrip { get; }
        bool ToolBoxAvailable { get; set; }

        int Order { get; set; }

        event EventHandler SelectionChanged;

        void RefreshView();

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    internal interface IMainView : IView, IReportProgress
    {
        string Title { get; }
        Image Icon { get; }
        object DataSource { get; set; }
        string SelectedObjectTitle { get; }
        object[] SelectedObjects { get; set; }
        ToolStrip ToolStrip { get; }
        bool ToolBoxAvailable { get; }

        event EventHandler SelectionChanged;

        void RefreshView();
    }
}

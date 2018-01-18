using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    public partial class MasterPropertiesView : UserControl, IMainView
    {
        public MasterPropertiesView()
        {
            InitializeComponent();
        }

        public string Title => GetType().Name;
        public Image Icon => null;

        public object DataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string SelectedObjectTitle => throw new NotImplementedException();

        public object[] SelectedObjects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ToolStrip ToolStrip => null;

        public Control Control => this;

        public bool ToolBoxAvailable => false;

        public event EventHandler SelectionChanged;
        public event EventHandler<ReportProgressEventArgs> ReportProgress;

        public void RefreshView()
        {
            throw new NotImplementedException();
        }
    }
}

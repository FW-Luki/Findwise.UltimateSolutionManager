using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using Findwise.Sharepoint.SolutionInstaller.Views;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public partial class ControllerForm : Form
    {
        public ControllerForm()
        {
            InitializeComponent();
        }
        private void ControllerForm_Load(object sender, EventArgs e)
        {
            LayoutViews();
            SetupProgressReporting();
            SetupErrorNotifying();

            SetupProjectManager();
        }
        private async void ControllerForm_Shown(object sender, EventArgs e)
        {
            InitViewsWithControllers();
            await LoadModules();
        }

        private IEnumerable<T> GetFields<T>()
        {
            return GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => typeof(T).IsAssignableFrom(f.FieldType)).Select(f => (T)f.GetValue(this));
        }

        private void LayoutViews()
        {
            foreach (var view in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => typeof(IComponentView).IsAssignableFrom(f.FieldType)).Select(f => (IComponentView)f.GetValue(this)))
            {
                while (tableLayoutPanel1.ColumnCount < (view.Layout.Column + 1)) //tableLayoutPanel1.ColumnCount = (view.Layout.Column + 1);
                {
                    tableLayoutPanel1.ColumnCount++;
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
                }
                while (tableLayoutPanel1.RowCount < (view.Layout.Row + 1)) //tableLayoutPanel1.RowCount = (view.Layout.Row + 1);
                {
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle());
                }
                tableLayoutPanel1.Controls.Add(view.Control, view.Layout.Column, view.Layout.Row);

                if (view.Layout.ColumnSpan > 1)
                {
                    tableLayoutPanel1.SetColumnSpan(view.Control, view.Layout.ColumnSpan);
                }
                else
                {
                    if (view.Layout.ColumnStyle != null) tableLayoutPanel1.ColumnStyles[view.Layout.Column] = view.Layout.ColumnStyle;
                }
                if (view.Layout.RowSpan > 1)
                {
                    tableLayoutPanel1.SetRowSpan(view.Control, view.Layout.RowSpan);
                }
                else
                {
                    if (view.Layout.RowStyle != null) tableLayoutPanel1.RowStyles[view.Layout.Row] = view.Layout.RowStyle;
                }
            }
        }

        private void SetupProgressReporting()
        {
            GetFields<IProgressReporter>().ToList().ForEach(c => c.ReportProgress += OnReportProgress);
        }
        private void OnReportProgress(object sender, ReportProgressEventArgs e)
        {
            Parallel.ForEach(GetFields<IProgressRepresentative>(), c => c.SetProgress(e));
        }

        private void SetupErrorNotifying()
        {
            GetFields<IErrorNotifier>().ToList().ForEach(c => c.ErrorOccured += OnErrorOccured);
        }
        private void OnErrorOccured(object sender, ErrorOccuredEventArgs e)
        {
            var result = MessageBox.Show(e.Exception.Message, e.CustomMessage, e.MessageBoxButtons, e.MessageBoxIcon);
            e.Result = result;
        }

        private void ModuleLoader1_PluginLoaded(object sender, Controllers.ModuleLoader.PluginLoadedEventArgs e)
        {
            foreach (var type in e.Types)
            {
                MainToolboxView1.AddModule(type);
            }
        }
        private void ModuleLoader1_LoadingFinished(object sender, EventArgs e)
        {
            MainToolboxView1.EndAddingModules();
        }

        private async Task LoadModules()
        {
            await ModuleLoader1.LoadModules("Modules", "*.dll");
        }

        private void InitViewsWithControllers()
        {
            //Parallel.ForEach(GetFields<ILateInit>(), c => c.Init());
            foreach (var view in GetFields<IView>())
            {
                view.Controllers = GetFields<Controller>().ToArray();
            }
        }

        private void MainToolboxView1_ModuleCreated(object sender, MainToolboxView.ModuleCreatedEventArgs e)
        {
            ProjectManager1.AddModule(e.Module);
        }

        private void SetupProjectManager()
        {
            ProjectManager1.WindowTitleBase = Text;
            DataBindings.Add(nameof(Text), ProjectManager1, nameof(ProjectManager1.WindowTitle));
        }

        private void MainTabularWorkspaceView1_ViewChanged(object sender, EventArgs e)
        {
            MainViewChanged();
        }
        private void MainTabularWorkspaceView1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            MainViewSelectedObjectsChanged();
        }
        private void MainViewChanged()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => MainViewChanged()));
            }
            else
            {
                MainToolStripView1.MergeToolStrip(MainTabularWorkspaceView1.CurrentView.ToolStrip);
                MainViewSelectedObjectsChanged();
            }
        }
        private void MainViewSelectedObjectsChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => MainViewSelectedObjectsChanged()));
            }
            else
            {
                MainPropertyGridView1.SelectedObjects = MainTabularWorkspaceView1.CurrentView.SelectedObjects;
                MainPropertyGridView1.SelectedObjectName = MainTabularWorkspaceView1.CurrentView.SelectedObjectTitle;
            }
        }

    }
}

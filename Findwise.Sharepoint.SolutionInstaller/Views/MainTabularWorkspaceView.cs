using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using Findwise.Sharepoint.SolutionInstaller.Models;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class MainTabularWorkspaceViewDesigner : UserControl
    {
        private Image _emptyImage { get; }

        public MainTabularWorkspaceViewDesigner()
        {
            InitializeComponent();
            _emptyImage = new Bitmap(imageList1.ImageSize.Width, imageList1.ImageSize.Height);
            LayoutViews();
        }
        private void LayoutViews()
        {
            foreach (var view in this.GetFields<IMainView>().OrderBy(v => v.Order))
            {
                AddTabPage(view);
            }
        }

        internal TabControl TabControl => tabControl1;

        internal IMainView SelectedView => TabControl.SelectedTab.Tag as IMainView;


        private void AddTabPage(IMainView view)
        {
            var page = new TabPage(view.Title);
            page.Controls.Add(view.Control);
            page.Tag = view;
            imageList1.Images.Add(view.Icon ?? _emptyImage);
            page.ImageIndex = imageList1.Images.Count - 1;
            tabControl1.TabPages.Add(page);
        }
    }


    public class MainTabularWorkspaceView : IComponentView, IMainViewContainer
    {
        private readonly MainTabularWorkspaceViewDesigner designer = new MainTabularWorkspaceViewDesigner();

        public Control Control => designer.TabControl;

        private Controller[] _controllers;
        public Controller[] Controllers
        {
            get { return _controllers; }
            set
            {
                _controllers = value;
                OnViewChanged();
            }
        }

        public TableLayout Layout { get; set; } = new TableLayout();

        public IMainView CurrentView => designer.SelectedView;

        private ProjectManager _projectManager;
        public ProjectManager ProjectManager
        {
            get { return _projectManager; }
            set
            {
                _projectManager = value;
                BindDataSources();
                _projectManager.PropertyChanged += (s_, e_) => BindDataSources(); ;
            }
        }


        public event EventHandler ViewChanged;
        protected void OnViewChanged()
        {
            ViewChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler SelectedObjectsChanged;


        public MainTabularWorkspaceView()
        {
            //designer.HandleCreated += (s_, e_) => OnViewChanged();
            designer.TabControl.SelectedIndexChanged += (s_, e_) => OnViewChanged();

            SetupInstallerModuleListEvents();
            SetupBindingSourceListEvents();
            SetupMasterConfigListEvents();

            SetupSelectionChangedEvents();
        }

        public void PreviewKeyDown(PreviewKeyDownEventArgs pkdevent)
        {
            if (pkdevent.KeyData == (Keys.F6))
            {
                if (designer.TabControl.SelectedIndex == designer.TabControl.TabPages.Count - 1)
                    designer.TabControl.SelectedIndex = 0;
                else
                    designer.TabControl.SelectedIndex++;
            }
            if (pkdevent.KeyData == (Keys.Shift | Keys.F6))
            {
                if (designer.TabControl.SelectedIndex == 0)
                    designer.TabControl.SelectedIndex = designer.TabControl.TabPages.Count - 1;
                else
                    designer.TabControl.SelectedIndex--;
            }
            //ToDo: Pass to sub-views.
        }


        private void SetupInstallerModuleListEvents()
        {
            designer.InstallerModuleMainView1.DuplicateRequested += (s_, e_) => ProjectManager.DuplicateModule();
            designer.InstallerModuleMainView1.DeleteRequested += (s_, e_) => ProjectManager.DeleteModules(GetSelectedModules());

            designer.InstallerModuleMainView1.MoveUpRequested += (s_, e_) =>
            {
                var modules = GetSelectedModules();
                ProjectManager.MoveUpModules(modules);
                designer.InstallerModuleMainView1.SelectedObjects = modules.ToArray();
            };
            designer.InstallerModuleMainView1.MoveDownRequested += (s_, e_) =>
            {
                var modules = GetSelectedModules();
                ProjectManager.MoveDownModules(modules);
                designer.InstallerModuleMainView1.SelectedObjects = modules.ToArray();
            };

            designer.InstallerModuleMainView1.RefreshRequested += async (s_, e_) =>
            {
                await ProjectManager.RefreshModuleStatus();
            };
            designer.InstallerModuleMainView1.ProceedRequested += async (s_, e_) =>
            {
                await ProjectManager.InstallAllModules();
            };
        }
        private void SetupBindingSourceListEvents()
        {
            designer.BindingsMainView1.AddRequested += (s_, e_) => ProjectManager.AddDataBindingSource();
            designer.BindingsMainView1.DeleteRequested+= (s_, e_) => ProjectManager.DeleteDataBindingSource(GetSelectedBindingItems());
        }
        private void SetupMasterConfigListEvents()
        {
            designer.MasterConfigMainView1.AddRequested += (s_, e_) => ProjectManager.AddMasterConfig();
            designer.MasterConfigMainView1.DeleteRequested += (s_, e_) => ProjectManager.DeleteMasterConfig(GetSelectedMasterConfigs());
        }

        private void SetupSelectionChangedEvents()
        {
            designer.GetFields<IMainView>().ToList().ForEach(v => v.SelectionChanged += (s_, e_) => SelectedObjectsChanged?.Invoke(this, EventArgs.Empty));
        }


        public void RefreshAllViews()
        {
            designer.GetFields<IMainView>().ToList().ForEach(v => v.RefreshView());
        }


        private IEnumerable<IInstallerModule> GetSelectedModules()
        {
            return designer.InstallerModuleMainView1.SelectedObjects?.OfType<IInstallerModule>() ?? Enumerable.Empty<IInstallerModule>();
        }
        private IEnumerable<BindingItem> GetSelectedBindingItems()
        {
            return designer.BindingsMainView1.SelectedObjects?.OfType<BindingItem>() ?? Enumerable.Empty<BindingItem>();
        }
        private IEnumerable<MasterConfig> GetSelectedMasterConfigs()
        {
            return designer.MasterConfigMainView1.SelectedObjects?.OfType<MasterConfig>() ?? Enumerable.Empty<MasterConfig>();
        }

        private void BindDataSources()
        {
            designer.InstallerModuleMainView1.DataSource = _projectManager.Project.ModuleList;
            designer.BindingsMainView1.DataSource = _projectManager.Project.BindingSourceList;
            designer.MasterConfigMainView1.DataSource = _projectManager.Project.MasterConfigurationList;
        }


        #region IComponent Support
        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    designer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MainToolboxView() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}

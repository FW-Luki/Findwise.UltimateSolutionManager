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
            foreach (var view in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => typeof(IMainView).IsAssignableFrom(f.FieldType)).Select(f => (IMainView)f.GetValue(this)).OrderBy(v => v.Order))
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


    public class MainTabularWorkspaceView : IComponentView, ILateInit
    {
        private readonly MainTabularWorkspaceViewDesigner designer = new MainTabularWorkspaceViewDesigner();

        public Control Control => designer.TabControl;
        public TableLayout Layout { get; set; } = new TableLayout();

        public IMainView CurrentView => designer.SelectedView;

        private ProjectManager _projectManager;
        public ProjectManager ProjectManager
        {
            get { return _projectManager; }
            set
            {
                _projectManager = value;
                designer.InstallerModuleMainView1.DataSource = _projectManager.Project.ModuleList;
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
            designer.HandleCreated += (s_, e_) => OnViewChanged();
            designer.TabControl.SelectedIndexChanged += (s_, e_) => OnViewChanged();

            designer.InstallerModuleMainView1.DuplicateRequested += (s_, e_) => ProjectManager.DuplicateModule();
            designer.InstallerModuleMainView1.DeleteRequested += (s_, e_) => ProjectManager.DeleteModule(GetSelectedModules());
            designer.InstallerModuleMainView1.MoveUpRequested += (s_, e_) => ProjectManager.MoveUpModules(GetSelectedModules());
            designer.InstallerModuleMainView1.MoveDownRequested += (s_, e_) => ProjectManager.MoveDownModules(GetSelectedModules());
            designer.InstallerModuleMainView1.RefreshRequested += (s_, e_) => ProjectManager.RefreshModuleStatus();
            designer.InstallerModuleMainView1.ProceedRequested += (s_, e_) => ProjectManager.InstallAllModules();
        }


        public void Init()
        {
            OnViewChanged();
        }


        private IEnumerable<IInstallerModule> GetSelectedModules()
        {
            return designer.InstallerModuleMainView1.SelectedObjects?.OfType<IInstallerModule>() ?? Enumerable.Empty<IInstallerModule>();
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

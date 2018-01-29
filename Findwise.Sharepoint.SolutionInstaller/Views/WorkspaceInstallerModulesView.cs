using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using Findwise.Sharepoint.SolutionInstaller.Controls;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class WorkspaceInstallerModulesViewDesigner : UserControl
    {
        public WorkspaceInstallerModulesViewDesigner()
        {
            InitializeComponent();
            DataGridView1.AutoGenerateColumns = false;
            ToolStrip.Visible = false;
        }
    }


    public class WorkspaceInstallerModulesView : IComponentView, IMainView
    {
        private readonly ILog logger;

        private readonly WorkspaceInstallerModulesViewDesigner designer = new WorkspaceInstallerModulesViewDesigner();

        public Control Control => designer.TableLayoutPanel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();


        public string Title { get; set; } = "Installer Modules";

        public Image Icon { get; set; } = null;

        private object _datasource;
        public object DataSource
        {
            get { return _datasource; }
            set
            {
                _datasource = value;
                designer.DataGridView1.DataSource = _datasource;
            }
        }

        private IEnumerable<object> _selectedObjects => designer.DataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem);
        private IEnumerable<IInstallerModule> _selectedModules => _selectedObjects.OfType<IInstallerModule>().Where(m => m != null);

        public string SelectedObjectTitle
        {
            get
            {
                var count = _selectedModules.Count();
                Type type = null;
                return
                    count > 0 ?
                        count > 1 ?
                            _selectedModules.All(m =>
                            {
                                var t = m.GetType();
                                if (type == null) type = t;
                                return t == type;
                            }) ?
                                $"{_selectedModules.First().Name} [{_selectedModules.Count()}]" :
                                $"{typeof(IInstallerModule).Name}[{_selectedModules.Count()}]" :
                            _selectedModules.First().FriendlyName ?? _selectedModules.First().Name :
                        string.Empty;
            }
        }

        public object[] SelectedObjects
        {
            get
            {
                return _selectedObjects.ToArray();
            }
            set
            {
                designer.DataGridView1.ClearSelection();
                designer.DataGridView1.Rows.Cast<DataGridViewRow>().Where(r => value.Contains(r.DataBoundItem)).ToList().ForEach(r => r.Selected = true);
            }
        }

        public ToolStrip ToolStrip => designer.ToolStrip;

        public bool ToolBoxAvailable { get; set; } = true;

        public int Order { get; set; }

        public event EventHandler SelectionChanged;

        public void RefreshView()
        {
            var dgv = designer.DataGridView1;
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new MethodInvoker(() => RefreshView()));
            }
            else
            {
                dgv.Refresh();
            }
        }


        private const string ToolStripButtonEventsCategoryName = "Commands";

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler DuplicateRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler DeleteRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler MoveUpRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler MoveDownRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler RefreshRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler ProceedRequested;


        public WorkspaceInstallerModulesView()
        {
            logger = LogManager.GetLogger(GetType());

            designer.DuplicateToolStripButton.Click += (s_, e_) => DuplicateRequested?.Invoke(this, EventArgs.Empty);
            designer.DeleteToolStripButton.Click += (s_, e_) => DeleteRequested?.Invoke(this, EventArgs.Empty);
            designer.MoveUpToolStripButton.Click += (s_, e_) => MoveUpRequested?.Invoke(this, EventArgs.Empty);
            designer.MoveDownToolStripButton.Click += (s_, e_) => MoveDownRequested?.Invoke(this, EventArgs.Empty);
            designer.RefreshToolStripButton.Click += (s_, e_) => RefreshRequested?.Invoke(this, EventArgs.Empty);
            designer.InstallAllToolStripButton.Click += (s_, e_) => ProceedRequested?.Invoke(this, EventArgs.Empty);

            designer.DataGridView1.SelectionChanged += DataGridView_SelectionChanged;
            designer.DataGridView1.CellPainting += DataGridView_CellPainting;
            designer.DataGridView1.CellContentClick += DataGridView_CellContentClick;
            designer.SingleClickInstallButtonTimer.Tick += SingleClickInstallButtonTimer_Tick;
            designer.DataGridView1.CellContentDoubleClick += DataGridView_CellContentDoubleClick;
        }


        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cell = designer.DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.ColumnIndex == designer.NumberColumn.Index)
                {
                    var value = e.RowIndex + 1;
                    if (!(cell.Value is int cellValue) || cellValue != value)
                    {
                        cell.Value = value;
                    }
                }
                if (e.ColumnIndex == designer.InstallColumn.Index)
                {
                    string value = string.Empty;
                    Action[] actions = null;
                    var module = (cell.OwningRow.DataBoundItem as IInstallerModule);
                    switch (module?.Status ?? InstallerModuleStatus.Unknown)
                    {
                        case InstallerModuleStatus.NotInstalled:
                            value = "Install";
                            actions = new Action[] { module.PrepareInstall, module.Install, module.CheckStatus };
                            break;
                        case InstallerModuleStatus.Installed:
                            value = "Uninstall";
                            actions = new Action[] { module.PrepareUninstall, module.Uninstall, module.CheckStatus };
                            break;
                    }
                    cell.Value = value;
                    cell.Tag = actions;
                    if (cell is DataGridViewHideableButtonCell hidable) hidable.Hidden = !cell.OwningRow.Selected;
                }
            }
        }

        private bool _installButtonSingleClicked;
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == designer.InstallColumn.Index)
            {
                if (designer.DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag is Action[] actions)
                {
                    if (!_installButtonSingleClicked)
                    {
                        SetSingleClickInstallButtonTimer();
                    }
                    else
                    {
                        ResetSingleClickInstallButtonTimer();
                        var rect = designer.DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        rect.Offset(designer.DataGridView1.Location);
                        designer.SingleClickInstallButtonToolTip.ToolTipTitle = $"Double click the {designer.DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value} button to perform the action.";
                        designer.SingleClickInstallButtonToolTip.Show("This prevents from accidental activations.", designer.DataGridView1.Parent, new Point(rect.Left, rect.Bottom), 1500);
                    }
                }
            }
            else
            {
                ResetSingleClickInstallButtonTimer();
            }
        }
        private void SingleClickInstallButtonTimer_Tick(object sender, EventArgs e)
        {
            ResetSingleClickInstallButtonTimer();
        }
        private void SetSingleClickInstallButtonTimer()
        {
            _installButtonSingleClicked = true;
            designer.SingleClickInstallButtonTimer.Start();
        }
        private void ResetSingleClickInstallButtonTimer()
        {
            _installButtonSingleClicked = false;
            designer.SingleClickInstallButtonTimer.Stop();
        }

        private async void DataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (designer.DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag is Action[] actions)
            {
                ResetSingleClickInstallButtonTimer();
                await Task.Run(() =>
                {
                    foreach (var action in actions)
                    {
                        try
                        {
                            logger.Info($"Invoking action {action.Method.Name} for module {(designer.DataGridView1.Rows[e.RowIndex].DataBoundItem as IInstallerModule)?.FriendlyName}...");
                            action.Invoke();
                        }
                        catch (Exception ex)
                        {
                            logger.Info($"Error invoking action {action.Method.Name} for module {(designer.DataGridView1.Rows[e.RowIndex].DataBoundItem as IInstallerModule)?.FriendlyName} - [{ex.Message}]");
                        }
                    }
                });
            }
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

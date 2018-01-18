using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controls;
using log4net;
using System.Threading;
using Findwise.Sharepoint.SolutionInstaller.Properties;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    public partial class InstallerModuleView : UserControl, IMainView, ICancellable
    {
        #region Fields
        private readonly ILog logger;
        #endregion


        #region Properties
        private IEnumerable<IInstallerModule> SelectedModules => dataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => (r.DataBoundItem as IInstallerModule)).Where(m => m != null);

        public Control Control => this;

        public string Title => "Installer Modules";
        public Image Icon => null;


        private BindingList<IInstallerModule> InstallerModules;
        public object DataSource
        {
            get { return InstallerModules; }
            set
            {
                InstallerModules = value as BindingList<IInstallerModule>;
                dataGridView1.DataSource = InstallerModules;
            }
        }

        public string SelectedObjectTitle
        {
            get
            {
                var count = SelectedModules.Count();
                Type type = null;
                return
                    count > 0 ?
                        count > 1 ?
                            SelectedModules.All(m =>
                            {
                                var t = m.GetType();
                                if (type == null) type = t;
                                return t == type;
                            }) ?
                                $"{SelectedModules.First().Name} [{SelectedModules.Count()}]" :
                                $"{typeof(IInstallerModule).Name}[{SelectedModules.Count()}]" :
                            SelectedModules.First().FriendlyName ?? SelectedModules.First().Name :
                        string.Empty;
            }
        }

        public object[] SelectedObjects
        {
            get => SelectedModules.Select(m => m.Configuration).Where(c => c != null).ToArray();
            set => dataGridView1.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Selected = value.Contains(r.DataBoundItem));
        }

        public ToolStrip ToolStrip => toolStrip1;

        public bool ToolBoxAvailable => true;
        #endregion


        #region Events
        public event EventHandler SelectionChanged;
        public event EventHandler<ReportProgressEventArgs> ReportProgress;
        public event EventHandler<CancellationTokenRequestedEventArgs> CancellationTokenRequested;
        public event EventHandler CancelRequested;
        #endregion


        #region Constructor
        public InstallerModuleView()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            logger = LogManager.GetLogger(GetType());
        }
        #endregion

        #region Methods
        /// <summary>
        /// Refreshes the view with the consideration of the data source.
        /// </summary>
        public void RefreshView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => dataGridView1.Refresh()));
            }
            else
            {
                dataGridView1.Refresh();
            }
        }

        public async Task CheckModuleStatuses()
        {
            await RefreshModuleList();
        }
        #endregion


        #region DataGridView code
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.ColumnIndex == NumberColumn.Index)
                {
                    var value = e.RowIndex + 1;
                    if (!(cell.Value is int cellValue) || cellValue != value)
                    {
                        cell.Value = value;
                    }
                }
                if (e.ColumnIndex == InstallColumn.Index)
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == InstallColumn.Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag is Action[] actions)
                {
                    if (!_installButtonSingleClicked)
                    {
                        SetSingleClickInstallButtonTimer();
                    }
                    else
                    {
                        ResetSingleClickInstallButtonTimer();
                        var rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        rect.Offset(dataGridView1.Location);
                        SingleClickInstallButtonToolTip.ToolTipTitle = $"Double click the {dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value} button to perform the action.";
                        SingleClickInstallButtonToolTip.Show("This prevents from accidental activations.", Parent, new Point(rect.Left, rect.Bottom), 1138);
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
            SingleClickInstallButtonTimer.Start();
        }
        private void ResetSingleClickInstallButtonTimer()
        {
            _installButtonSingleClicked = false;
            SingleClickInstallButtonTimer.Stop();
        }

        private async void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == InstallColumn.Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag is Action[] actions)
                {
                    ResetSingleClickInstallButtonTimer();
                    await Task.Run(() =>
                    {
                        foreach (var action in actions)
                        {
                            try
                            {
                                logger.Info($"Invoking action {action.Method.Name} for module {(dataGridView1.Rows[e.RowIndex].DataBoundItem as IInstallerModule)?.FriendlyName}...");
                                action.Invoke();
                            }
                            catch (Exception ex)
                            {
                                logger.Info($"Error invoking action {action.Method.Name} for module {(dataGridView1.Rows[e.RowIndex].DataBoundItem as IInstallerModule)?.FriendlyName} - [{ex.Message}]");
                            }
                        }
                    });
                }
            }
        }
        #endregion

        #region ToolStripButtons code
        private void DuplicateToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedModules.ToList().ForEach(m => InstallerModules.Remove(m));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error removing item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MoveUpToolStripButton_Click(object sender, EventArgs e)
        {
            var selected = SelectedModules.Reverse().ToList();
            if (InstallerModules.IndexOf(selected.FirstOrDefault()) > 0)
            {
                selected.ForEach(m =>
                {
                    var idx = InstallerModules.IndexOf(m);
                    InstallerModules.Remove(m);
                    InstallerModules.Insert(idx - 1, m);
                });
                dataGridView1.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Selected = selected.Contains(r.DataBoundItem as IInstallerModule));
            }
        }

        private void MoveDownToolStripButton_Click(object sender, EventArgs e)
        {
            var selected = SelectedModules.ToList();
            if (InstallerModules.IndexOf(selected.FirstOrDefault()) < InstallerModules.Count - 1)
            {
                selected.ForEach(m =>
                {
                    var idx = InstallerModules.IndexOf(m);
                    InstallerModules.Remove(m);
                    InstallerModules.Insert(idx + 1, m);
                });
                dataGridView1.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Selected = selected.Contains(r.DataBoundItem as IInstallerModule));
            }
        }

        private async void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            await RefreshModuleList();
        }

        private void InstallAllToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            CancelToolStripButton.Enabled = false;
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Code
        private CancellationToken GetCancellationToken()
        {
            var e = new CancellationTokenRequestedEventArgs();
            CancellationTokenRequested?.Invoke(this, e);
            return e.Token;
        }

        private void SetStatus(bool active, int num, int qty, string message)
        {
            SetStatus(active, Math.Max(Math.Min((int)(((double)num / qty) * 100), 100), 0), message);
        }
        private void SetStatus(bool active, int percentage, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetStatus(active, percentage, message)));
            }
            else
            {
                if (active)
                    ReportProgress?.Invoke(this, new ReportProgressEventArgs(percentage, message, OperationTrait.Active,
                                                                                                  OperationTrait.NoSetProjectAllowed,
                                                                                                  OperationTrait.NoProjectItemsChangeAllowed,
                                                                                                  OperationTrait.NoModuleOperationsAllowed,
                                                                                                  OperationTrait.Cancellable));
                else
                    ReportProgress?.Invoke(this, new ReportProgressEventArgs(percentage, message, OperationTrait.Inactive));
            }
        }

        private async Task RefreshModuleList(bool selectedOnly = false, bool throwIfCancelled = false)
        {
            await Task.Run(() =>
            {
                var options = new ParallelOptions()
                {
                    CancellationToken = GetCancellationToken()
                };
                try
                {
                    System.Collections.IEnumerable rows;
                    if (selectedOnly) rows = dataGridView1.SelectedRows;
                    else rows = dataGridView1.Rows;
                    var currow = 0;
                    Parallel.ForEach(rows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as IInstallerModule), options, module =>
                    {
                        Interlocked.Add(ref currow, 1);
                        SetStatus(true, currow, dataGridView1.Rows.Count, "Refreshing list...");
                        try
                        {
                            options.CancellationToken.ThrowIfCancellationRequested();
                            //logger.Info($"Module {module.Name} - Checking status...");
                            if (module != null && module.Status != InstallerModuleStatus.Refreshing)
                                module.CheckStatus();
                        }
                        catch (Exception ex)
                        {
                            logger.Warn($"Error checking status of module {module.FriendlyName}.", ex);
                        }
                    });
                }
                catch (OperationCanceledException)
                {
                    if (throwIfCancelled) throw;
                }
                finally
                {
                    SetStatus(false, 0, StatusName.Idle);
                }
            });
        }

        #endregion
    }
}

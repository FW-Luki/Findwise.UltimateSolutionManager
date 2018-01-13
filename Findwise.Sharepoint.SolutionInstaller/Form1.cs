using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.PluginManager;
using Findwise.Sharepoint.SolutionInstaller.Controls;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public partial class Form1 : Form
    {
        private static readonly ILog logger = LogManager.GetLogger("MainForm");

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken GetCancellationToken()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }

        [Obsolete("Do not use this field. Please use corresponding property.")]
        private BindingList<IInstallerModule> __installerModules;
        protected internal BindingList<IInstallerModule> InstallerModules
        {
#pragma warning disable CS0618 // Type or member is obsolete
            get { return __installerModules; }
            set
            {
                __installerModules = value;
                //var bindingList = new BindingList<IInstallerModule>(__installerModules);
                //bindingList.ListChanged += (s_, e_) => dataGridView1.Update();
                //var bindingSource = new BindingSource { DataSource = __installerModules };
                //__installerModules.CollectionChanged +=(s_,e_)=>dataGridView1.data
                dataGridView1.DataSource = __installerModules;
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            LogRichTextBoxAppender.Configure("ColoredTextBox", richTextBox1);
        }

        private void Module_StatusChanged(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private async void Form1_Shown(object sender, EventArgs e)
        {
            await LoadModules();
            NewProject();
        }

        private async Task LoadModules()
        {
            await Task.Run(() =>
            {
                try
                {
                    var buttons = System.IO.Directory.GetFiles("Modules", "*.dll", System.IO.SearchOption.TopDirectoryOnly)
                        .SelectMany(filename =>
                        {
                            return AssemblyLoader.LoadClassesFromFile<IInstallerModule>(System.IO.Path.GetFullPath(filename));
                        })
                        .Where(type => type != null)
                        .Select(type =>
                        {
                            var module = (IInstallerModule)Activator.CreateInstance(type);
                            try
                            {
                                return GetToolboxButton(module.Name, module.Icon, type);
                            }
                            finally
                            {
                                (module as IDisposable)?.Dispose();
                            }
                        });
                    if (buttons.Any())
                    {
                        try
                        {
                            foreach (var button in buttons)
                            {
                                Invoke(new MethodInvoker(() =>
                                {
                                    sizeablePanel1.Controls.Add(button);
                                    sizeablePanel1.Controls.SetChildIndex(button, 0);
                                }));
                            }
                        }
                        catch (Exception ex)
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                var b = GetToolboxButton(ex.Message, null);
                                b.Enabled = false;
                                b.ForeColor = Color.Red;
                                sizeablePanel1.Controls.Add(b);
                            }));
                        }
                    }
                    else
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            var b = GetToolboxButton("No modules found", null);
                            b.Enabled = false;
                            sizeablePanel1.Controls.Add(b);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading modules", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
        private Button GetToolboxButton(string text, Image image, object tag = null)
        {
            var button = new NoFocusButton()
            {
                AutoSize = true,
                AutoEllipsis = true,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Text = text,
                Image = image,
                Tag = tag
            };
            //toolTip1.SetToolTip(button, text);
            button.Click += ToolboxButton_Click;
            return button;
        }

        private void ToolboxButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button)?.Tag is Type moduleType)
            {
                var module = (IInstallerModule)Activator.CreateInstance(moduleType);
                module.StatusChanged += Module_StatusChanged;
                InstallerModules.Add(module);
            }
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            NewProject();
        }
        private void NewProject()
        {
            InstallerModules = new BindingList<IInstallerModule>(); ;
        }

        private async void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadProject(openFileDialog1.FileName);
                    await RefreshModuleList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadProject(string filename)
        {
            var modules = Configuration.ConfigurationBase.Deserialize<Project>(System.IO.File.ReadAllText(filename)).Modules.ToList();
            modules.ForEach(m => m.StatusChanged += Module_StatusChanged);
            InstallerModules = new BindingList<IInstallerModule>(modules);
            InstallerModules.OfType<ISaveLoadAware>().ToList().ForEach(m => m.AfterLoad());
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveProject(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error saving project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SaveProject(string filename)
        {
            InstallerModules.OfType<ISaveLoadAware>().ToList().ForEach(m => m.BeforeSave());
            System.IO.File.WriteAllText(filename, Project.Create(InstallerModules).Serialize());
            InstallerModules.OfType<ISaveLoadAware>().ToList().ForEach(m => m.AfterSave());
        }


        private IEnumerable<IInstallerModule> SelectedModules => dataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as IInstallerModule).Where(m => m != null);

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
            SetActionButtonsEnabled(false);
            await RefreshModuleList();
            SetActionButtonsEnabled(true);
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
                    Parallel.ForEach(rows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as IInstallerModule), options, module =>
                    {
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
            });
        }

        private async void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            await RefreshModuleList(selectedOnly: true);
        }

        private async void InstallAllToolStripButton_Click(object sender, EventArgs e)
        {
            SetActionButtonsEnabled(false);
            await InstallAllModules();
            SetActionButtonsEnabled(true);
        }
        private async Task InstallAllModules()
        {
            try
            {
                await RefreshModuleList(throwIfCancelled: true);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            await Task.Run(() =>
            {
                try
                {
                    var options = new ParallelOptions()
                    {
                        CancellationToken = GetCancellationToken()
                    };
                    Parallel.ForEach(InstallerModules.Where(m => m.Status == InstallerModuleStatus.NotInstalled), options, module =>
                    {
                        try
                        {
                            options.CancellationToken.ThrowIfCancellationRequested();
                            module.PrepareInstall();
                        }
                        catch (Exception ex)
                        {
                            logger.Warn($"Error preparing module {module.FriendlyName} for install.", ex);
                        }
                    });
                }
                catch (OperationCanceledException)
                {
                    return;
                }

                var refreshTasks = new List<Task>();
                var token = GetCancellationToken();
                foreach (var module in InstallerModules)
                {
                    try
                    {
                        token.ThrowIfCancellationRequested();
                        module.Install();
                        refreshTasks.Add(Task.Run(() => module.CheckStatus()));
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        logger.Warn($"Error installing module {module.FriendlyName}.", ex);
                    }
                }
                Task.WaitAll(refreshTasks.ToArray());
            });
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            CancelToolStripButton.Enabled = false;
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            toolStrip1.Items.OfType<LockableToolStripButton>().ToList().ForEach(b => b.SetLocked(enabled));
            sizeablePanel1.Enabled = enabled;
            //splitContainer1.Enabled = enabled;
            groupBox2.Enabled = enabled;
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
                    if (cell is DataGridViewHideableButtonCell dgvdbc) dgvdbc.Hidden = !cell.OwningRow.Selected;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObjects = dataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => (r.DataBoundItem as IInstallerModule)?.Configuration).Where(m => m != null).ToArray();
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
                        SingleClickInstallButtonToolTip.Show("This prevents from accidental activations.", groupBox1, new Point(rect.Left, rect.Bottom), 1138);
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


        private void ClearLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

    }
}

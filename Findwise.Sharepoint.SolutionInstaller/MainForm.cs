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
using Findwise.Sharepoint.SolutionInstaller.Views;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public partial class MainForm : Form
    {
        private readonly ILog logger;

        private readonly InstallerModuleView_OLD _installerModuleListView = new InstallerModuleView_OLD();
        private readonly MasterPropertiesView _masterPropertiesView = new MasterPropertiesView();

        private readonly Project_OLD.Manager _projectManager = new Project_OLD.Manager();

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken GetCancellationToken()
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
            return _cancellationTokenSource.Token;
        }

        public MainForm()
        {
            InitializeComponent();

            propertyGrid1.MergeToolStrip(PropertyGridMergeToolStrip);
            PropertyGridMergeToolStrip.Dispose();

            _installerModuleListView.SelectionChanged += MainViews_SelectionChanged;
            _installerModuleListView.ReportProgress += MainViews_ReportProgress;
            _installerModuleListView.CancellationTokenRequested += MainViews_CancellationTokenRequested;
            _installerModuleListView.CancelRequested += MainViews_CancelRequested;
            _masterPropertiesView.SelectionChanged += MainViews_SelectionChanged;
            _masterPropertiesView.ReportProgress += MainViews_ReportProgress;
            //_masterPropertiesView.CancellationTokenRequested += MainViews_CancellationTokenRequested;
            //_masterPropertiesView.CancelRequested += MainViews_CancelRequested;
            _projectManager.ReportProgress += MainViews_ReportProgress;
            _projectManager.CancellationTokenRequested += MainViews_CancellationTokenRequested;
            _projectManager.CancelRequested += MainViews_CancelRequested;

            SetupMainViewControls(_installerModuleListView,
                                  _masterPropertiesView);

            LogRichTextBoxAppender.Configure("ColoredTextBox", richTextBox1);
            logger = LogManager.GetLogger(GetType());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _installerModuleListView.DataSource = _projectManager.InstallerModules;
            ResetStatus();
        }
        private async void Form1_Shown(object sender, EventArgs e)
        {
            _projectManager.New();
            await LoadModules();
        }


        private void SetupMainViewControls(params IMainView_OLD[] views)
        {
            tabControl1.TabPages.AddRange(views.Select(v =>
            {
                int imgidx = -1; //string imgkey = null;                
                if (v.Icon != null)
                {
                    imageList1.Images.Add(v.Icon);
                    imgidx = imageList1.Images.Count - 1; //imgkey = Guid.NewGuid().ToString();
                }
                var page = new TabPage(v.Title)
                {
                    ImageIndex = imgidx //ImageKey = imgkey
                };
                page.Controls.Add(v.Control);
                v.Control.Dock = DockStyle.Fill;
                return page;
            }).ToArray());
            tabControl1_Selected(tabControl1, new TabControlEventArgs(tabControl1.TabPages.Cast<TabPage>().FirstOrDefault(), 0, TabControlAction.Selected));
        }


        private void MainViews_SelectionChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void MainViews_ReportProgress(object sender, ReportProgressEventArgs_OLD e)
        {
            SetStatus(e.Percentage, e.Message, e.OperationTraits);
        }
        private void SetStatus(int percentage, string message, params OperationTrait[] traits)
        {
            toolStripProgressBar1.Value = percentage;
            toolStripStatusLabel1.Text = message;
            if (traits.Contains(OperationTrait.Active)) Indicator.Active = true;
            if (traits.Contains(OperationTrait.Inactive)) Indicator.Active = false;
            ToolboxPanel.Enabled = !traits.Contains(OperationTrait.NoToolBoxAllowed);
            tabControl1.Enabled = !traits.Contains(OperationTrait.NoMainViewsAllowed);
            propertyGrid1.Enabled = !traits.Contains(OperationTrait.NoPropertiesAllowed);
            toolStrip1.Items.OfType<LockableToolStripButton>().ToList().ForEach(b => b.SetLocked(!traits.Contains(b.OperationTrait)));
        }
        private void ResetStatus()
        {
            SetStatus(0, StatusName.Idle, OperationTrait.Inactive);
        }

        private void MainViews_CancellationTokenRequested(object sender, CancellationTokenRequestedEventArgs e)
        {
            e.Token = GetCancellationToken();
        }

        private void MainViews_CancelRequested(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private ToolStrip _lastSelectedToolstrip = null;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_lastSelectedToolstrip != null)
                ToolStripManager.RevertMerge(toolStrip1, _lastSelectedToolstrip);
        }
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Controls.OfType<IMainView_OLD>().FirstOrDefault() is IMainView_OLD view)
            {
                ToolboxPanel.Enabled = view.ToolBoxAvailable;

                if (view.ToolStrip is ToolStrip viewToolStrip)
                {
                    ToolStripManager.Merge(viewToolStrip, toolStrip1);
                    _lastSelectedToolstrip = viewToolStrip;
                    viewToolStrip.Visible = false;
                }
            }
        }

        private void Module_StatusChanged(object sender, EventArgs e)
        {
            _installerModuleListView.RefreshView();
        }

        private async Task LoadModules()
        {
            await Task.Run(() =>
            {
                try
                {
                    var curplug = 0;
                    var pluginFiles = System.IO.Directory.GetFiles("Modules", "*.dll", System.IO.SearchOption.TopDirectoryOnly);
                    var buttons = pluginFiles
                        .SelectMany(filename =>
                        {
                            //SetStatus(++curplug, pluginFiles.Length, "Loading plugins...");
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
                        })/*.ToArray()*/;
                    if (buttons.Any())
                    {
                        try
                        {
                            //curplug = 0;
                            foreach (var button in buttons)
                            {
                                //SetStatus(true, ++curplug, buttons.Count(), "Loading modules...");
                                Invoke(new MethodInvoker(() =>
                                {
                                    ToolboxPanel.Controls.Add(button);
                                    ToolboxPanel.Controls.SetChildIndex(button, 0);
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
                                ToolboxPanel.Controls.Add(b);
                            }));
                        }
                    }
                    else
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            var b = GetToolboxButton("No modules found", null);
                            b.Enabled = false;
                            ToolboxPanel.Controls.Add(b);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading modules", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ResetStatus();
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
                module.FriendlyName = $"{module.Name} {_projectManager.InstallerModules.Count(m => m.GetType() == module.GetType()) + 1}";
                module.StatusChanged += Module_StatusChanged;
                _projectManager.InstallerModules.Add(module);
            }
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            _projectManager.New();
        }

        private async void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _projectManager.Load(openFileDialog1.FileName);
                    await _installerModuleListView.CheckModuleStatuses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ResetStatus();
                }
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _projectManager.Save(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error saving project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ResetStatus();
                }
            }
        }


        private async void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //await RefreshModuleList(selectedOnly: true);
        }


        private void ClearLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }


        private void BindingWindowToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}

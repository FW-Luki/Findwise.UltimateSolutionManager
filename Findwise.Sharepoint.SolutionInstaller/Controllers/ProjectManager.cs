﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.PluginManager;
using Findwise.Sharepoint.SolutionInstaller.Models;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller.Controllers
{
    public class ProjectManager : Controller, IProgressReporter, INotifyPropertyChanged
    {
        #region Fileds
        private readonly ILog logger;
        #endregion


        #region Properties
        public string WindowTitleBase { get; set; }

        private const string WindowTitleFormatDefaultValue = "{0}{1} - {2}";
        [DefaultValue(WindowTitleFormatDefaultValue)]
        [Description("A Format string where {0} represents Modified project symbol, {1} represents Project name and {2} represents Application title.")]
        public string WindowTitleFormat { get; set; } = WindowTitleFormatDefaultValue;

        public string EmptyProjectName { get; set; }

        public string ProjectName => Project?.Name;

        [Browsable(false)]
        public bool IsEmpty => string.IsNullOrEmpty(ProjectName);
        [Browsable(false)]
        public bool IsModified { get; set; }

        public string WindowTitle => string.Format(WindowTitleFormat, IsModified ? "* " : "", IsEmpty ? EmptyProjectName : ProjectName, WindowTitleBase);

        [Obsolete("Don't use this field!")]
        private Project __project;
        public Project Project
        {
#pragma warning disable CS0618 // Type or member is obsolete
            get { return __project; }
            private set
            {
                __project = value;
                NotifyProjectChange();
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }
        private void NotifyProjectChange()
        {
            var propertyChangedEventHandler = PropertyChanged;
            if (propertyChangedEventHandler != null)
            {
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(nameof(Project)));
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(nameof(ProjectName)));
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(nameof(IsEmpty)));
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(nameof(WindowTitle)));
            }
        }

        public ProjectSaverHelper Saver { get; }
        #endregion


        #region Events
        public event EventHandler<ReportProgressEventArgs> ReportProgress;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ModuleStatusChanged;
        #endregion


        #region Constructor
        public ProjectManager()
        {
            logger = LogManager.GetLogger(GetType());
            Saver = new ProjectSaverHelper(this);
            NewProject();
        }
        #endregion


        #region Code Project
        public void NewProject()
        {
            Project = new Project();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Project)));
        }

        public void LoadProject(string filename)
        {
            var proj = ConfigurationBase.Deserialize<Project>(System.IO.File.ReadAllText(filename), new PluginSerializationBinder());
            proj.Name = System.IO.Path.GetFileName(filename);

            var curmod = 0;
            var loadAwareModules = proj.Modules.OfType<ISaveLoadAware>();

            foreach (var module in loadAwareModules)
            {
                ReportProgress?.Invoke(this, new ReportProgressEventArgs(++curmod, loadAwareModules.Count(), "Loading project...", OperationTag.Active | OperationTag.Cancellable));
                module.AfterLoad();
            }

            proj.Modules.ToList().ForEach(m => m.StatusChanged += Module_StatusChanged);
            Project = proj;
        }

        public void SaveProject(string filename)
        {
            var saveAwareModules = Project.Modules.OfType<ISaveLoadAware>();
            var curmod = 0;
            var allmods = saveAwareModules.Count() * 2;

            foreach (var module in saveAwareModules)
            {
                ReportProgress?.Invoke(this, new ReportProgressEventArgs(++curmod, allmods, "Saving project...", OperationTag.Active | OperationTag.Cancellable));
                module.BeforeSave();
            }

            System.IO.File.WriteAllText(filename, Project.Serialize());

            foreach (var module in Project.Modules.OfType<ISaveLoadAware>())
            {
                ReportProgress?.Invoke(this, new ReportProgressEventArgs(++curmod, allmods, "Saving project...", OperationTag.Active | OperationTag.Cancellable));
                module.AfterSave();
            }

            Project.Name = System.IO.Path.GetFileName(filename);
            NotifyProjectChange();
        }
        #endregion

        #region Code Modules
        public void AddModule(IInstallerModule module)
        {
            Project.ModuleList.Add(module);
            module.StatusChanged += Module_StatusChanged;
        }

        public void DuplicateModule()
        {
        }
        public void DeleteModules(IEnumerable<IInstallerModule> modules)
        {
            foreach (var module in modules)
            {
                Project.ModuleList.Remove(module);
                module.StatusChanged -= Module_StatusChanged;
            }
        }

        public void MoveUpModules(IEnumerable<IInstallerModule> modules)
        {
            var selected = modules.OrderBy(m => Project.ModuleList.IndexOf(m)).ToList();
            if (Project.ModuleList.IndexOf(selected.FirstOrDefault()) > 0)
            {
                selected.ForEach(m =>
                {
                    var idx = Project.ModuleList.IndexOf(m);
                    Project.ModuleList.Remove(m);
                    Project.ModuleList.Insert(idx - 1, m);
                });
            }
        }
        public void MoveDownModules(IEnumerable<IInstallerModule> modules)
        {
            var selected = modules.OrderBy(m => Project.ModuleList.IndexOf(m)).Reverse().ToList();
            if (Project.ModuleList.IndexOf(selected.FirstOrDefault()) < Project.ModuleList.Count - 1)
            {
                selected.ForEach(m =>
                {
                    var idx = Project.ModuleList.IndexOf(m);
                    Project.ModuleList.Remove(m);
                    Project.ModuleList.Insert(idx + 1, m);
                });
            }
        }

        public async Task RefreshModuleStatus(IInstallerModule singleModule = null, bool throwIfCancelled = false)
        {
            await Task.Run(() =>
            {
                var options = new ParallelOptions()
                {
                    //CancellationToken = GetCancellationToken()
                };
                try
                {
                    IEnumerable<IInstallerModule> modules;
                    if (singleModule != null) modules = new[] { singleModule };
                    else modules = Project.ModuleList;
                    var currow = 0;
                    Parallel.ForEach(modules, options, module =>
                    {
                        Interlocked.Add(ref currow, 1);
                        ReportProgress?.Invoke(this, new ReportProgressEventArgs(++currow, modules.Count(), "Refreshing list...", OperationTag.Active));
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
                    ReportProgress?.Invoke(this, new ReportProgressEventArgs(0, StatusName.Idle, OperationTag.None));
                }
            });
        }

        public async Task InstallAllModules()
        {
            try
            {
                await RefreshModuleStatus(throwIfCancelled: true);
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
                        //CancellationToken = GetCancellationToken()
                    };
                    Parallel.ForEach(Project.ModuleList.Where(m => m.Status == InstallerModuleStatus.NotInstalled), options, module =>
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
                //var token = GetCancellationToken();
                var pending = Project.ModuleList.Where(m => m.Status == InstallerModuleStatus.InstallationPending);
                var curmod = 0;
                var allmods = pending.Count();
                foreach (var module in pending)
                {
                    ReportProgress?.Invoke(this, new ReportProgressEventArgs(++curmod, allmods, $"Installing module {module.FriendlyName ?? module.Name}...", OperationTag.Active));
                    try
                    {
                        //token.ThrowIfCancellationRequested();
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
                ReportProgress?.Invoke(this, new ReportProgressEventArgs(0, StatusName.Idle, OperationTag.None));
            });
        }


        private void Module_StatusChanged(object sender, EventArgs e)
        {
            ModuleStatusChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region DataBindings
        public void AddDataBindingSource(/*BindingItem item*/)
        {
            Project.BindingSourcesList.AddNew();
        }
        #endregion

        #region Code Common
        #endregion


        #region Classes
        public class ProjectSaverHelper //ToDo: make separate controller out of this class.
        {
            private const string DefaultExtension = "xml";

            private readonly ProjectManager _manager;
            private readonly System.Windows.Forms.OpenFileDialog _openDialog = new System.Windows.Forms.OpenFileDialog()
            {
                FileName = "",
                Filter = $"XML files|*.xml|All files|*.*"
            };
            private readonly System.Windows.Forms.SaveFileDialog _saveDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Filter = $"XML files|*.xml|All files|*.*"
            };

            public ProjectSaverHelper(ProjectManager manager)
            {
                _manager = manager;
            }

            public void New()
            {
                if (AskForProjectSave("Save current project before creating new?", "New project"))
                {
                    _manager.NewProject();
                }
            }
            public void Load()
            {
                if (AskForProjectSave("Save current project before loading another?", "Load project"))
                {
                    if (_openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            _manager.LoadProject(_openDialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message, "Error loading project", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
            }
            public void Save()
            {
                DoSave();
            }

            private bool AskForProjectSave(string text, string caption)
            {
                if (_manager.IsModified)
                {
                    var r = System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question);
                    if (r == System.Windows.Forms.DialogResult.Yes)
                    {
                        var ss = DoSave();
                        if (!ss) return false;
                    }
                    if (r == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return false;
                    }
                }
                return true;
            }
            private bool DoSave()
            {
                if (_saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        _manager.SaveProject(_saveDialog.FileName);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Error loading project", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                return false;
            }
        }
        #endregion
    }
}

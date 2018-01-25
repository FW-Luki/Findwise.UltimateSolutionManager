using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.PluginManager;
using Findwise.Sharepoint.SolutionInstaller.Models;

namespace Findwise.Sharepoint.SolutionInstaller.Controllers
{
    public class ProjectManager : Controller, IProgressReporter, INotifyPropertyChanged
    {
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


        public event EventHandler<ReportProgressEventArgs> ReportProgress;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ModuleStatusChanged;

        public ProjectManager()
        {
            Saver = new ProjectSaverHelper(this);
            NewProject();
        }


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

            proj.Modules.ToList().ForEach(m => m.StatusChanged += (s_, e_) => ModuleStatusChanged?.Invoke(this, EventArgs.Empty));
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


        public void AddModule(IInstallerModule module)
        {
            Project.ModuleList.Add(module);
        }

        public void DuplicateModule()
        {
        }
        public void DeleteModules(IEnumerable<IInstallerModule> modules)
        {
            modules.ToList().ForEach(m => Project.ModuleList.Remove(m));
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
                //dataGridView1.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Selected = selected.Contains(r.DataBoundItem as IInstallerModule));
            }
        }

        public void RefreshModuleStatus(IInstallerModule singleModule = null)
        {
        }
        public void InstallAllModules()
        {
        }


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
    }
}

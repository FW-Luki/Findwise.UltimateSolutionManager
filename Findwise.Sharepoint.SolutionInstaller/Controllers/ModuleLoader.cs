using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.PluginManager;

namespace Findwise.Sharepoint.SolutionInstaller.Controllers
{
    class ModuleLoader : Controller, IProgressReporter, IErrorNotifier
    {
        public event EventHandler<PluginLoadedEventArgs> PluginLoaded;
        public class PluginLoadedEventArgs
        {
            public IEnumerable<Type> Types { get; }
            public PluginLoadedEventArgs(IEnumerable<Type> types)
            {
                Types = types;
            }
        }

        public event EventHandler LoadingFinished;
        public event EventHandler<ReportProgressEventArgs> ReportProgress;
        public event EventHandler<ErrorOccuredEventArgs> ErrorOccured;

        public async Task LoadModules(string path, string filter)
        {
            await Task.Run(() =>
            {
                try
                {
                    var curplug = 0;
                    var pluginFiles = System.IO.Directory.GetFiles(path, filter, System.IO.SearchOption.TopDirectoryOnly);
                    foreach (var filename in pluginFiles)
                    {
                        ReportProgress?.Invoke(this, new ReportProgressEventArgs(++curplug, pluginFiles.Length, "Loading plugins...", OperationTag.Active));
                        var types = AssemblyLoader.LoadClassesFromFile<IInstallerModule>(System.IO.Path.GetFullPath(filename)).Where(t => t != null);
                        if (types.Any()) PluginLoaded?.Invoke(this, new PluginLoadedEventArgs(types));
                    }
                }
                catch (Exception ex)
                {
                    ErrorOccured?.Invoke(this, new ErrorOccuredEventArgs(ex, "Error loading modules"));
                }
                finally
                {
                    ReportProgress?.Invoke(this, new ReportProgressEventArgs(0, StatusName.Idle, OperationTag.None));
                    LoadingFinished?.Invoke(this, EventArgs.Empty);
                }
            });
        }

    }
}

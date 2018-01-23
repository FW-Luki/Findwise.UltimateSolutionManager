using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.PluginManager;
using log4net;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public class Project_OLD : ConfigurationBase
    {
        public IInstallerModule[] Modules { get; set; }

        public static Project_OLD Create(IEnumerable<IInstallerModule> moduleCollection)
        {
            return new Project_OLD()
            {
                Modules = moduleCollection.ToArray()
            };
        }


        public class Manager : IReportProgress, ICancellable
        {
            private readonly ILog logger;


            public BindingList<IInstallerModule> InstallerModules { get; } = new BindingList<IInstallerModule>();


            public event EventHandler<ReportProgressEventArgs_OLD> ReportProgress;

            public event EventHandler<CancellationTokenRequestedEventArgs> CancellationTokenRequested;
            public event EventHandler CancelRequested;

            public Manager()
            {
                logger = LogManager.GetLogger(GetType());
            }


            public void New()
            {
                InstallerModules.Clear();
            }

            public void Load(string filename)
            {
                var modules = ConfigurationBase.Deserialize<Project_OLD>(System.IO.File.ReadAllText(filename), new PluginSerializationBinder()).Modules.ToList();
                //modules.ForEach(m => m.StatusChanged += Module_StatusChanged);

                InstallerModules.Clear();
                //InstallerModules.add = new BindingList<IInstallerModule>(modules);
                foreach (var module in modules)
                {
                    InstallerModules.Add(module);
                }

                var curmod = 0;
                var loadAwareModules = InstallerModules.OfType<ISaveLoadAware>();

                foreach (var module in loadAwareModules)
                {
                    SetStatus(++curmod, loadAwareModules.Count(), "Loading project...");
                    module.AfterLoad();
                }
            }

            public void Save(string filename)
            {
                var saveAwareModules = InstallerModules.OfType<ISaveLoadAware>();
                var curmod = 0;
                var allmods = saveAwareModules.Count() * 2;

                foreach (var module in saveAwareModules)
                {
                    SetStatus(++curmod, allmods, "Saving project...");
                    module.BeforeSave();
                }

                System.IO.File.WriteAllText(filename, Project_OLD.Create(InstallerModules).Serialize());

                foreach (var module in InstallerModules.OfType<ISaveLoadAware>())
                {
                    SetStatus(++curmod, allmods, "Saving project...");
                    module.AfterSave();
                }
            }

            private async Task InstallAllModules()
            {
                try
                {
                    //await RefreshModuleList(throwIfCancelled: true);
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
                    var curmod = 0;
                    foreach (var module in InstallerModules)
                    {
                        SetStatus(curmod++, InstallerModules.Count, $"Installing module {module.FriendlyName ?? module.Name}...");
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
                    ResetStatus();
                });
            }


            private CancellationToken GetCancellationToken()
            {
                var e = new CancellationTokenRequestedEventArgs();
                CancellationTokenRequested?.Invoke(this, e);
                return e.Token;
            }

            private void SetStatus(int num, int qty, string messaage)
            {
                ReportProgress?.Invoke(this, new ReportProgressEventArgs_OLD(num, qty, messaage, OperationTrait.Active,
                                                                                             OperationTrait.NoGetProjectAllowed,
                                                                                             OperationTrait.NoSetProjectAllowed));
            }
            private void ResetStatus()
            {
                ReportProgress?.Invoke(this, new ReportProgressEventArgs_OLD(0, StatusName.Idle, OperationTrait.Inactive));
            }
        }
    }
}

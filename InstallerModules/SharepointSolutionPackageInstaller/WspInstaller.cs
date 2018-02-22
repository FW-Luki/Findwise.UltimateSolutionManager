using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.Sharepoint.SolutionInstaller;
using SharepointSolutionPackageInstaller.Properties;
using Findwise.InstallerModule;
using log4net;
using Microsoft.SharePoint.Administration;
using System.IO;
using System.Diagnostics;
using Microsoft.SharePoint.BusinessData.SharedService;
using Microsoft.SharePoint;
using Microsoft.SharePoint.BusinessData.MetadataModel;
using Microsoft.SharePoint.BusinessData.Administration;

namespace SharepointSolutionPackageInstaller
{
    public class WspInstaller : InstallerModuleBase, ISaveLoadAware
    {
        private readonly ILog logger;
        public WspInstaller()
        {
            logger = LogManager.GetLogger(GetType());
        }


        public override string Name => "Sharepoint Solution Package Installer";

        public override Image Icon => Resources.if_package_x_generic_15417;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                Status = SPFarm.Local.Solutions[Path.GetFileName(myConfiguration.PackageConfiguration.WspPackage.ToLower())] != null ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        [Obsolete("For test purposes only!")]
        private InstallerModuleStatus _testStatus = InstallerModuleStatus.NotInstalled;

        public override void Install()
        {
            Status = InstallerModuleStatus.Installing;
            try
            {
                if (SPFarm.Local.Solutions[Path.GetFileName(myConfiguration.PackageConfiguration.WspPackage.ToLower())] == null) 
                {
                    SPSolution customSolution = SPFarm.Local.Solutions.Add(myConfiguration.PackageConfiguration.WspPackage); 
                    System.Threading.Thread.Sleep(4096);
                    customSolution.DeployLocal(true, false);

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var deployed = false;
                    var flags = false;
                    while (!deployed)
                    {
                        var solution = SPFarm.Local.Solutions[Path.GetFileName(myConfiguration.PackageConfiguration.WspPackage.ToLower())];
                        if (solution.Deployed && !solution.JobExists)
                        {
                            deployed = true;
                        }
                        else if (stopWatch.ElapsedMilliseconds > 51200 && !flags && !customSolution.JobExists)
                        {
                            customSolution.DeployLocal(true, false);
                            flags = true;
                        }
                        else if (stopWatch.ElapsedMilliseconds > 102400)
                        {
                            Uninstall();
                            throw new TimeoutException("Timeout");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }
        
        public override void Uninstall()
        {
            SPServiceContext serviceContext = SPServiceContext.GetContext(new SPSite(myConfiguration.SPSite));
            BdcServiceApplicationProxy bdcServiceApplicationProxy = (BdcServiceApplicationProxy)serviceContext.GetDefaultProxy(typeof(BdcServiceApplicationProxy));
            AdministrationMetadataCatalog administrationMetadataCatalog = bdcServiceApplicationProxy.GetAdministrationMetadataCatalog();
            LobSystem lobSystem = null;
            try
            {
                try
                {
                    lobSystem = administrationMetadataCatalog.GetLobSystem(myConfiguration.LobSystemName);
                }
                catch
                {

                }
                if (lobSystem != null)
                {
                    if (lobSystem.GetEntityCount() == 0)
                    {
                        lobSystem.Delete();
                    }
                    else
                    {
                        if (lobSystem.HasOnlyInactiveEntities())
                        {
                            foreach (Entity entity in lobSystem.Entities)
                            {
                                entity.Delete();
                            }
                        }
                        else
                        {
                            //ToDo
                        }
                    }
                }
                if (SPFarm.Local.Solutions[Path.GetFileName(myConfiguration.PackageConfiguration.WspPackage)] != null)
                {
                    SPSolution solution = SPFarm.Local.Solutions[Path.GetFileName(myConfiguration.PackageConfiguration.WspPackage)];
                    solution.Retract(DateTime.Now);
                    solution.Delete();
                }

            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }

        }


        public virtual void BeforeSave()
        {
            //ToDo: Here building and publishing project.

        }

        public virtual void AfterSave()
        {
        }

        public virtual void AfterLoad()
        {
        }

    }
}

using Findwise.InstallerModule;
using Findwise.Sharepoint.SolutionInstaller;
using ImpactRulesCreator.Properties;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactRulesCreator
{
    public class ImpactRulesCreator : InstallerModuleBase
    {
        public override string Name => "Impact Rules Creator";

        public override System.Drawing.Image Icon => Resources.if_utilities_system_monitor_15358;

        private Configuration myConfiguration = new Configuration();
        public override Findwise.Configuration.ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                var siteHitRulesCollection = GetSiteHitRules();
                var siteHitRuleExists = siteHitRulesCollection.Any(x => x.Site == myConfiguration.Site);

                Status = siteHitRuleExists ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        public override void Install()
        {
            Status = InstallerModuleStatus.Installing;
            try
            {
                var siteHitRulesCollection = GetSiteHitRules();
                siteHitRulesCollection.Create(myConfiguration.Site, myConfiguration.HitRate, SiteHitRuleBehavior.DelayBetweenRequests);
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
            Status = InstallerModuleStatus.Uninstalling;
            try
            {
                var siteHitRulesCollection = GetSiteHitRules();
                var siteHitRuleExist = siteHitRulesCollection.Where(x => x.Site == myConfiguration.Site).First();

                siteHitRuleExist.Delete();
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }
        private static SiteHitRulesCollection GetSiteHitRules()
        {
            SearchService searchService = SearchService.Service;
            return searchService.SiteHitRules;
        }
    }
}

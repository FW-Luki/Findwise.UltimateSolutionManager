using System;
using System.Drawing;
using System.Linq;
using Findwise.Sharepoint.SolutionInstaller;
using Findwise.Configuration;
using Microsoft.Office.Server.Search.Administration;
using CrawlRulesCreator.Properties;
using Findwise.InstallerModule;

namespace CrawlRulesCreator
{
    public class CrawlRulesCreator : InstallerModuleBase
    {
        public override string Name => "Crawl Rules Creator";

        public override Image Icon => Resources.if_Precision_1562695;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
                var result = myConfiguration.CrawlRuleDefinitions.All(myRule => content.CrawlRules.Any(sharepointRule => myRule.CompareToCrawlRule(sharepointRule)));

                if (result)
                    Status = InstallerModuleStatus.Installed;
                else
                    Status = InstallerModuleStatus.NotInstalled;
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }

        public override void Install()
        {
            Status = InstallerModuleStatus.Installing;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

                var notInstalledRules = myConfiguration.CrawlRuleDefinitions.Where(myRule => !content.CrawlRules.Any(sharepointRule => myRule.CompareToCrawlRule(sharepointRule)));

                foreach (var rule in notInstalledRules)
                {
                    content.CrawlRules.Create(rule.IsExclude ? CrawlRuleType.ExclusionRule : CrawlRuleType.InclusionRule, rule.Path);
                }
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }

        public override void Uninstall()
        {
            Status = InstallerModuleStatus.Uninstalling;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

                if (myConfiguration.UninstallAll)
                {
                    var allSharePointCrawlRules = content.CrawlRules;

                    allSharePointCrawlRules.ToList().ForEach(element => element.Delete());
                }
                else
                {
                    var allMyCrawlRules = content.CrawlRules.Where(sharepointRule => myConfiguration.CrawlRuleDefinitions.Any(myRule => myRule.CompareToCrawlRule(sharepointRule)));

                    allMyCrawlRules.ToList().ForEach(element => element.Delete());
                }
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }
        
        private Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return  new Content(context);
        }

    }
}

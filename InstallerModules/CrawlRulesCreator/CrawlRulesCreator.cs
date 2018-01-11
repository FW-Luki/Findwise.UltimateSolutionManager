using System;
using System.Drawing;
using System.Linq;
using Findwise.Sharepoint.SolutionInstaller;
using Findwise.Configuration;
using Microsoft.Office.Server.Search.Administration;
using CrawlRulesCreator.Properties;

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
                var result = myConfiguration.CrawlRuleDefinitions.All(myRule => content.CrawlRules.Any(sharepointRule => ExtensionMethod.CompareToCrawlRule(sharepointRule, myRule.Path, myRule.IsExclude)));

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
            try
            {
                Status = InstallerModuleStatus.Installing;

                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

                var notInstalledRules = myConfiguration.CrawlRuleDefinitions.Where(myRule => !content.CrawlRules.Any(sharepointRule => ExtensionMethod.CompareToCrawlRule(sharepointRule, myRule.Path, myRule.IsExclude)));

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
            try
            {
                Status = InstallerModuleStatus.Uninstalling;

                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

                if (myConfiguration.UninstallAll)
                {
                    var allSharePointCrawlRules = content.CrawlRules;

                    allSharePointCrawlRules.ToList().ForEach(element => element.Delete());
                }
                else
                {
                    var allMyCrawlRules = content.CrawlRules.Where(sharepointRule => myConfiguration.CrawlRuleDefinitions.Any(myRule => ExtensionMethod.CompareToCrawlRule(sharepointRule, myRule.Path, myRule.IsExclude)));

                    allMyCrawlRules.ToList().ForEach(element => element.Delete());
                }
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }
        
        private static Content SearchApplicationContent(string searchApplicationName)
        {
            string ssaName = searchApplicationName;
            SearchContext context = SearchContext.GetContext(ssaName);
            Content content = new Content(context);

            return content;
        }

    }
}

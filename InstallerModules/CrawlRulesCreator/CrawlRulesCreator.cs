using System;
using System.Drawing;
using System.Linq;
using Findwise.Sharepoint.SolutionInstaller;
using Findwise.Configuration;
using Microsoft.Office.Server.Search.Administration;
using CrawlRulesCreator.Properties;
using Findwise.InstallerModule;
using log4net;
using System.Runtime.CompilerServices;
using Microsoft.SharePoint;
using System.ComponentModel;

namespace CrawlRulesCreator
{
    [Category(ModuleCategoryNames.SharepointSearch)]
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
                var crawlRuleExists = myConfiguration.CrawlRuleDefinitions.All(myRule => content.CrawlRules.Any(sharepointRule => myRule.CompareToCrawlRule(sharepointRule)));

                if (!crawlRuleExists && myConfiguration.CrawlRuleDefinitions.All(myRule => content.CrawlRules.Any(sharepointRule => StringComparer.InvariantCultureIgnoreCase.Compare(myRule.Path, sharepointRule.Path) == 0)))
                {
                    throw new SPDuplicateValuesFoundException("The path already exists in any crawl rules. Try again using a different path.");
                }

                if (crawlRuleExists)
                    Status = InstallerModuleStatus.Installed;
                else
                    Status = InstallerModuleStatus.NotInstalled;
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
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

                var notInstalledRules = myConfiguration.CrawlRuleDefinitions.Where(myRule => !content.CrawlRules.Any(sharepointRule => myRule.CompareToCrawlRule(sharepointRule)));

                foreach (var rule in notInstalledRules)
                {
                    ICrawlRuleConfiguration crawlRuleConfiguration = rule.CrawlRuleConfiguration;
                    var crawlRule = content.CrawlRules.Create(rule.CrawlRuleConfiguration is Exclude ? CrawlRuleType.ExclusionRule : CrawlRuleType.InclusionRule, rule.IsRegularExpression, rule.Path);
                    if (rule.CrawlRuleConfiguration is Include include)
                    {

                        crawlRule.SuppressIndexing = include.SuppressIndexing;
                        crawlRule.FollowComplexUrls = include.FollowComplexUrls;
                        crawlRule.CrawlAsHttp = include.CrawlAsHttp;
                    }
                    else if (rule.CrawlRuleConfiguration is Exclude exclude)
                    {
                        crawlRule.FollowComplexUrls = exclude.FollowComplexUrls;
                    }
                    if (rule.Priority.HasValue)
                        content.CrawlRules.SetPriority(crawlRule, rule.Priority.Value);

                    crawlRule.Update();
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
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        private static Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }

    }
}

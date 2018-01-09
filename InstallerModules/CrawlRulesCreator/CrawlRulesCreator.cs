using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Sharepoint.SolutionInstaller;
using Findwise.Configuration;
using Microsoft.SharePoint.Administration;
using Microsoft.Office.Server.Search.Administration;
using static CrawlRulesCreator.Configuration;

namespace CrawlRulesCreator
{
    public class CrawlRulesCreator : InstallerModuleBase
    {
        public override string Name => "Crawl Rules Creator";

        public override Image Icon => null;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
                var result = myConfiguration.CrawlRuleDefinitions.All(myRule => content.CrawlRules.Any(sharepointRule => Helpers.CompareToCrawlRule(sharepointRule, myRule.Path, myRule.IsExclude)));

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
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);

            var notInstalledRules = myConfiguration.CrawlRuleDefinitions.Where(myRule => !content.CrawlRules.Any(sharepointRule => Helpers.CompareToCrawlRule(sharepointRule, myRule.Path, myRule.IsExclude)));

            foreach (var rule in notInstalledRules)
            {
                content.CrawlRules.Create(rule.IsExclude ? CrawlRuleType.ExclusionRule : CrawlRuleType.InclusionRule, rule.Path);
            }
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
        
        public Content SearchApplicationContent(string searchApplicationName)
        {
            string ssaName = searchApplicationName;
            SearchContext context = SearchContext.GetContext(ssaName);
            Content content = new Content(context);

            return content;
        }

    }
}

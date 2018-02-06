using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Server.Search.Administration;
using static CrawlRulesCreator.Configuration;

namespace CrawlRulesCreator
{
    internal static class Helpers
    {
        public static bool CompareToCrawlRule(this CrawlRuleDefinition crawlRuleDefinition, CrawlRule sharepointRule)
        {
            return StringComparer.InvariantCultureIgnoreCase.Compare(sharepointRule.Path, crawlRuleDefinition.Path) == 0 && sharepointRule.Type == (crawlRuleDefinition.CrawlRuleConfiguration is Exclude ? CrawlRuleType.ExclusionRule : CrawlRuleType.InclusionRule);
        }
    }
}

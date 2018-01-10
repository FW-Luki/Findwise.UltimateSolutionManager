using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Server.Search.Administration;

namespace CrawlRulesCreator
{
    static class ExtensionMethod
    {
        public static bool CompareToCrawlRule(CrawlRule sharepointRule, string path, bool isExclude)
        {
            return StringComparer.InvariantCultureIgnoreCase.Compare(sharepointRule.Path, path) == 0 && sharepointRule.Type == (isExclude ? CrawlRuleType.ExclusionRule : CrawlRuleType.InclusionRule);
        }
    }
}

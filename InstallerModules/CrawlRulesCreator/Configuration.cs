using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;


namespace CrawlRulesCreator
{
    public class Configuration : ConfigurationBase
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        [RefreshProperties(RefreshProperties.All)]
        public CrawlRuleDefinition[] CrawlRuleDefinitions { get; set; }
        [DisplayName("Uninstall all")]
        [Description("Choose true if you want delete all crawl rules on uninstall operation.")]
        [DefaultValue(false)]
        public bool UninstallAll { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class CrawlRuleDefinition
        {
            public string Path { get; set; }    
            public bool IsExclude{ get; set; }
                
            public override string ToString()
            {
                return Path;
            }
        }
    }

}

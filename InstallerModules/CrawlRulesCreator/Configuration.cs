using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Findwise.Configuration.TypeEditors;
using System.Drawing.Design;
using Findwise.Configuration.TypeConverters;
using Findwise.SolutionManager.Core.Model;

namespace CrawlRulesCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [HelpLink("https://technet.microsoft.com/en-us/library/jj219686(v=office.16).aspx#proc1")]
        [RefreshProperties(RefreshProperties.All)]
        public CrawlRuleDefinition[] CrawlRuleDefinitions { get; set; }

        [OrderedCategory(ModuleCategoryNames.Properties.SettingsCategoryName, 1)]
        [DisplayName("Uninstall all")]
        [Description("Choose true if you want delete all crawl rules on uninstall operation.")]
        [DefaultValue(false)]
        public bool UninstallAll { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class CrawlRuleDefinition
        {
            [Description("Type the path affected by this rule.\nExamples: http://hostname/*; http://*.*; *://hostname/*")]
            public string Path { get; set; }

            [Description("Use regular expression syntax for matching this rule")]
            [DisplayName("Use regular expression")]
            public bool IsRegularExpression { get; set; }

            [DisplayName("Order")]
            public int? Priority { get; set; }

            [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(ICrawlRuleConfiguration))]
            [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
            [Description("Select whether items in the path are excluded from or included in the content index.")]
            [DisplayName("Crawl Configuration")]
            public ICrawlRuleConfiguration CrawlRuleConfiguration { get; set; }


            public override string ToString()
            {
                return Path;
            }
        }
    }
    public interface ICrawlRuleConfiguration
    {
        bool FollowComplexUrls { get; set; }
    }

    [DisplayName("Exclude all items in this path")]
    public class Exclude : ConfigurationBase, ICrawlRuleConfiguration
    {
        [Description("Exclude complex URLs (URLs that contain question marks - ?)")]
        [DisplayName("Exclude complex URLs")]
        public bool FollowComplexUrls { get; set; }
    }

    [DisplayName("Include all items in this path")]
    public class Include : ConfigurationBase, ICrawlRuleConfiguration
    {
        [Description("Follow links on the URL without crawling the URL itself")]
        [DisplayName("Follow links on the URL")]
        public bool SuppressIndexing { get; set; }

        [Description("Crawl complex URLs (URLs that contain a question mark - ?)")]
        [DisplayName("Crawl complex URLs")]
        public bool FollowComplexUrls { get; set; }

        [DisplayName("Crawl SharePoint content as http pages")]
        public bool CrawlAsHttp { get; set; }
    }
}

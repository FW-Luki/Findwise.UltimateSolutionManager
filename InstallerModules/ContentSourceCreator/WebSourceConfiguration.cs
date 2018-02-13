using Findwise.Configuration;
using Findwise.Configuration.TypeConverters;
using Findwise.Configuration.TypeEditors;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAdministration = Microsoft.Office.Server.Search.Administration;

namespace ContentSourceCreator
{
    public class WebSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
    {
        [Description("Type a name to describe this content source.")]
        [DisplayName("Content Source Name")]
        public string ContentSourceName { get; set; }

        [Description("Type the URLs from which the search system should start crawling.")]
        [DisplayName("Start Addresses")]
        public string[] StartAddresses { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(ICrawlSettingsConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [Description("Specify the behavior for crawling this type of content.")]
        [DisplayName("Crawl Settings")]
        public ICrawlSettingsConfiguration CrawlSettingsConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        [Description("If you want start full crawl after add content source choose true.")]
        [DisplayName("Start Full Crawl")]
        public bool StartFullCrawl { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public ContentSource GetContentSource(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var webSource = myConfiguration.ContentSourceConfiguration as WebSourceConfiguration;
            var webContentSource = (WebContentSource)contentSources.Create(typeof(WebContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                webContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            var crawlSettingsConfiguration = webSource.CrawlSettingsConfiguration;
            webContentSource.MaxPageEnumerationDepth = crawlSettingsConfiguration.MaxPageEnumerationDepth.HasValue ? crawlSettingsConfiguration.MaxPageEnumerationDepth.Value : Int32.MaxValue;
            webContentSource.MaxSiteEnumerationDepth = crawlSettingsConfiguration.MaxSiteEnumerationDepth.HasValue ? crawlSettingsConfiguration.MaxSiteEnumerationDepth.Value : Int32.MaxValue;

            webContentSource.Update();

            return webContentSource;
        }
    }
}

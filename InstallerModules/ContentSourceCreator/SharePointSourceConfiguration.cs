using Findwise.Configuration;
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
    public class SharePointSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
    {
        [Description("Type a name to describe this content source.")]
        [DisplayName("Content Source Name")]
        public string ContentSourceName { get; set; }
        [Description("Type the URLs from which the search system should start crawling.")]
        [DisplayName("Start Addresses")]
        public string[] StartAddresses { get; set; }
        [Description("Continuous Crawl is a special type of crawl that eliminates the need to create incremental crawl schedules and will seamlessly work with the content source to provide maximum freshness. Please Note: Once enabled, you will not be able to pause or stop continuous crawl. You will only have the option of disabling continuous crawl. ")]
        [DisplayName("Enable Continuous Crawls")]
        public bool EnableContinuousCrawls { get; set; }
        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl everything under the hostname for each start address' or false for 'Only crawl the Site Collection of each start address'")]
        [DefaultValue(true)]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public ContentSource GetContentSource(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var sharePointSource = myConfiguration.ContentSourceConfiguration as SharePointSourceConfiguration;
            var sharepointCrawlBehavior = sharePointSource.CrawlSettings ? SharePointCrawlBehavior.CrawlVirtualServers : SharePointCrawlBehavior.CrawlSites;
            SharePointContentSource sharepointContentSource = (SharePointContentSource)contentSources.Create(typeof(SharePointContentSource), sharepointCrawlBehavior, myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                sharepointContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            sharepointContentSource.EnableContinuousCrawls = sharePointSource.EnableContinuousCrawls;

            sharepointContentSource.Update();

            return sharepointContentSource;
        }
    }
}

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
    public class ExchangeSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
    {
        [Description("Type a name to describe this content source.")]
        [DisplayName("Content Source Name")]
        public string ContentSourceName { get; set; }

        [Description("Type the URLs from which the search system should start crawling.")]
        [DisplayName("Start Addresses")]
        public string[] StartAddresses { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl the folder and all subfolders of each start address' or false for 'Only crawl the folder of each start address'")]
        [DefaultValue(true)]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        [Description("If you want start full crawl after add content source choose true.")]
        [DisplayName("Start Full Crawl")]
        public bool StartFullCrawl { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public ContentSource GetContentSource(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var exchangeSource = myConfiguration.ContentSourceConfiguration as ExchangeSourceConfiguration;
            ExchangePublicFolderContentSource exchangeContentSource = (ExchangePublicFolderContentSource)contentSources.Create(typeof(ExchangePublicFolderContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                exchangeContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            exchangeContentSource.FollowDirectories = exchangeSource.CrawlSettings;
            exchangeContentSource.Update();

            return exchangeContentSource;
        }
    }
}

using Findwise.Configuration;
using Findwise.Configuration.TypeEditors;
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
        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl everything under the hostname for each start address' or false for 'Only crawl the Site Collection of each start address'")]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        [Editor(typeof(TypeScheduleClassEditor), typeof(UITypeEditor)), TypeScheduleTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(TypeScheduleClassEditor), typeof(UITypeEditor)), TypeScheduleTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        SearchAdministration.ContentSourceType IContentSourceConfiguration.ContentSourceType { get => SearchAdministration.ContentSourceType.SharePoint; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}

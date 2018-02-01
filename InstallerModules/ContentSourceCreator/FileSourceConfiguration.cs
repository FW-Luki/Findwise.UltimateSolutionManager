using Findwise.Configuration;
using Findwise.Configuration.TypeEditors;
using Findwise.Sharepoint.SolutionInstaller;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAdministration = Microsoft.Office.Server.Search.Administration;
using System.ComponentModel.DataAnnotations;

namespace ContentSourceCreator
{
    public class FileSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
    {
        [Description("Type a name to describe this content source.")]
        [DisplayName("Content Source Name")]
        public string ContentSourceName { get; set; }
        private string[] _startAdresses { get; set; }
        [Description(@"Type the URLs from which the search system should start crawling. Examples: \\server\directory, or file://server/directory")]
        [DisplayName("Start Addresses")]
        public string[] StartAddresses { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Select the crawl schedules for this content source.")]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Select the crawl schedules for this content source.")]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl the folder and all subfolders of each start address' or false for 'Only crawl the folder of each start address'")]
        [DefaultValue(true)]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public ContentSource GetContentSource(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var fileSource = myConfiguration.ContentSourceConfiguration as FileSourceConfiguration;
            FileShareContentSource fileContentSource = (FileShareContentSource)contentSources.Create(typeof(FileShareContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                fileContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            fileContentSource.FollowDirectories = fileSource.CrawlSettings;

            fileContentSource.Update();

            return fileContentSource;
        }
    }
}

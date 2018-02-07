using Findwise.Configuration;
using Findwise.Configuration.TypeEditors;
using Microsoft.Office.Server.Search.Administration;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAdministration = Microsoft.Office.Server.Search.Administration;

namespace ContentSourceCreator
{
    public class BusinessSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
    {
        [Description("Type a name to describe this content source.")]
        [DisplayName("Content Source Name")]
        public string ContentSourceName { get; set; }

        //[Description("Type the URLs from which the search system should start crawling.")]
        [DisplayName("External System Name")]
        [DefaultValue(new string[] { "ArticleConnector", "ArticleConnector" })]
        //[Browsable(false)]
        public string[] StartAddresses { get; set; }

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
            var businessSource = myConfiguration.ContentSourceConfiguration as BusinessSourceConfiguration;
            var businessContentSource = (BusinessDataContentSource)contentSources.Create(typeof(BusinessDataContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            var bdcServiceApplicationProxyGroup = GetSPServiceApplicationProxyGroup();

            SetBusinessDataContentSourceStartAddress(businessContentSource, bdcServiceApplicationProxyGroup, myConfiguration);

            businessContentSource.Update();

            return businessContentSource;
        }
        private void SetBusinessDataContentSourceStartAddress(BusinessDataContentSource contentSource, SPServiceApplicationProxyGroup bdcServiceApplicationProxyGroup, Configuration myConfiguration)
        {
            BusinessDataContentSource businessDataContentSource = contentSource as BusinessDataContentSource;
            if (businessDataContentSource != null && bdcServiceApplicationProxyGroup != null)
            {
                string[] LOBSystemSet = myConfiguration.ContentSourceConfiguration.StartAddresses;
                string text2 = bdcServiceApplicationProxyGroup.Name;
                if (string.IsNullOrEmpty(text2))
                {
                    text2 = "Default";
                }
                if (LOBSystemSet != null)
                {
                    for (int k = 0; k < LOBSystemSet.Length; k += 2)
                    {
                        if (k == LOBSystemSet.Length - 1)
                        {
                            throw new ArgumentException(LOBSystemSet[k]);
                        }
                        string lobSystemName = LOBSystemSet[k];
                        string lobSystemInstanceName = LOBSystemSet[k + 1];
                        Uri address = BusinessDataContentSource.ConstructStartAddress(text2, Guid.Empty, lobSystemName, lobSystemInstanceName);
                        businessDataContentSource.StartAddresses.Add(address);
                    }
                }
                else
                {
                    Uri address2 = BusinessDataContentSource.ConstructStartAddress(text2, Guid.Empty);
                    businessDataContentSource.StartAddresses.Add(address2);
                }
            }
        }
        private SPServiceApplicationProxyGroup GetSPServiceApplicationProxyGroup()
        {
            SPFarm local = SPFarm.Local;
            if (null != local)
            {
                foreach (SPServiceApplicationProxyGroup current in local.ServiceApplicationProxyGroups)
                {
                    if (null != current)
                    {
                        return current;
                    }
                }
            }
            return null;
        }
    }
}

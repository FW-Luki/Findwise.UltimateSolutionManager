using ContentSourceCreator.Properties;
using Findwise.InstallerModule;
using Findwise.Sharepoint.SolutionInstaller;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Linq;
using System.Threading;

namespace ContentSourceCreator
{
    public class ContentSourceCreator : InstallerModuleBase
    {
        public override string Name => "Content Source Creator";

        public override System.Drawing.Image Icon => Resources.if_edit_find_15278;

        private Configuration myConfiguration = new Configuration();
        public override Findwise.Configuration.ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
                var contentSources = content.ContentSources;

                bool isExistedContentSource = contentSources.Any(x => x.Name == myConfiguration.ContentSourceConfiguration.ContentSourceName);

                Status = isExistedContentSource ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;

            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        public override void Install()
        {
            Status = InstallerModuleStatus.InstallationPending;

            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            ContentSourceCollection contentSources = content.ContentSources;
            try
            {
                switch (myConfiguration.ContentSourceConfiguration.ContentSourceType)
                {
                    case (ContentSourceType.Exchange):
                        ExchangeSourceConfiguration exchangeSourceConfiguration = new ExchangeSourceConfiguration();
                        exchangeSourceConfiguration.CreateContentType(content, myConfiguration, contentSources);
                        break;

                    case (ContentSourceType.File):
                        FileSourceConfiguration fileSourceConfiguration = new FileSourceConfiguration();
                        fileSourceConfiguration.CreateContentType(content, myConfiguration, contentSources);
                        break;

                    case (ContentSourceType.SharePoint):
                        SharePointSourceConfiguration sharePointSourceConfiguration = new SharePointSourceConfiguration();
                        sharePointSourceConfiguration.CreateContentType(content, myConfiguration, contentSources);
                        break;
                    case (ContentSourceType.Web):
                        WebSourceConfiguration webSourceConfiguration = new WebSourceConfiguration();
                        webSourceConfiguration.CreateContentType(content, myConfiguration, contentSources);
                        break;
                    default:
                        Status = InstallerModuleStatus.Error;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }
        public override void Uninstall()
        {
            Status = InstallerModuleStatus.Uninstalling;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
                ContentSourceCollection contentSources = content.ContentSources;

                var existedContentSource = contentSources.First(x => x.Name == myConfiguration.ContentSourceConfiguration.ContentSourceName);

                var crawlStatus = existedContentSource.CrawlStatus;
                if (crawlStatus != CrawlStatus.Idle)
                {
                    existedContentSource.StopCrawl();
                    while (crawlStatus != CrawlStatus.Idle)
                    {
                        crawlStatus = existedContentSource.CrawlStatus;
                        Thread.Sleep(5000);
                    }
                }
                existedContentSource.Delete();
            }
            catch (Exception ex)
            {
                LogError(ex);
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }

        private static Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }        
    }
}

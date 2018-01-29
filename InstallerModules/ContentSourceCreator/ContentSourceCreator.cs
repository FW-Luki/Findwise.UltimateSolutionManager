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

        public override System.Drawing.Image Icon => null;

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
                        var fileSource = myConfiguration.ContentSourceConfiguration as FileSourceConfiguration;
                        FileShareContentSource fileContentSource = (FileShareContentSource)contentSources.Create(typeof(FileShareContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
                        foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
                        {
                            fileContentSource.StartAddresses.Add(new Uri(startAddress));
                        }
                        fileContentSource.FollowDirectories = fileSource.CrawlSettings;
                        fileContentSource.StartFullCrawl();
                        fileContentSource.Update();
                        break;

                    case (ContentSourceType.SharePoint):
                        var sharePointSource = myConfiguration.ContentSourceConfiguration as SharePointSourceConfiguration;
                        SharePointContentSource sharepointContentSource = (SharePointContentSource)contentSources.Create(typeof(SharePointContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
                        foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
                        {
                            sharepointContentSource.StartAddresses.Add(new Uri(startAddress));
                        }
                        sharepointContentSource.FollowDirectories = sharePointSource.CrawlSettings;
                        sharepointContentSource.StartFullCrawl();
                        sharepointContentSource.Update();
                        break;
                    case (ContentSourceType.Web):
                        var webSource = myConfiguration.ContentSourceConfiguration as WebSourceConfiguration;
                        WebContentSource webContentSource = (WebContentSource)contentSources.Create(typeof(WebContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
                        foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
                        {
                            webContentSource.StartAddresses.Add(new Uri(startAddress));
                        }
                        webContentSource.MaxPageEnumerationDepth = webSource.MaxPageEnumerationDepth;
                        webContentSource.MaxSiteEnumerationDepth = webSource.MaxSiteEnumerationDepth;

                        webContentSource.StartFullCrawl();
                        webContentSource.Update();
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

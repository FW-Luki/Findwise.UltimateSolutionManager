using ContentSourceCreator.Properties;
using Findwise.InstallerModule;
using Findwise.Sharepoint.SolutionInstaller;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.SharePoint;

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
                var content = GetSearchApplicationContent(myConfiguration.SearchApplicationName);
                var contentSources = content.ContentSources;

                bool contentSourceExists = contentSources.Any(x => x.Name == myConfiguration.ContentSourceConfiguration.ContentSourceName);

                if (!contentSourceExists)
                {
                    myConfiguration.ContentSourceConfiguration.StartAddresses.ToList().ForEach(mcsa =>
                    {
                        var contentsourceList = contentSources.ToList();
                        foreach (ContentSource item in contentsourceList)
                        {
                            foreach (var startAddress in item.StartAddresses)
                            {
                                if (startAddress.Equals(mcsa))
                                {
                                    throw new SPDuplicateValuesFoundException("The start address already exists in any content source");
                                }
                            }
                        }
                    });
                }

                Status = contentSourceExists ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
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
            try
            {
                var content = GetSearchApplicationContent(myConfiguration.SearchApplicationName);
                ContentSourceCollection contentSources = content.ContentSources;

                IContentSourceConfiguration contentSourceConfiguration = myConfiguration.ContentSourceConfiguration;
                var contentSource = contentSourceConfiguration?.GetContentSource(content, myConfiguration, contentSources);
                SetContentSourceCrawlSchedule(content, contentSource, contentSourceConfiguration.IncrementalCrawlConfiguration, contentSourceConfiguration.FullCrawlConfiguration);
                if (contentSourceConfiguration.StartFullCrawl)
                    contentSource.StartFullCrawl();
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
                var content = GetSearchApplicationContent(myConfiguration.SearchApplicationName);
                ContentSourceCollection contentSources = content.ContentSources;

                var existedContentSource = contentSources.First(x => x.Name == myConfiguration.ContentSourceConfiguration.ContentSourceName);

                var crawlStatus = existedContentSource.CrawlStatus;
                if (existedContentSource.ContinuousCrawlStatus == ContinuousCrawlStatus.Completing || existedContentSource.ContinuousCrawlStatus == ContinuousCrawlStatus.Crawling)
                {
                    SharePointContentSource sharepoint = (SharePointContentSource)existedContentSource;
                    sharepoint.EnableContinuousCrawls = false;
                    sharepoint.Update();
                }
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

        private void SetContentSourceCrawlSchedule(Content content, ContentSource contentSource, IContentScheduleConfiguration incrementalScheduleConfiguration, IContentScheduleConfiguration fullScheduleConfiguration)
        {
            if (incrementalScheduleConfiguration != null)
            {
                var schedule = incrementalScheduleConfiguration.GetSchedule(content);
                SetScheduleForContentSource(contentSource, schedule, incrementalScheduleConfiguration, false);
            }
            if (fullScheduleConfiguration != null)
            {
                var schedule = fullScheduleConfiguration.GetSchedule(content);
                SetScheduleForContentSource(contentSource, schedule, fullScheduleConfiguration, true);
            }
            contentSource.Update();
        }
        public void SetScheduleForContentSource(ContentSource contentSource, Schedule schedule, IContentScheduleConfiguration contentScheduleConfiguration, bool isFullCrawl)
        {
            if (contentScheduleConfiguration.RepeatConfiguration is Repeat repeat)
            {
                schedule.RepeatInterval = repeat.CrawlScheduleRepeatInterval;
                schedule.RepeatDuration = repeat.CrawlScheduleRepeatDuration;
            }
            if (isFullCrawl)
                contentSource.FullCrawlSchedule = schedule;
            else
                contentSource.IncrementalCrawlSchedule = schedule;
            contentSource.Update();
        }

        private static Content GetSearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }
    }
}

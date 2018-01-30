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
    public class WebSourceConfiguration : ConfigurationBase, IContentSourceConfiguration
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

        [Description("Specify the behavior for crawling this type of content.")]
        [DefaultValue(true)]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        [Description("Page depth is the number of links that the crawler follows on the same host name, starting from each start address in the content source.")]
        [DisplayName("Limit Page Depth")]
        public int MaxPageEnumerationDepth { get; set; }
        [Description("Server hops are how many times the crawler moves from one host name to another host name.")]
        [DisplayName("Limit Server Hops")]
        public int MaxSiteEnumerationDepth { get; set; }

        SearchAdministration.ContentSourceType IContentSourceConfiguration.ContentSourceType { get => SearchAdministration.ContentSourceType.Web; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public void CreateContentType(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var webSource = myConfiguration.ContentSourceConfiguration as WebSourceConfiguration;
            WebContentSource webContentSource = (WebContentSource)contentSources.Create(typeof(WebContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                webContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            webContentSource.MaxPageEnumerationDepth = webSource.MaxPageEnumerationDepth;
            webContentSource.MaxSiteEnumerationDepth = webSource.MaxSiteEnumerationDepth;

            var incrementalScheduleType = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.GetType().Name;
            var fullScheduleType = myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration.GetType().Name;
            if (incrementalScheduleType != null)
            {
                SetScheduleForContentSource(content, myConfiguration, incrementalScheduleType, webContentSource, false);
            }
            if (fullScheduleType != null)
            {
                SetScheduleForContentSource(content, myConfiguration, fullScheduleType, webContentSource, true);
            }
            webContentSource.StartFullCrawl();
            webContentSource.Update();
        }
        public void SetScheduleForContentSource(Content content, Configuration myConfiguration, string scheduleType, WebContentSource webContentSource, bool isFullCrawl)
        {
            switch (scheduleType)
            {
                case ("Daily"):
                    var daily = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration as Daily : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Daily;
                    DailySchedule dailySchedule = new DailySchedule(content.SearchApplication);
                    dailySchedule.DaysInterval = daily.CrawlScheduleRunEveryInterval;
                    dailySchedule.StartHour = daily.CrawlScheduleStartDateTime;

                    var repeatName = daily.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration.RepeatConfiguration as Repeat : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        dailySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        dailySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    var scheduleD = isFullCrawl ? webContentSource.FullCrawlSchedule = dailySchedule : webContentSource.IncrementalCrawlSchedule = dailySchedule;
                    webContentSource.Update();
                    break;
                case ("Weekly"):
                    var weekly = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration as Weekly : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Weekly;
                    WeeklySchedule weeklySchedule = new WeeklySchedule(content.SearchApplication);
                    weeklySchedule.WeeksInterval = weekly.CrawlScheduleRunEveryInterval;
                    weeklySchedule.DaysOfWeek = weekly.DaysOfWeek;
                    weeklySchedule.StartHour = weekly.CrawlScheduleStartDateTime;

                    repeatName = weekly.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration.RepeatConfiguration as Repeat : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        weeklySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        weeklySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    var scheduleW = isFullCrawl ? webContentSource.FullCrawlSchedule = weeklySchedule : webContentSource.IncrementalCrawlSchedule = weeklySchedule;
                    webContentSource.Update();
                    webContentSource.Update();
                    break;
                case ("Monthly"):
                    var monthly = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration as Monthly : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Monthly;
                    MonthlyDateSchedule monthlySchedule = new MonthlyDateSchedule(content.SearchApplication);
                    monthlySchedule.DaysOfMonth = monthly.DaysOfMonth;
                    monthlySchedule.MonthsOfYear = monthly.MonthsOfYear;
                    monthlySchedule.StartHour = monthly.CrawlScheduleStartDateTime;

                    repeatName = monthly.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = isFullCrawl ? myConfiguration.ContentSourceConfiguration.FullCrawlConfiguration.RepeatConfiguration as Repeat : myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        monthlySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        monthlySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    var scheduleM = isFullCrawl ? webContentSource.FullCrawlSchedule = monthlySchedule : webContentSource.IncrementalCrawlSchedule = monthlySchedule;
                    webContentSource.Update();
                    break;
            }
        }
    }
}

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
        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl the folder and all subfolders of each start address' or false for 'Only crawl the folder of each start address'")]
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

        SearchAdministration.ContentSourceType IContentSourceConfiguration.ContentSourceType { get => SearchAdministration.ContentSourceType.Exchange; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public void CreateContentType (Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var exchangeSource = myConfiguration.ContentSourceConfiguration as ExchangeSourceConfiguration;
            ExchangePublicFolderContentSource exchangeContentSource = (ExchangePublicFolderContentSource)contentSources.Create(typeof(ExchangePublicFolderContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                exchangeContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            exchangeContentSource.FollowDirectories = exchangeSource.CrawlSettings;

            var incrementalScheduleType = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.GetType().Name;
            if (incrementalScheduleType != null)
            {
                SetScheduleForContentSource(content, myConfiguration, incrementalScheduleType, exchangeContentSource);
            }
            exchangeContentSource.StartFullCrawl();
            exchangeContentSource.Update();
        }
        public void SetScheduleForContentSource(Content content, Configuration myConfiguration, string incrementalScheduleType, ExchangePublicFolderContentSource exchangeContentSource)
        {
            switch (incrementalScheduleType)
            {
                case ("Daily"):
                    var daily = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Daily;
                    DailySchedule dailySchedule = new DailySchedule(content.SearchApplication);
                    dailySchedule.DaysInterval = daily.CrawlScheduleRunEveryInterval;
                    dailySchedule.StartHour = daily.CrawlScheduleStartDateTime;

                    var repeatName = daily.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        dailySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        dailySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    exchangeContentSource.IncrementalCrawlSchedule = dailySchedule;
                    exchangeContentSource.Update();
                    break;
                case ("Weekly"):
                    var weekly = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Weekly;
                    WeeklySchedule weeklySchedule = new WeeklySchedule(content.SearchApplication);
                    weeklySchedule.WeeksInterval = weekly.CrawlScheduleRunEveryInterval;
                    weeklySchedule.DaysOfWeek = weekly.DaysOfWeek;
                    weeklySchedule.StartHour = weekly.CrawlScheduleStartDateTime;

                    repeatName = weekly.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        weeklySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        weeklySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    exchangeContentSource.IncrementalCrawlSchedule = weeklySchedule;
                    exchangeContentSource.Update();
                    break;
                case ("Monthly"):
                    var monthly = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration as Monthly;
                    MonthlyDateSchedule monthlySchedule = new MonthlyDateSchedule(content.SearchApplication);
                    monthlySchedule.DaysOfMonth = monthly.DaysOfMonth;
                    monthlySchedule.MonthsOfYear = monthly.MonthsOfYear;
                    monthlySchedule.StartHour = monthly.CrawlScheduleStartDateTime;

                    repeatName = monthly.RepeatConfiguration.GetType().Name;
                    if (repeatName == "Repeat")
                    {
                        var repeatConfiguration = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.RepeatConfiguration as Repeat;
                        monthlySchedule.RepeatInterval = repeatConfiguration.CrawlScheduleRepeatInterval;
                        monthlySchedule.RepeatDuration = repeatConfiguration.CrawlScheduleRepeatDuration;
                    }
                    exchangeContentSource.IncrementalCrawlSchedule = monthlySchedule;
                    exchangeContentSource.Update();
                    break;
            }
        }
    }
}

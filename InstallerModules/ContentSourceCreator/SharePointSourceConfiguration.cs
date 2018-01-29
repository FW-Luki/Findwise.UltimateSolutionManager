﻿using Findwise.Configuration;
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
        [Description("Specify the behavior for crawling this type of content. Choose true if you want: 'Crawl everything under the hostname for each start address' or false for 'Only crawl the Site Collection of each start address'")]
        [DisplayName("Crawl Settings")]
        public bool CrawlSettings { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Incremental Crawl")]
        public IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentScheduleConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Full Crawl")]
        public IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

        SearchAdministration.ContentSourceType IContentSourceConfiguration.ContentSourceType { get => SearchAdministration.ContentSourceType.SharePoint; }

        public override string ToString()
        {
            return GetType().Name;
        }
        public void CreateContentType(Content content, Configuration myConfiguration, ContentSourceCollection contentSources)
        {
            var sharePointSource = myConfiguration.ContentSourceConfiguration as SharePointSourceConfiguration;
            SharePointContentSource sharepointContentSource = (SharePointContentSource)contentSources.Create(typeof(SharePointContentSource), myConfiguration.ContentSourceConfiguration.ContentSourceName);
            foreach (var startAddress in myConfiguration.ContentSourceConfiguration.StartAddresses)
            {
                sharepointContentSource.StartAddresses.Add(new Uri(startAddress));
            }
            sharepointContentSource.FollowDirectories = sharePointSource.CrawlSettings;

            var incrementalScheduleType = myConfiguration.ContentSourceConfiguration.IncrementalCrawlConfiguration.GetType().Name;
            if (incrementalScheduleType != null)
            {
                SetScheduleForContentSource(content, myConfiguration, incrementalScheduleType, sharepointContentSource);
            }
            sharepointContentSource.StartFullCrawl();
            sharepointContentSource.Update();
        }
        public void SetScheduleForContentSource(Content content, Configuration myConfiguration, string incrementalScheduleType, SharePointContentSource fileContentSource)
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
                    fileContentSource.IncrementalCrawlSchedule = dailySchedule;
                    fileContentSource.Update();
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
                    fileContentSource.IncrementalCrawlSchedule = weeklySchedule;
                    fileContentSource.Update();
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
                    fileContentSource.IncrementalCrawlSchedule = monthlySchedule;
                    fileContentSource.Update();
                    break;
            }
        }
    }
}

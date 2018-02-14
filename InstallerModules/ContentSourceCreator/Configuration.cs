using Findwise.Configuration;
using Findwise.InstallerModule;
using SearchAdministration = Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;
using Findwise.Configuration.TypeEditors;
using System.Windows.Forms.Design;
using Findwise.Configuration.TypeConverters;
using Microsoft.Office.Server.Search.Administration;
using Findwise.SolutionInstaller.Core.Model;

namespace ContentSourceCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [HelpLink("https://technet.microsoft.com/pl-pl/library/jj219808(v=office.16).aspx#proc2")]
        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IContentSourceConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Select what type of content will be crawled. Note: This cannot be changed after this content source is created because other settings depend on it.")]
        [DisplayName("Content Source Type")]
        public IContentSourceConfiguration ContentSourceConfiguration { get; set; }

    }
    public interface IContentSourceConfiguration
    {
        string ContentSourceName { get; set; }
        string[] StartAddresses { get; set; }
        IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }
        IContentScheduleConfiguration FullCrawlConfiguration { get; set; }
        bool StartFullCrawl { get; set; }
        ContentSource GetContentSource(Content content, Configuration myConfiguration, SearchAdministration.ContentSourceCollection contentSources);
    }
    public interface IContentScheduleConfiguration
    {
        int CrawlScheduleStartDateTime { get; set; }
        IRepeatConfiguration RepeatConfiguration { get; set; }
        Schedule GetSchedule(Content content);
    }
    public interface IRepeatConfiguration
    {
    }

    public interface ICrawlSettingsConfiguration
    {
        int? MaxPageEnumerationDepth { get; set; }
        int? MaxSiteEnumerationDepth { get; set; }
    }

    public class Daily : ConfigurationBase, IContentScheduleConfiguration
    {
        [DefaultValue(1)]
        [Description("Run every: x days")]
        [DisplayName("Run every")]
        public int CrawlScheduleRunEveryInterval { get; set; }

        [Description("Enter a hour. Example: 15 it's 3:00 PM.")]
        [DisplayName("Starting time")]
        public int CrawlScheduleStartDateTime { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IRepeatConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DisplayName("Repeat within the day")]
        public IRepeatConfiguration RepeatConfiguration { get; set; } = new DontRepeat();

        public Schedule GetSchedule(Content content)
        {
            DailySchedule dailySchedule = new DailySchedule(content.SearchApplication)
            {
                DaysInterval = this.CrawlScheduleRunEveryInterval,
                StartHour = this.CrawlScheduleStartDateTime
            };

            return dailySchedule;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    public class Weekly : ConfigurationBase, IContentScheduleConfiguration
    {
        [DefaultValue(1)]
        [Description("Run every: x weeks")]
        [DisplayName("Run every")]
        public int CrawlScheduleRunEveryInterval { get; set; }

        [DefaultValue(1)]
        [Description("Choose day of week")]
        [DisplayName("On")]
        public SearchAdministration.DaysOfWeek DaysOfWeek { get; set; }

        [Description("Enter a hour. Example: 15 it's 3:00 PM.")]
        [DisplayName("Starting time")]
        public int CrawlScheduleStartDateTime { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IRepeatConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DisplayName("Repeat within the day")]
        public IRepeatConfiguration RepeatConfiguration { get; set; }

        public Schedule GetSchedule(Content content)
        {
            WeeklySchedule weeklySchedule = new WeeklySchedule(content.SearchApplication)
            {
                WeeksInterval = this.CrawlScheduleRunEveryInterval,
                DaysOfWeek = this.DaysOfWeek,
                StartHour = this.CrawlScheduleStartDateTime
            };

            return weeklySchedule;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    public class Monthly : ConfigurationBase, IContentScheduleConfiguration
    {
        [DefaultValue(1)]
        [DisplayName("Day of Month")]
        public SearchAdministration.DaysOfMonth DaysOfMonth { get; set; }

        [DefaultValue(1)]
        [DisplayName("Month of Year")]
        [Editor(typeof(FlagsEnumEditor<MonthsOfYear>), typeof(UITypeEditor))]
        public SearchAdministration.MonthsOfYear MonthsOfYear { get; set; }

        [Description("Enter a hour. Example: 15 it's 3:00 PM.")]
        [DisplayName("Starting time")]
        public int CrawlScheduleStartDateTime { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IRepeatConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DisplayName("Repeat within the day")]
        public IRepeatConfiguration RepeatConfiguration { get; set; }

        public Schedule GetSchedule(Content content)
        {
            MonthlyDateSchedule monthlySchedule = new MonthlyDateSchedule(content.SearchApplication)
            {
                DaysOfMonth = this.DaysOfMonth,
                MonthsOfYear = this.MonthsOfYear,
                StartHour = this.CrawlScheduleStartDateTime
            };

            return monthlySchedule;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    public class Repeat : ConfigurationBase, IRepeatConfiguration
    {
        [Description("Every: x minutes")]
        [DisplayName("Every")]
        public int CrawlScheduleRepeatInterval { get; set; }

        [Description("For: x minutes")]
        [DisplayName("For")]
        public int CrawlScheduleRepeatDuration { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    [DisplayName("Don't repeat")]
    public class DontRepeat : ConfigurationBase, IRepeatConfiguration
    {
    }

    [DisplayName("Only crawl within the server of each start address")]
    public class OnlyCrawlWithinTheServerOfEachStartAddress : ICrawlSettingsConfiguration
    {
        [Browsable(false)]
        public int? MaxPageEnumerationDepth { get; set; } = null;

        [Browsable(false)]
        public int? MaxSiteEnumerationDepth { get; set; } = 0;
    }
    [DisplayName("Only crawl the first page of each start address")]
    public class OnlyCrawltheFirstPageOfEachStartAddress : ICrawlSettingsConfiguration
    {
        [Browsable(false)]
        public int? MaxPageEnumerationDepth { get; set; } = 0;

        [Browsable(false)]
        public int? MaxSiteEnumerationDepth { get; set; } = 0;
    }
    [DisplayName("Custom")]
    public class Custom : ICrawlSettingsConfiguration
    {
        [Description("Page depth is the number of links that the crawler follows on the same host name, starting from each start address in the content source.")]
        [DisplayName("Limit Page Depth")]
        public int? MaxPageEnumerationDepth { get; set; }

        [Description("Server hops are how many times the crawler moves from one host name to another host name.")]
        [DisplayName("Limit Server Hops")]
        public int? MaxSiteEnumerationDepth { get; set; }
    }
}

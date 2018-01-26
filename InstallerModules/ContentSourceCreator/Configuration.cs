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

namespace ContentSourceCreator
{
    public class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [Editor(typeof(ContentSourceClassEditor), typeof(UITypeEditor)), ContentSourceTypeEditor.Options(BaseType = typeof(IContentSourceConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Select what type of content will be crawled. Note: This cannot be changed after this content source is created because other settings depend on it.")]
        [DisplayName("Content Source Type")]
        public IContentSourceConfiguration ContentSourceConfiguration { get; set; }

    }
    public interface IContentSourceConfiguration
    {
        string ContentSourceName { get; set; }
        string[] StartAddresses { get; set; }
        bool CrawlSettings { get; set; }
        SearchAdministration.ContentSourceType ContentSourceType { get; }
        IContentScheduleConfiguration IncrementalCrawlConfiguration { get; set; }
        IContentScheduleConfiguration FullCrawlConfiguration { get; set; }

    }
    public interface IContentScheduleConfiguration
    {
        int CrawlScheduleRunEveryInterval { get; set; }
        int CrawlScheduleStartDateTime { get; set; }
        IRepeatConfiguration RepeatConfiguration { get; set; }
    }
    public interface IRepeatConfiguration
    {
    }

    public class Daily : ConfigurationBase, IContentScheduleConfiguration
    {
        [Description("Run every: x days")]
        [DisplayName("Run every")]
        public int CrawlScheduleRunEveryInterval { get; set; }
        [Description("Enter a hour. Example: 15 it's 3:00 PM.")]
        [DisplayName("Starting time")]
        public int CrawlScheduleStartDateTime { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IRepeatConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DisplayName("Repeat within the day")]
        public IRepeatConfiguration RepeatConfiguration { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    public class Weekly : ConfigurationBase, IContentScheduleConfiguration
    {
        [Description("Run every: x days")]
        [DisplayName("Run every")]
        public int CrawlScheduleRunEveryInterval { get; set; }
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

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    public class Monthly : ConfigurationBase, IContentScheduleConfiguration
    {
        [Description("Run every: x days")]
        [DisplayName("Run every")]
        public int CrawlScheduleRunEveryInterval { get; set; }
        [DisplayName("Day of Month")]
        public SearchAdministration.DaysOfMonth DaysOfMonth { get; set; }
        [DisplayName("Month of Year")]
        public SearchAdministration.MonthsOfYear MonthsOfYear { get; set; }
        [Description("Enter a hour. Example: 15 it's 3:00 PM.")]
        [DisplayName("Starting time")]
        public int CrawlScheduleStartDateTime { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IRepeatConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DisplayName("Repeat within the day")]
        public IRepeatConfiguration RepeatConfiguration { get; set; }

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

    //public enum TypeContentSource
    //{
    //    SharePoint = SearchAdministration.ContentSourceType.SharePoint,
    //    Web = SearchAdministration.ContentSourceType.Web,
    //    File = SearchAdministration.ContentSourceType.File,
    //    Exchange = SearchAdministration.ContentSourceType.Exchange,
    //    Business = SearchAdministration.ContentSourceType.Business,
    //}
    //public enum ContentSourceCrawlScheduleType
    //{
    //    None,
    //    Full,
    //    Incremental
    //}
    //public enum TypeOfSchedule
    //{
    //    None,
    //    Daily,
    //    Weekly,
    //    Monthly
    //}
}

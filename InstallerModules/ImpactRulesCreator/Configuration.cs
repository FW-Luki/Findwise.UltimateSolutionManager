using Findwise.Configuration;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionInstaller.Core.Model;

namespace ImpactRulesCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [Description("Type the name of a site.  Do not include the protocol (for example 'http://').")]
        [DisplayName("Site")]
        public string SpSite { get; set; }

        [Description("Simultaneous requests - Request up to the specified number of documents at a time and do not wait between requests\nDelay between requests - Request one document at a time and wait the specified time between requests")]
        [DisplayName("Behavior")]
        public SiteHitRuleBehavior Behavior { get; set; }

        [DefaultValue(1)]
        [Description("Indicate how the crawler will request documents from this site.\n\nValue to use for maximum requests or seconds of delay, according to behavior.\n\nIf a value is SimultaneousRequests, the hit rate is the maximum number of simultaneous requests.\nIf a value is DelayBetweenRequests, then hit rate is the number of seconds to delay between requests to the server.")]
        [DisplayName("Hit Rate")]
        public int HitRate { get; set; }
    }
}

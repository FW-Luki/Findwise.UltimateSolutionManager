using Findwise.Configuration;
using Findwise.InstallerModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactRulesCreator
{
    public class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        [Description("Type the name of a site.  Do not include the protocol (for example 'http://').")]
        public string Site { get; set; }
        [DisplayName("Behavior")]
        public int Behavior { get; set; }
        [DisplayName("Hit Rate")]
        public int HitRate { get; set; }
    }
}

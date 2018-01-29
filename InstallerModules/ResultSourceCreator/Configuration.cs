using Findwise.Configuration;
using Findwise.InstallerModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultSourceCreator
{
    public class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        [DisplayName("Result Source Name")]
        [Description("Names must be unique at each administrative level.")]
        public string ResultSourceName { get; set; }
        [Description("Change incoming queries to use this new query text instead. Include the incoming query in the new text by using the query variable '{searchTerms}'.")]
        public string Query { get; set; }

    }
}

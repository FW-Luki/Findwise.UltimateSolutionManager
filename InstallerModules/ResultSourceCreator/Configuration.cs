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
    class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        public string ResultSourceName { get; set; }
        public string Query { get; set; }

    }
}

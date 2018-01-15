using Findwise.Configuration;
using Findwise.InstallerModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedPropertiesCreator
{
    public class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        [RefreshProperties(RefreshProperties.All)]
        public ManagedPropertyDefinition[] ManagedPropertyDefinitions { get; set; }

        public class ManagedPropertyDefinition
        {
            public string Name { get; set; }
            public string[] Properties { get; set; }
            public Type PropertyType {get; set; } 
            public bool Sort { get; set; }
            public bool Retrieve { get; set; }
            public bool Refine { get; set; }
            public bool Search { get; set; }
            public bool Multivalue { get; set; }
            public bool Query { get; set; }
            public bool Safe { get; set; }

            public override string ToString()
            {
                return Name;
            }
            public enum Type
            {
                Text = 1,
                Integer = 2,
                Decimal = 3,
                DateTime = 4,
                YesNo = 5,
                Binary = 6,
                Double = 7
            }
        }
    }
}

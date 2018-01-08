using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace SharepointSolutionPackageInstaller
{
    public class Configuration : ConfigurationBase
    {
        public string WspPath { get; set; }

        public RegistryEntry[] RegistryEntries{get;set;}


        public class RegistryEntry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}

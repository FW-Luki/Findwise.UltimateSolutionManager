using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public class Project : ConfigurationBase
    {
        public IInstallerModule[] Modules { get; set; }

        public static Project Create(IEnumerable<IInstallerModule> moduleCollection)
        {
            return new Project()
            {
                Modules = moduleCollection.ToArray()
            };
        }
    }
}

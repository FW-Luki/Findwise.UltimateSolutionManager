using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ProvidePropertiesAttribute : Attribute
    {
        public string PropertiesMemberName { get; }
        public ProvidePropertiesAttribute(string propertiesMemberName)
        {
            PropertiesMemberName = propertiesMemberName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager
{
    /// <summary>
    /// Provides a name of the property containing an object that is meant to be displayed in the property grid instead of the instance of the class decorated with this attribute.
    /// This attribute is automatically inherited by classes derived from the class decorated with this attribute.
    /// </summary>
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

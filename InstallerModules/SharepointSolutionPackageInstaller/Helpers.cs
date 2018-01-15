using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharepointSolutionPackageInstaller
{
    public static class Helpers
    {
        /// <summary>
        /// Gets an attribute of an enum field value.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The attribute of type <see cref="T"/> that exists on the enum value</returns>
        /// <example>string desc = myEnumMember.GetAttribute<DescriptionAttribute>()?.Description;</example>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}

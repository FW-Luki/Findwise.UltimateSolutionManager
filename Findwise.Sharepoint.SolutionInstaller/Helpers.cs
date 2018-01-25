using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    internal static class Helpers
    {
        public static IEnumerable<T> GetFields<T>(this IComponent me, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
        {
            return me.GetType().GetFields(bindingFlags).Where(f => typeof(T).IsAssignableFrom(f.FieldType)).Select(f => (T)f.GetValue(me));
        }

    }
}

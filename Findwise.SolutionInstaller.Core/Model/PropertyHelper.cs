using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionInstaller.Core.Model
{
    public class PropertyHelper : ObservableObject
    {
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="initializer">Optional static initializer.</param>
        /// <param name="name">Property name. Set automatically.</param>
        /// <returns>Property value.</returns>
        public T Get<T>(Func<T> initializer = null, [CallerMemberName] string name = "")
        {
            return Property(initializer, name);
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="value">Value to set.</param>
        /// <param name="name">Property name. Set automatically.</param>
        public void Set<T>(T value, [CallerMemberName] string name = "")
        {
            Property(value, name);
        }
    }
}

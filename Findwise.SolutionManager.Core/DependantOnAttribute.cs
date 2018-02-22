using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Core
{
    /// <summary>
    /// Indicates to raise <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> event for this property when property of name passed in <see cref="PropertyName"/> changes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class DependantOnAttribute : Attribute
    {
        public string PropertyName { get; }
        public DependantOnAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}

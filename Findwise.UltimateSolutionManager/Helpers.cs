using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.SolutionManager.Core;

namespace Findwise.SolutionManager
{
    internal static class Helpers
    {
        public static IEnumerable<T> GetFields<T>(this IComponent me, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
        {
            return me.GetType().GetFields(bindingFlags).Where(f => typeof(T).IsAssignableFrom(f.FieldType)).Select(f => (T)f.GetValue(me));
        }

        public static bool IsBound(this GridItem item, out IBindableComponent component)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (GetBindableObject(item) is IBindableComponent bindableComponent && (item.PropertyDescriptor?.Attributes.OfType<BindableAttribute>().FirstOrDefault() ?? BindableAttribute.Default).Bindable)
            {
                component = bindableComponent;
                return bindableComponent.DataBindings.Cast<Binding>().Any(b => b.PropertyName == GetBindablePropertyDescriptor(item.PropertyDescriptor).Name);
            }
            component = null;
            return false;
        }
        private static PropertyDescriptor GetBindablePropertyDescriptor(PropertyDescriptor descriptor)
        {
            return descriptor.PropertyType.GetCustomAttributes(false).OfType<BindingSurrogateAttribute>().FirstOrDefault() is BindingSurrogateAttribute bsa ?
                TypeDescriptor.GetProperties(descriptor.PropertyType)[bsa.BindablePropertyName] : descriptor;
        }
        private static object GetBindableObject(/*PropertyGrid propertyGrid,*/ GridItem gridItem)
        {
            if (gridItem != null)
            {
                if (gridItem.Value?.GetType().GetCustomAttributes(false).OfType<BindingSurrogateAttribute>().Any() ?? false)
                {
                    return gridItem.Value;
                }
                if (gridItem.Parent?.Value != null)
                {
                    return gridItem.Parent.Value;
                }
            }
            return (gridItem.GetType().GetProperty("OwnerGrid")?.GetValue(gridItem) as PropertyGrid)?.SelectedObject;
        }


    }
}

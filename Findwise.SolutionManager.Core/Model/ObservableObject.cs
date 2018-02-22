using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Core.Model
{
    /// <summary>
    /// Base class for object raising <see cref="INotifyPropertyChanging.PropertyChanging"/> event on the beginning of property value set procedure
    /// and <see cref="INotifyPropertyChanged.PropertyChanged"/> event on the end of the property value set procedure as well as on <see cref="INotifyCollectionChanged.CollectionChanged"/> event if the property is of <see cref="INotifyCollectionChanged"/> type.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [NonSerialized]
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        [NonSerialized]
        private List<KeyValuePair<object, Delegate>> handlers = new List<KeyValuePair<object, Delegate>>();


        public event PropertyChangedEventHandler PropertyChanged;
        private IEnumerable<DependantOnAttribute> GetDependantOnAttributes(System.Reflection.PropertyInfo p, string name) => p.GetCustomAttributes(false).OfType<DependantOnAttribute>().Where(a => a.PropertyName == name);
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            foreach (var property in GetType().GetProperties().Where(p => GetDependantOnAttributes(p, name).Any()))
            {
                OnPropertyChanged(property.Name);
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        //protected void OnPropertyChanging(string propertyName)
        //{
        //    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        //}


        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="initializer">Optional static initializer.</param>
        /// <param name="name">Property name. Set automatically.</param>
        /// <returns>Property value.</returns>
        protected T Property<T>(Func<T> initializer = null, [CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (!properties.ContainsKey(name))
            {
                if (initializer != null)
                    properties.Add(name, initializer.Invoke());
                else
                    properties.Add(name, default(T));
                AddHandlers(properties[name], name);
            }
            return (T)properties[name];
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="value">Value to set.</param>
        /// <param name="name">Property name. Set automatically.</param>
        protected void Property<T>(T value, [CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            if (properties.ContainsKey(name))
            {
                RemoveHandlers(properties[name]);
                properties[name] = value;
            }
            else
            {
                properties.Add(name, value);
            }
            AddHandlers(properties[name], name);
            OnPropertyChanged(name);
        }


        private void AddHandlers(object obj, string propertyName)
        {
            if (obj is INotifyPropertyChanged observableItem)
            {
                PropertyChangedEventHandler handler = (sender_, e_) => ObservableItem_PropertyChanged(sender_, e_, propertyName);
                observableItem.PropertyChanged += handler;
                handlers.Add(new KeyValuePair<object, Delegate>(obj, handler));
            }
            if (obj is INotifyCollectionChanged observableCollection)
            {
                NotifyCollectionChangedEventHandler handler = (sender_, e_) => ObservableCollection_CollectionChanged(sender_, e_, propertyName);
                observableCollection.CollectionChanged += handler;
                handlers.Add(new KeyValuePair<object, Delegate>(obj, handler));
            }
        }
        private void RemoveHandlers(object obj)
        {
            if (obj is INotifyPropertyChanged observableItem)
            {
                foreach (var handler in handlers.Where(h => ReferenceEquals(h.Key, obj) && h.Value is PropertyChangedEventHandler))
                {
                    observableItem.PropertyChanged -= (PropertyChangedEventHandler)handler.Value;
                }
            }
            if (obj is INotifyCollectionChanged observableCollection)
            {
                foreach (var handler in handlers.Where(h => ReferenceEquals(h.Key, obj) && h.Value is NotifyCollectionChangedEventHandler))
                {
                    observableCollection.CollectionChanged -= (NotifyCollectionChangedEventHandler)handler.Value;
                }
            }
        }

        private void ObservableItem_PropertyChanged(object sender, PropertyChangedEventArgs e, string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
        private void ObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e, string propertyName)
        {
            PropertyChangedEventHandler handler = (sender_, e_) => ObservableItem_PropertyChanged(sender_, e_, propertyName);
            foreach (var item in e.OldItems?.OfType<INotifyPropertyChanged>() ?? Enumerable.Empty<INotifyPropertyChanged>())
                item.PropertyChanged -= handler;
            foreach (var item in e.NewItems?.OfType<INotifyPropertyChanged>() ?? Enumerable.Empty<INotifyPropertyChanged>())
                item.PropertyChanged += handler;
            OnPropertyChanged(propertyName);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Findwise.Configuration.TypeConverters;
using Findwise.Configuration.TypeEditors;

#warning All these classes go to Core to separate files.
namespace Findwise.Sharepoint.SolutionInstaller.Models
{
    public class BindingItem : ObservableObject
    {
        [Browsable(false)]
        public Guid Id { get => Property(() => Guid.NewGuid()); }

        [ReadOnly(false)] //Can't make it read-only for PropertyGrid only.
        public string Name
        {
            get => Property<string>();
            set => Property(value);
        }


        [TypeConverter(typeof(TypeNameConverter))]
        [Editor(typeof(DerivedTypeEditor<object>), typeof(UITypeEditor)), DerivedTypeEditor.Options(AlphabeticOrder = true, IncludeBaseType = true)]
        public Type Type { get => Property<Type>(); set => Property(value); }

        [XmlIgnore, IgnoreDataMember]
        [DisplayName("Values")]
        [TypeConverter(typeof(ValueDictionaryConverter<MasterConfig, object>))]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))] //Empty editor for now.
        public ObservableConcurrentDictionary<MasterConfig, object> ValueDictionary { get => Property(() => new ObservableConcurrentDictionary<MasterConfig, object>()); }
        [Browsable(false)]
        public KeyValuePair<MasterConfig, object>[] Values
        {
            get { return ValueDictionary.ToArray(); }
            set
            {
                ValueDictionary.AsCollection().Clear();
                foreach (var item in value)
                {
                    ValueDictionary.Add(item.Key, item.Value);
                }
                //OnPropertyChanged(nameof(ValueDictionary));
            }
        }

        [XmlIgnore, IgnoreDataMember]
        [Browsable(false)]
        public MasterConfig MasterConfig { get => Property<MasterConfig>(); set => Property(value); }

        [XmlIgnore, IgnoreDataMember]
        [DisplayName("Current Value")]
        [DependantOn(nameof(ValueDictionary))]
        [DependantOn(nameof(MasterConfig))]
        public object Value
        {
            get
            {
                return MasterConfig != null && ValueDictionary.TryGetValue(MasterConfig, out var value) ? value : null;
            }
        }


        private class ValueDictionaryConverter<TKey, TValue> : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is IDictionary<TKey, TValue> dictionary)
                    return string.Join(",", dictionary.Values.Select(v => v?.ToString()));
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                if (value is IDictionary<TKey, TValue> dictionary)
                {
                    return new PropertyDescriptorCollection(dictionary.Select(kvp => new DictionaryValuePropertyDescriptor(kvp, (context.Instance as BindingItem)?.Type)).ToArray());
                }
                return base.GetProperties(context, value, attributes);
            }

            private class DictionaryValuePropertyDescriptor : PropertyDescriptor
            {
                private readonly KeyValuePair<TKey, TValue> item;
                private readonly Type editType;

                public DictionaryValuePropertyDescriptor(KeyValuePair<TKey, TValue> kvp, Type type)
                    : base(kvp.Key.ToString(), new Attribute[]
                    {
                        new RefreshPropertiesAttribute(RefreshProperties.All)
                    })
                {
                    item = kvp;
                    editType = type ?? typeof(string);
                }

                public override Type ComponentType => typeof(IDictionary<TKey, TValue>);

                public override bool IsReadOnly => false;

                public override Type PropertyType => editType;

                public override bool CanResetValue(object component)
                {
                    return false;
                }

                public override object GetValue(object component)
                {
                    return item.Value;
                }

                public override void ResetValue(object component)
                {
                    throw new NotImplementedException();
                }

                public override void SetValue(object component, object value)
                {
                    if (component is IDictionary<TKey, TValue> dictionary)
                    {
                        dictionary[item.Key] = (TValue)value;
                    }
                }

                public override bool ShouldSerializeValue(object component)
                {
                    return true;
                }
            }
        }
    }


    public class MasterConfig : ObservableObject
    {
        [Browsable(false)]
        public Guid Id { get => Property(() => Guid.NewGuid()); }

        public string Name
        {
            get => Property<string>();
            set => Property(value);
        }

        public override string ToString()
        {
            return Name;
        }
    }


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

    /// <summary>
    /// Causes to raise <see cref="INotifyPropertyChanged.PropertyChanged"/> event for this property when property of name set in <see cref="PropertyName"/> changes.
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


    public static class Helpers
    {
        public static ICollection<KeyValuePair<TKey, TValue>> AsCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary;
        }
    }
}

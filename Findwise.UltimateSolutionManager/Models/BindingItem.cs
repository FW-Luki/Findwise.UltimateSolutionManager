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
using Findwise.SolutionManager.Core;
using Findwise.SolutionManager.Core.Model;

namespace Findwise.SolutionManager.Models
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
        [RefreshProperties(RefreshProperties.All)]
        public ObservableConcurrentDictionary<MasterConfig, object> ValueDictionary { get => Property(() => new ObservableConcurrentDictionary<MasterConfig, object>()); }
        [Browsable(false)]
        public KeyValuePair<MasterConfig, object>[] Values
        {
            get { return ValueDictionary.OrderBy(d => d.Key).ToArray(); } //ToDo: Add Order property to MasterConfig and use it here in OrderBy.
            set
            {
                ValueDictionary.AsCollection().Clear();
                foreach (var item in value)
                {
                    ValueDictionary.Add(item.Key, item.Value);
                }
                //OnPropertyChanged(nameof(ValueDictionary)); <-- No need to raise this, ValueDictionary.Add above does everything we need.
            }
        }

        [XmlIgnore, IgnoreDataMember]
        [Browsable(false)]
        public MasterConfig MasterConfig { get => Property<MasterConfig>(); set => Property(value); }

        public bool SingleValue { get => Property<bool>(); set => Property(value); }

        [XmlIgnore, IgnoreDataMember]
        [DisplayName("Current Value")]
        [DependantOn(nameof(ValueDictionary))]
        [DependantOn(nameof(MasterConfig))]
        [DependantOn(nameof(SingleValue))]
        public object Value
        {
            get
            {
                return SingleValue ? (ValueDictionary.Any() ? ValueDictionary.OrderBy(d => d.Key).First().Value : null) //ToDo: Add Order property to MasterConfig and use it here in OrderBy.
                    : (MasterConfig != null && ValueDictionary.TryGetValue(MasterConfig, out var value) ? value : null);
            }
        }


        private class ValueDictionaryConverter<TKey, TValue> : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is IDictionary<TKey, TValue> dictionary)
                    return string.Join(",", dictionary.OrderBy(d => d.Key).Select(v => v.Value?.ToString())); //ToDo: Add Order property to MasterConfig and use it here in OrderBy.
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                if (value is IDictionary<TKey, TValue> dictionary)
                {
                    return new PropertyDescriptorCollection(dictionary.OrderBy(d => d.Key).Select(kvp => new DictionaryValuePropertyDescriptor(kvp, (context.Instance as BindingItem)?.Type)).ToArray());
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
                    this.OnValueChanged(component, EventArgs.Empty);
                }

                public override bool ShouldSerializeValue(object component)
                {
                    return true;
                }
            }
        }
    }

}

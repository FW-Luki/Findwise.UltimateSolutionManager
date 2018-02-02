using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Findwise.Configuration.TypeConverters;
using Findwise.Configuration.TypeEditors;

namespace Findwise.Sharepoint.SolutionInstaller.Models
{
    public class BindingItem
    {
        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        [ReadOnly(false)] //Can't make it read-only for PropertyGrid only.
        public string Name { get; set; }

        [TypeConverter(typeof(TypeNameConverter))]
        [Editor(typeof(DerivedTypeEditor<object>), typeof(UITypeEditor)), DerivedTypeEditor.Options(AlphabeticOrder = true, IncludeBaseType = true)]
        public Type Type { get; set; }

        [XmlIgnore, IgnoreDataMember]
        [DisplayName("Values")]
        [TypeConverter(typeof(ValueDictionaryConverter<MasterConfig, object>))]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))] //Empty editor for now.
        public Dictionary<MasterConfig, object> ValueDictionary { get; } = new Dictionary<MasterConfig, object>();
        [Browsable(false)]
        public KeyValuePair<MasterConfig, object>[] Values
        {
            get { return ValueDictionary.ToArray(); }
            set
            {
                ValueDictionary.Clear();
                foreach (var item in value)
                {
                    ValueDictionary.Add(item.Key, item.Value);
                }
            }
        }

        [XmlIgnore, IgnoreDataMember]
        [Browsable(false)]
        public MasterConfig MasterConfig { get; set; }

        [XmlIgnore, IgnoreDataMember]
        [DisplayName("Current Value")]
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
                if (destinationType == typeof(string) && value is Dictionary<TKey, TValue> dictionary)
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

                public override Type ComponentType => typeof(Dictionary<TKey, TValue>);

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
                    if (component is Dictionary<TKey, TValue> dictionary)
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


    public class MasterConfig
    {
        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionManager.Core;
using Findwise.SolutionManager.Core.Model;

namespace PowershellScriptExecutor
{
    [BindingSurrogate(nameof(Value))]
    //[TypeConverter(typeof(ValueTypeConverter))]
    public class Parameter : BindableConfiguration
    {
        public string Name { get => Prop.Get<string>(); set => Prop.Set(value); }

        public Type Type { get => Prop.Get(() => typeof(string)); set => Prop.Set(value); }

        [Bindable(true)]
        [DefaultBindingItemNameProvider(nameof(Name))]
        [DefaultBindingItemTypeProvider(nameof(Type))]
        public object Value { get => Prop.Get<object>(); set => Prop.Set(value); }


        public class ValueTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string str)
                    return str;
                else
                    return base.ConvertFrom(context, culture, value);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is Parameter p)
                    return p.Value;
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}

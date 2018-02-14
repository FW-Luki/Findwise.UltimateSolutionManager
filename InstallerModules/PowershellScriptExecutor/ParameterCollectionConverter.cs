using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowershellScriptExecutor
{
    internal class ParameterCollectionConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(((value as Parameter[])?.Select(p => new ParameterDescriptor(p)) ?? Enumerable.Empty<ParameterDescriptor>()).ToArray());
        }

        internal class ParameterDescriptor : PropertyDescriptor
        {
            private readonly Parameter parameter;
            public ParameterDescriptor(Parameter parameter)
                : base(parameter.Name, new Attribute[]
                {
                    new BindableAttribute(true),
                    new TypeConverterAttribute(typeof(Parameter.ValueTypeConverter))
                })
            {
                this.parameter = parameter;
            }

            public override Type ComponentType => typeof(Parameter[]);

            public override bool IsReadOnly => false;

            public override Type PropertyType => typeof(Parameter);

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                return parameter;
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                parameter.Value = value;
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
        }
    }
}

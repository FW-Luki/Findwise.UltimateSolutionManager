using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionInstaller.Core;
using Findwise.SolutionInstaller.Core.Model;

namespace PowershellScriptExecutor
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Parameter : BindableConfiguration
    {
        public string Name { get => Prop.Get<string>(); set => Prop.Set(value); }

        public Type Type { get => Prop.Get(() => typeof(string)); set => Prop.Set(value); }

        [Bindable(true)]
        [DefaultBindingItemNameProvider(nameof(Name))]
        [DefaultBindingItemTypeProvider(nameof(Type))]
        public object Value { get => Prop.Get<object>(); set => Prop.Set(value); }


        public override string ToString()
        {
            return $"${Name}={Value}";
        }
    }
}

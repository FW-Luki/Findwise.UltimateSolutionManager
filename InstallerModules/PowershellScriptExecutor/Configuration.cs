using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Findwise.SolutionInstaller.Core.Model;

namespace PowershellScriptExecutor
{
    public class Configuration : BindableConfiguration
    {
        private const string ScriptConfigCategoryName = "Script configuration";


        [NonSerialized]
        private Dictionary<Parameter, Binding[]> bindings = new Dictionary<Parameter, Binding[]>(new ParameterSimilarityComparer());


        [OrderedCategory(ScriptConfigCategoryName, 2)]
        [Bindable(true)]
        [Editor(typeof(PowershellScriptFilenameEditor), typeof(UITypeEditor))]
        public string ScriptFilename { get => Prop.Get<string>(); set => Prop.Set(value); }

        [OrderedCategory(ScriptConfigCategoryName, 2)]
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        public Parameter[] Parameters { get => Prop.Get<Parameter[]>(); set => Prop.Set(value); }

        [OrderedCategory(ModuleCategoryNames.Properties.SettingsCategoryName, 1)]
        [DefaultValue(true)]
        [DisplayName("Allow update " + nameof(Parameters))]
        [Description("When set to true, Parameters are recreated whenever script filename is changed.")]
        public bool AllowUpdateParameters { get => Prop.Get<bool>(); set => Prop.Set(value); }

        [OrderedCategory(ModuleCategoryNames.Properties.SettingsCategoryName, 1)]
        [DefaultValue(true)]
        [DisplayName("Allow restore bindings on " + nameof(Parameters) + " change")]
        [Description("When set to true, an attempt of restore already set parameter value bindings will be taken whenever " + nameof(Parameters) + " property is changed.")]
        public bool AllowRestoreBindings { get => Prop.Get<bool>(); set => Prop.Set(value); }


        public Configuration()
        {
            Prop.PropertyChanged += Prop_PropertyChanged;
        }


        public void UpdateParameters()
        {
            try
            {
                var parameters = PowershellScriptParser.GetParameters(File.ReadAllText(ScriptFilename));
                Parameters = parameters.ToArray();
            }
            catch { } //ToDo: Do we need to log this?
        }

        [Browsable(true)]
        [DisplayName("Get script template")]
        public void CreateScriptTemplate()
        {
        }

        [Browsable(true)]
        [DisplayName("Clear binding cache")]
        public void ForgetBindings()
        {
            if (MessageBox.Show($"Are you sure you want to clear binding cache?\r\nCurrently set bindings will not be able to be restored on {nameof(Parameters)} property change.",
                "Clear binding cache", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindings.Clear();
            }
        }


        private void Prop_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ScriptFilename))
            {
                if (AllowUpdateParameters)
                {
                    UpdateParameters();
                }
            }
            if (e.PropertyName == nameof(Parameters))
            {
                if (AllowRestoreBindings)
                {
                    TryRestoreBindings();
                    RememberBindings();
                }
            }
        }

        private void TryRestoreBindings()
        {
            if (bindings != null)
            {
                foreach (var param in Parameters)
                {
                    if (bindings.TryGetValue(param, out var b) && b != null && b.Any())
                        param.Bindings = b;
                }
            }
        }
        private void RememberBindings()
        {
            foreach (var param in Parameters)
            {
                if (bindings.ContainsKey(param))
                    bindings[param] = param.Bindings;
                else
                    bindings.Add(param, param.Bindings);
            }
        }


        private class ParameterSimilarityComparer : IEqualityComparer<Parameter>
        {
            public bool Equals(Parameter x, Parameter y)
            {
                return x.Name.Equals(y.Name) && x.Type.Equals(y.Type);
            }
            public int GetHashCode(Parameter obj)
            {
                unchecked
                {
                    int result = 17;
                    result = result * 31 + obj?.Name?.GetHashCode() ?? 0;
                    result = result * 31 + obj?.Type?.GetHashCode() ?? 0;
                    return result;
                }
            }
        }
    }

}

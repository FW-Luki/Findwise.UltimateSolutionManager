using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Findwise.Configuration;
using Findwise.Configuration.TypeConverters;
using Findwise.Configuration.TypeEditors;

namespace SharepointSolutionPackageInstaller
{
    public class Configuration : ConfigurationBase, IBindableComponent
    {
        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IPackageConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [DefaultValue(null)]
        public IPackageConfiguration PackageConfiguration { get; set; }


        public Configuration()
        {
            DataBindings = new ControlBindingsCollection(this);
        }


        #region IBindableComponent Support
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings { get; }

        [System.Xml.Serialization.XmlIgnore, System.Runtime.Serialization.IgnoreDataMember]
        [Browsable(false)]
        public BindingContext BindingContext { get; set; } = new BindingContext();

        #region IComponent Support
        [Browsable(false)]
        public ISite Site { get; set; } = null;

        public event EventHandler Disposed;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Configuration() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
        #endregion
    }


    public interface IPackageConfiguration
    {
        string GetPackagePath();
    }

    [DisplayName("Direct package file")]
    public class DirectPackageFileConfiguration : ConfigurationBase, IPackageConfiguration
    {
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string WspPackagePath { get; set; }

        public string GetPackagePath()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    [DisplayName("Visual Studio project")]
    public class VisualStudioProjectFileConfiguration : ConfigurationBase, IPackageConfiguration
    {
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string VisualStudioProjectPath { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CompilerConfiguration CompilerSetup { get; set; } = new CompilerConfiguration();


        public string GetPackagePath()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return GetType().Name;
        }


        public class CompilerConfiguration : ConfigurationBase
        {
            [DefaultValue(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe")]
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            [HelpLink("https://msdn.microsoft.com/en-us/library/dd393574.aspx")]
            public string MsBuildExecutablePath { get; set; }

            [Editor(typeof(ApplicationHelpCommandEditor), typeof(UITypeEditor)), ApplicationHelpCommandEditor.Options(nameof(MsBuildExecutablePath))]
            public string MsBuildCommandArguments { get; set; }

            public override string ToString()
            {
                return GetType().Name;
            }
        }
    }

}

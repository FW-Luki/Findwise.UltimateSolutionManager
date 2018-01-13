using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Findwise.Configuration;
using Findwise.Configuration.TypeEditors;

namespace SharepointSolutionPackageInstaller
{
    public class Configuration : ConfigurationBase
    {
        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(IPackageConfiguration))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IPackageConfiguration PackageConfiguration { get; set; }
    }


    public interface IPackageConfiguration
    {
        string GetPackagePath();
    }

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
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            public string MsBuildExecutablePath { get; set; }

            public override string ToString()
            {
                return GetType().Name;
            }
        }
    }

}

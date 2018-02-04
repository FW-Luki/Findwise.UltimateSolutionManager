using Findwise.Configuration;
using Findwise.Configuration.TypeEditors;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedPropertiesCreator
{
    public class Configuration : ConfigurationBase, ISharepointInstallerModuleConfiguration
    {
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }
        [RefreshProperties(RefreshProperties.All)]
        [Editor(typeof(CsvLoaderEditor), typeof(UITypeEditor))]
        [HelpLink("https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.search.extended.administration.schema.managedproperty_properties(v=office.14).aspx")]
        public ManagedPropertyDefinition[] ManagedProperties { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagedPropertyDefinition : ConfigurationBase
        {
            public string Name { get; set; }
            public string[] Properties { get; set; }
            [Description("Basic, Business Data, Document Parser, Internal, Mail, MetadataExtractor, Notes, Office, People, SharePoint, Tiff, Web, XML")]
            public string CrawledPropertiesCategory { get; set; }
            private ManagedDataType _propertyType;
            [DefaultValue(ManagedDataType.Text)]
            public ManagedDataType PropertyType { get => _propertyType; set { if (value == ManagedDataType.Unsupported) throw new System.ArgumentException();  _propertyType = value; } }
            public bool Sort { get; set; }
            public bool Retrieve { get; set; }
            public bool Refine { get; set; }
            public bool Search { get; set; }
            public bool Multivalue { get; set; }
            public bool Query { get; set; }
            public bool Safe { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}

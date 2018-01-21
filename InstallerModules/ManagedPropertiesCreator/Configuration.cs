using Findwise.Configuration;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ManagedPropertyDefinition[] ManagedPropertyDefinitions { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagedPropertyDefinition
        {
            public string Name { get; set; }
            public string[] Properties { get; set; }
            [Description("Basic, Business Data, Document Parser, Internal, Mail, MetadataExtractor, Notes, Office, People, SharePoint, Tiff, Web, XML")]
            public string CrawledPropertiesCategory { get; set; }
            //[DefaultValue(typeof(Type), "Text")]  
            public Type PropertyType { get; set; } = Type.Text;
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
            public enum Type
            {
                Text = ManagedDataType.Text,
                Integer = ManagedDataType.Integer,
                Decimal = ManagedDataType.Decimal,
                DateTime = ManagedDataType.DateTime,
                YesNo = ManagedDataType.YesNo,
                Binary = ManagedDataType.Binary,
                Double = ManagedDataType.Double
            }
        }
    }
}

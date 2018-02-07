using Findwise.Configuration;
using Findwise.Configuration.TypeConverters;
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
using Findwise.SolutionInstaller.Core.Model;

namespace ManagedPropertiesCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        [Editor(typeof(CsvLoaderEditor), typeof(UITypeEditor))]
        [HelpLink("https://technet.microsoft.com/en-us/library/jj219667(v=office.16).aspx#proc2")]
        public ManagedPropertyDefinition[] ManagedProperties { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagedPropertyDefinition : ConfigurationBase
        {
            public string Name { get; set; }
            public string Description { get; set; }

            [DisplayName("Mappings to crawled properties")]
            [Description("A managed property can get its content from one or more crawled properties.")]
            [TypeConverter(typeof(StringArrayConverter))]
            public string[] Properties { get; set; }

            [Description("Basic, Business Data, Document Parser, Internal, Mail, MetadataExtractor, Notes, Office, People, SharePoint, Tiff, Web, XML")]
            public string CrawledPropertiesCategory { get; set; }
            private ManagedDataType _propertyType;

            [Description("Type of information that is stored in this property. ")]
            [DefaultValue(ManagedDataType.Text)]
            public ManagedDataType PropertyType { get => _propertyType; set { if (value == ManagedDataType.Unsupported) throw new System.ArgumentException(); _propertyType = value; } }

            [Description("Set whether this managed property can be sorted.")]
            public bool Sort { get; set; }

            [Description("Enabled: Enables sorting the result set based on the property before the result set is returned. Use for example for large result sets that cannot be sorted and retrieved at the same time.\n\nLatent: Enables switching sortable to active later, without having to do a full re - crawl when you switch.\n\nBoth options require a full crawl to take effect.")]
            public SortableType Sortable { get; set; }

            [Description("Enables the content of this managed property to be returned in search results. Enable this setting for managed properties that are relevant to present in search results.")]
            public bool Retrievable { get; set; }

            [Description("Yes - active: Enables using the property as a refiner for search results in the front end. You must manually configure the refiner in the web part.\n\nYes - latent: Enables switching refinable to active later, without having to do a full re - crawl when you switch.\n\nBoth options require a full crawl to take effect.")]
            public bool Refinable { get; set; }

            [Description("Enables querying against the content of the managed property. The content of this managed property is included in the full-text index. For example, if the property is 'author', a simple query for 'Smith' returns items containing the word 'Smith' and items whose author property contains 'Smith'.")]
            public bool Searchable { get; set; }

            [Description("Allow multiple values of the same type in this managed property. For example, if this is the 'author' managed property, and a document has multiple authors, each author name will be stored as a separate value in this managed property.")]
            [DisplayName("Allow multiple values")]
            public bool Multivalue { get; set; }

            [Description("Enables querying against the specific managed property. The managed property field name must be included in the query, either specified in the query itself or included in the query programmatically. If the managed property is 'author', the query must contain 'author: Smith'.")]
            public bool Queryable { get; set; }

            [Description("Safe for Anonymous: Enables this managed property to be returned for queries executed by anonymous users. Enable this setting for managed properties that do not contain sensitive information and are appropriate for anonymous users to view.")]
            public bool Safe { get; set; }

            [Description("Enables the system to extract company name entities from the managed property when crawling new or updated items. Afterwards, the extracted entities can be used to set up refiners in the web part.\n\nThere is a pre - populated dictionary for company name extraction.The system saves the original managed property content unchanged in the index and, in addition, copies the extracted entities to the managed property 'companies'.The 'companies' managed property is configured to be searchable, queryable, retrievable, sortable and refinable.")]
            [DisplayName("Company name extraction")]
            public bool CompanyExtraction { get; set; }

            [Description("Enable to return results independent of letter casing and diacritics(for example accented characters) used in the query.")]
            [DisplayName("Token Normalization")]
            [DefaultValue(true)]
            public bool TokenNormalization { get; set; }

            [Description("Queries will only be matched against the exact content of the property. For example, if you have a managed property 'ID' that contains the string '1 - 23 - 456#7', complete matching only returns results on the query ID:'1-23-456#7', and not on the queries ID:'1-23' or  ID:'1 23 456 7'.")]
            [DisplayName("Complete Matching")]
            public bool CompleteMatching { get; set; }

            [Description("Define an alias for a managed property if you want to use the alias instead of the managed property name in queries and in search results. Use the original managed property and not the alias to map to a crawled property. Use an alias if you don't want to or don't have permission to create a new managed property.")]
            public string[] Alias { get; set; }

            [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(ICustomEntityExtractionConfiguration))]
            [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
            [Description("Enables one or more custom entity extractors to be associated with this managed property. This enables the system to extract entities from the managed property when crawling new or updated items. Afterwards, the extracted entities can be used to set up refiners in the web part.\n\nThere are four types of custom extraction dictionaries. You create your own, separate custom entity extraction dictionaries that you deploy using the PowerShell cmdlet Import-SPEnterpriseSearchCustomExtractionDictionary.\n\nThe system saves the original managed property content unchanged in the index and, in addition, copies the extracted entities to the managed properties  'WordCustomRefiner1' through 5, 'WordPartCustomRefiner1' through 5, 'WordExactCustomRefiner' and / or 'WordPartExactCustomRefiner' respectively.\n\nThese managed properties are configured to be searchable, queryable, retrievable, sortable and refinable.")]
            [DisplayName("Custom entity extraction")]
            public ICustomEntityExtractionConfiguration CustomEntityExtractionConfiguration { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
    public interface ICustomEntityExtractionConfiguration
    {
        bool WordExtractionCustom1 { get; set; }
        bool WordExtractionCustom2 { get; set; }
        bool WordExtractionCustom3 { get; set; }
        bool WordExtractionCustom4 { get; set; }
        bool WordExtractionCustom5 { get; set; }
        bool WordPartExtractionCustom1 { get; set; }
        bool WordPartExtractionCustom2 { get; set; }
        bool WordPartExtractionCustom3 { get; set; }
        bool WordPartExtractionCustom4 { get; set; }
        bool WordPartExtractionCustom5 { get; set; }
        bool WordExactExtractionCustom { get; set; }
        bool WordPartExactExtractionCustom { get; set; }
    }
    [DisplayName("Word Extraction")]
    public class WordExtraction : ICustomEntityExtractionConfiguration
    {
        [DisplayName("Word Extraction - Custom1")]
        public bool WordExtractionCustom1 { get; set; }

        [DisplayName("Word Extraction - Custom2")]
        public bool WordExtractionCustom2 { get; set; }

        [DisplayName("Word Extraction - Custom3")]
        public bool WordExtractionCustom3 { get; set; }

        [DisplayName("Word Extraction - Custom4")]
        public bool WordExtractionCustom4 { get; set; }

        [DisplayName("Word Extraction - Custom5")]
        public bool WordExtractionCustom5 { get; set; }

        [DisplayName("Word Part Extraction - Custom1")]
        public bool WordPartExtractionCustom1 { get; set; }

        [DisplayName("Word Part Extraction - Custom2")]
        public bool WordPartExtractionCustom2 { get; set; }

        [DisplayName("Word Part Extraction - Custom3")]
        public bool WordPartExtractionCustom3 { get; set; }

        [DisplayName("Word Part Extraction - Custom4")]
        public bool WordPartExtractionCustom4 { get; set; }

        [DisplayName("Word Part Extraction - Custom5")]
        public bool WordPartExtractionCustom5 { get; set; }

        [DisplayName("Word Exact Extraction - Custom")]
        public bool WordExactExtractionCustom { get; set; }

        [DisplayName("Word Part Exact Extraction - Custom")]
        public bool WordPartExactExtractionCustom { get; set; }
    }

}

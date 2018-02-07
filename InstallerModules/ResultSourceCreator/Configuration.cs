using Findwise.Configuration;
using Findwise.Configuration.TypeConverters;
using Findwise.Configuration.TypeEditors;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionInstaller.Core.Model;

namespace ResultSourceCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        [DefaultValue("Search Service Application")]
        public string SearchApplicationName { get; set; }

        [HelpLink("https://technet.microsoft.com/en-us/library/jj683115.aspx#BKMK_CreateResutlSource")]
        [DisplayName("Result Source Name")]
        [Description("Names must be unique at each administrative level.")]
        public string ResultSourceName { get; set; }

        [Description("Descriptions are shown as tooltips when selecting result sources in other configuration pages.")]
        public string Description { get; set; }

        [Editor(typeof(DerivedClassEditor), typeof(UITypeEditor)), DerivedTypeEditor.Options(BaseType = typeof(ITypeConfiguration))]
        [TypeConverter(typeof(DisplayNameExpandableObjectConverter))]
        [Description("Select SharePoint Search Results to search over the entire index.\n\nSelect People Search Results to enable query processing specific to People Search, such as phonetic name matching or nickname matching.Only people profiles will be returned from a People Search source. ")]
        [DisplayName("Type")]
        public ITypeConfiguration TypeConfiguration { get; set; }

        [Description("Change incoming queries to use this new query text instead. Include the incoming query in the new text by using the query variable '{searchTerms}'.")]
        public string Query { get; set; }

        [DisplayName("Set as Default")]
        public bool SetAsDefault { get; set; }
    }
    public interface ITypeConfiguration
    {
        string Provider { get; }
    }

    [DisplayName("SharePoint Search Results")]
    public class SharePointSearchResults : ITypeConfiguration
    {
        [Browsable(false)]
        public string Provider { get; } = "Local SharePoint Provider";
    }
    [DisplayName("People Search Results")]
    public class PeopleSearchResults : ITypeConfiguration
    {
        [Browsable(false)]
        public string Provider { get; } = "Local People Provider";
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller.Models
{
    public class Project : ConfigurationBase
    {
        [XmlIgnore, IgnoreDataMember]
        public string Name { get; set; }


        public IInstallerModule[] Modules
        {
            get { return ModuleList.ToArray(); }
            set { ModuleList = new BindingList<IInstallerModule>(value.ToList()); }
        }

        [XmlIgnore, IgnoreDataMember]
        public BindingList<IInstallerModule> ModuleList { get; set; } = new BindingList<IInstallerModule>();


        public BindingItem[] BindingSources { get; set; }


        [XmlIgnore, IgnoreDataMember]
        public BindingList<BindingItem> BindingSourcesList { get; set; } = new BindingList<BindingItem>();
    }
}

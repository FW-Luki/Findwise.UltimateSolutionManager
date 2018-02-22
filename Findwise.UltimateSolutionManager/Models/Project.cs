using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Findwise.Configuration;

namespace Findwise.SolutionManager.Models
{
    [SerializationSurrogate(typeof(MySerializationSurrogate))]
    public class Project : ConfigurationBase
    {
        [XmlIgnore, IgnoreDataMember]
        public string Name { get; set; }


        [XmlElement(Order = 3), DataMember(Order = 3)]
        public IInstallerModule[] Modules
        {
            get { return ModuleList.ToArray(); }
            set { ModuleList = new BindingList<IInstallerModule>(value.ToList()); }
        }

        [XmlIgnore, IgnoreDataMember]
        public BindingList<IInstallerModule> ModuleList { get; set; } = new BindingList<IInstallerModule>();


        [XmlElement(Order = 1), DataMember(Order = 1)]
        public MasterConfig[] MasterConfigurations
        {
            get { return MasterConfigurationList.ToArray(); }
            set { MasterConfigurationList = new BindingList<MasterConfig>(value.ToList()); }
        }

        [XmlIgnore, IgnoreDataMember]
        public BindingList<MasterConfig> MasterConfigurationList { get; set; } = new BindingList<MasterConfig>();


        [XmlElement(Order = 2), DataMember(Order = 2)]
        public BindingItem[] BindingSources
        {
            get { return BindingSourceList.ToArray(); }
            set { BindingSourceList = new BindingList<BindingItem>(value.ToList()); }
        }

        [XmlIgnore, IgnoreDataMember]
        public BindingList<BindingItem> BindingSourceList { get; set; } = new BindingList<BindingItem>();



        private class MySerializationSurrogate : ISerializationSurrogate
        {
            private IEnumerable<XmlElementAttribute> GetXmlAttribs(PropertyInfo p) => p.GetCustomAttributes(false).OfType<XmlElementAttribute>();

            public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context)
            {
                var project = (Project)obj;
                foreach (var property in project.GetType().GetProperties().Where(p => GetXmlAttribs(p).Any()).OrderBy(p => GetXmlAttribs(p).First().Order))
                {
                    info.AddValue(property.Name, property.GetValue(project));
                }
            }

            public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                var project = (Project)obj;
                foreach (var property in project.GetType().GetProperties().Where(p => GetXmlAttribs(p).Any()).OrderBy(p => GetXmlAttribs(p).First().Order))
                {
                    property.SetValue(project, info.GetValue(property.Name, property.PropertyType));
                }
                return null;
            }
        }
    }
}

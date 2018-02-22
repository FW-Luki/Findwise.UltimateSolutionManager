using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Configuration;
using Findwise.SolutionManager.Core.Model;

namespace DatabaseCreator
{
    public class Configuration : BindableConfiguration
    {
        [Bindable(true)]
        public string ServerName { get => Prop.Get<string>(); set => Prop.Set(value); }

        [Bindable(true)]
        public string DatabaseName { get => Prop.Get<string>(); set => Prop.Set(value); }

        [Bindable(true)]
        public string Tablename { get => Prop.Get<string>(); set => Prop.Set(value); }

        public DbColumn[] Columns { get => Prop.Get<DbColumn[]>(); set => Prop.Set(value); }


        public Configuration()
        {
        }



        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class DbColumn
        {
            public string Name { get; set; }
            public string TypeName { get; set; }
            public bool IsIdentity { get; set; }
            public bool IsNullable { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }

}

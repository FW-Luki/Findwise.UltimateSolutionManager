using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace DatabaseCreator
{
    public class Configuration : ConfigurationBase
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Tablename { get; set; }
        public DbColumn[] Columns { get; set; }

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

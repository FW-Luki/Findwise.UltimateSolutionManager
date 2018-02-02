using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.PluginManager
{
    public sealed class PluginSerializationBinder : SerializationBinder
    {
        //ToDo: It happens that typeName is "...[]" which means it's an array. It also happens that assemblyName is "0". 
        //In both cases returned type is null, but I didn't notice any harmful impact of this bahavior.
        public override Type BindToType(string assemblyName, string typeName)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
            {
                try
                {
                    return a.GetTypes();
                    //return a.GetExportedTypes();
                }
                catch
                {
                    return Enumerable.Empty<Type>();
                }
            }).FirstOrDefault(t => t.FullName == typeName && t.Assembly.FullName == assemblyName);
            return type;
        }
    }
}

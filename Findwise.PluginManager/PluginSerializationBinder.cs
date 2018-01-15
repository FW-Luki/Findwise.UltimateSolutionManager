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
        public override Type BindToType(string assemblyName, string typeName)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
            {
                try
                {
                    return a.GetExportedTypes();
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

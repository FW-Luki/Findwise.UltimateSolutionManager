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
        private Dictionary<AssemblyQualifiedTypeName, Type> typeCache = new Dictionary<AssemblyQualifiedTypeName, Type>(new AssemblyQualifiedTypeName.EqualityComparer());

        //ToDo: It happens that typeName is "...[]" which means it's an array. It also happens that assemblyName is "0". 
        //In both cases returned type is null, but I didn't notice any harmful impact of this bahavior.
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type;

            if (typeCache.TryGetValue(new AssemblyQualifiedTypeName(assemblyName, typeName), out type))
            {
                return type;
            }
            else
            {
                type = Type.GetType(typeName);
                if (type == null)
                {
                    type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                    {
                        try
                        {
                            return a.GetTypes(); //return a.GetExportedTypes();
                        }
                        catch
                        {
                            return Enumerable.Empty<Type>();
                        }
                    }).FirstOrDefault(t => t.FullName == typeName && t.Assembly.FullName == assemblyName);
                }
                typeCache.Add(new AssemblyQualifiedTypeName(assemblyName, typeName), type);
                return type;
            }
        }


        private class AssemblyQualifiedTypeName
        {
            public string AssemblyName { get; }
            public string TypeName { get; }

            public AssemblyQualifiedTypeName(string assemblyName, string typeName)
            {
                AssemblyName = assemblyName;
                TypeName = typeName;
            }

            public class EqualityComparer : IEqualityComparer<AssemblyQualifiedTypeName>
            {
                public bool Equals(AssemblyQualifiedTypeName x, AssemblyQualifiedTypeName y)
                {
                    return x.TypeName.Equals(y.TypeName) && x.AssemblyName.Equals(y.AssemblyName);
                }

                public int GetHashCode(AssemblyQualifiedTypeName obj)
                {
                    unchecked
                    {
                        int result = 17;
                        result = result * 31 + obj?.TypeName?.GetHashCode() ?? 0;
                        result = result * 31 + obj?.AssemblyName?.GetHashCode() ?? 0;
                        return result;
                    }
                }
            }
        }
    }
}

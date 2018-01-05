using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.PluginManager
{
    public class AssemblyLoader
    {
        public static AssemblyLoadResult LoadClassesFromFile<TBase>(string filename, out IEnumerable<Type> validTypes)
        {
            try
            {
                var assembly = Assembly.LoadFile(filename);
                validTypes = assembly.GetTypes().Where(t => typeof(TBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
                return validTypes.Any() ? AssemblyLoadResult.Success : AssemblyLoadResult.InterfaceNotImplemented;
            }
            catch
            {
                validTypes = null;
                return AssemblyLoadResult.NotManagedAssembly;
            }
        }

        public static IEnumerable<NamedAssemblyLoadResult> LoadClassesFromFolder<TBase>(string path, string filter, SearchOption includeSubfolders, out IEnumerable<Type> validTypes)
        {
            var loadResults = new List<NamedAssemblyLoadResult>();
            var returnedTypes = new List<Type>();
            foreach (var filename in Directory.GetFiles(path, filter, includeSubfolders))
            {
                var result = LoadClassesFromFile<TBase>(filename, out IEnumerable<Type> types);
                loadResults.Add(new NamedAssemblyLoadResult(filename, result));
                returnedTypes.AddRange(types);
            }
            validTypes = returnedTypes.AsEnumerable();
            return loadResults.AsEnumerable();
        }


        public static IEnumerable<Type> LoadClassesFromFile<TBase>(string filename)
        {
            try
            {
                var assembly = Assembly.LoadFile(filename);
                return assembly.GetTypes().Where(t => typeof(TBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            }
            catch
            {
                return null;
            }
        }
    }
}

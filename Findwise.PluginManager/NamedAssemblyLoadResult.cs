using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.PluginManager
{
    public class NamedAssemblyLoadResult
    {
        public string AssemblyFilename { get; }
        public AssemblyLoadResult AssemblyLoadResult { get; }
        public NamedAssemblyLoadResult(string assemblyFilename, AssemblyLoadResult assemblyLoadResult)
        {
            AssemblyFilename = assemblyFilename;
            AssemblyLoadResult = assemblyLoadResult;
        }
    } }

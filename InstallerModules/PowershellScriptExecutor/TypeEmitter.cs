using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PowershellScriptExecutor
{
    internal class TypeEmitter
    {
        public Type BuildType(string name, Type baseType, params PropertyConstructor[] properties)
        {
            //TypeBuilder builder = new TypeBuilder();

            return typeof(object);
        }

        public class PropertyConstructor
        {
            public string Name { get; set; }
            public Type Type { get; set; }
            public AttributeCollection Attributes { get; set; }
        }
    }
}

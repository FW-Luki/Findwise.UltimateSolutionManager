using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.PluginManager.Properties;

namespace Findwise.PluginManager
{
    public enum AssemblyLoadResult
    {
        //[Description("")]
        [AmbientValue(nameof(Resources.if_okay_14617))]
        Success = 0,

        [Description("Not a managed assembly or load error")]
        [AmbientValue(nameof(Resources.if_error_do_not_14413))]
        NotManagedAssembly,

        [Description("No classes implementing specified interface found")]
        [AmbientValue(nameof(Resources.if_error_fuck_14415))]
        InterfaceNotImplemented,

        [Description("Assembly is not trusted")]
        [AmbientValue(nameof(Resources.if_warning_triangle_14684))]
        NotTrustedAssembly
    }
}

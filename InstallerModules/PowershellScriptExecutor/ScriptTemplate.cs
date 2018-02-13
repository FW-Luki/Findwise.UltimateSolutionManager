using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowershellScriptExecutor
{
    internal class ScriptTemplate
    {
        public const string OnInstallCommandName = "OnInstall";
        public const string OnUninstallCommandName = "OnUninstall";
        public const string OnCheckStatusCommandName = "OnCheckStatus";

        public string GetScriptTemplate(params Parameter[] parameters)
        {
            return @"";
        }
    }
}

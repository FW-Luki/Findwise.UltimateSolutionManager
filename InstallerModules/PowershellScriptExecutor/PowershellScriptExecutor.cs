using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Findwise.Sharepoint.SolutionInstaller;
using log4net;
using PowershellScriptExecutor.Properties;

namespace PowershellScriptExecutor
{
    [Category(ModuleCategoryNames.Administration)]
    public class PowershellScriptExecutor : InstallerModuleBase, ISaveLoadAware
    {
        private readonly ILog logger;

        public override string Name => "PowerShell Script Executor";

        public override Image Icon => Resources.if_utilities_terminal_15359;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }


        public PowershellScriptExecutor()
        {
            logger = LogManager.GetLogger(GetType());
        }


        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.NotInstalled;
        }

        public override void Install()
        {
            var results = RunScript("OnInstall");
            LogResults(results);
        }

        public override void Uninstall()
        {
        }


        public void BeforeSave()
        {
        }

        public void AfterSave()
        {
        }

        public void AfterLoad()
        {
        }


        private System.Collections.ObjectModel.Collection<PSObject> RunScript(string command)
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                var shell = PowerShell.Create();
                shell.Runspace = runspace;

                shell.AddScript(myConfiguration.ScriptContents);
                shell.Invoke();

                shell.AddCommand(command);
                foreach (var param in myConfiguration.Parameters)
                {
                    shell.AddParameter(param.Name, param.Value);
                }

                shell.AddCommand("Out-String");
                return shell.Invoke(/*"Set-ExecutionPolicy Unrestricted -Scope Process"*/);
            }

            //Runspace runspace = RunspaceFactory.CreateRunspace();
            //runspace.Open();

            //Pipeline pipeline = runspace.CreatePipeline();
            //pipeline.Commands.AddScript(myConfiguration.ScriptContents);

            //foreach (var param in myConfiguration.Parameters)
            //{
            //    pipeline.Commands[0].Parameters.Add(param.Name, param.Value);
            //}
            //pipeline.Invoke();

            //pipeline.Commands.Add(command);
            //pipeline.Commands.Add("Out-String");

            //// execute the script

            //return pipeline.Invoke("Set-ExecutionPolicy Unrestricted -Scope Process");

            //// close the runspace

            //runspace.Close();

            //// convert the script result into a single string

            ////StringBuilder stringBuilder = new StringBuilder();
            ////foreach (PSObject obj in results)
            ////{
            ////    stringBuilder.AppendLine(obj.ToString());
            ////}

            ////return stringBuilder.ToString();
        }

        private void LogResults(IEnumerable<PSObject> psObjects)
        {
            foreach (var psobj in psObjects)
            {
                logger.Info(psobj.BaseObject?.ToString());
            }
        }
    }
}

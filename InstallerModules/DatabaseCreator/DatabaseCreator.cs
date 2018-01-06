using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCreator.Properties;
using Findwise.Configuration;
using Findwise.Sharepoint.SolutionInstaller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseCreator
{
    public class DatabaseCreator : InstallerModuleBase
    {
        public override string Name => "Database Creator";

        public override Image Icon => Resources.if_server_11124;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }


        public override void CheckStatus()
        {
            try
            {
                var cmd = new System.Data.Odbc.OdbcCommand("select case when exists((select * from information_schema.tables where table_name = @tablename)) then 1 else 0 end");
                cmd.Parameters.Add(new System.Data.Odbc.OdbcParameter("tablename", myConfiguration.Tablename));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    Status = InstallerModuleStatus.Installed;
                }
                else
                {
                    Status = InstallerModuleStatus.Uninstalling;
                }
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
            }
        }

        public override void Install()
        {
            throw new NotImplementedException();
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}

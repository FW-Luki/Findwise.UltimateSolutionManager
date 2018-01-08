using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                if ((int)ExecuteScalar(GetConnectionString(myConfiguration.DatabaseName, myConfiguration.Tablename),
                                       "select case when exists((select * from information_schema.tables where table_name = @TableName)) then 1 else 0 end",
                                       new Parameter("TableName", myConfiguration.Tablename)) == 1)
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
                throw;
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


        private static object ExecuteScalar(string connectionString, string query, params Parameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection)
            {
            })
            {
                if (parameters != null && parameters.Length > 0) command.Parameters.AddRange(parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray());
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        private static string GetConnectionString(string dataSource, string initialCatalog)
        {
            return string.Format("data source={0};initial catalog={1};integrated security=True;MultipleActiveResultSets=True;", dataSource, initialCatalog);
        }


        public class Parameter
        {
            public string Name { get; private set; }
            public object Value { get; private set; }
            public Parameter(string name, object value)
            {
                Name = name;
                Value = value;
            }
        }

    }
}

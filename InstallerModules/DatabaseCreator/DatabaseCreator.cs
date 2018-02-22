using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCreator.Properties;
using Findwise.Configuration;
using Findwise.SolutionManager;
using Findwise.InstallerModule;
using System.ComponentModel;

namespace DatabaseCreator
{
    [Category(ModuleCategoryNames.Administration)]
    public class DatabaseCreator : InstallerModuleBase
    {
        private string ConnectionString
        {
            get
            {
                return $@"data source={myConfiguration?.ServerName};initial catalog={myConfiguration?.DatabaseName};integrated security=SSPI;MultipleActiveResultSets=True;Trusted_Connection=True";
            }
        }


        public override string Name => "Database Creator";

        public override Image Icon => Resources.if_server_11124;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }


        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                if ((int)ExecuteScalar("select case when exists((select * from information_schema.tables where table_name = @TableName)) then 1 else 0 end",
                                       new Parameter("TableName", myConfiguration.Tablename)) == 1)
                {
                    Status = InstallerModuleStatus.Installed;
                }
                else
                {
                    Status = InstallerModuleStatus.NotInstalled;
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
            try
            {
                var query = $@"CREATE TABLE [dbo].[{myConfiguration.Tablename}]({string.Join(",", myConfiguration.Columns.Select(c => $"[{c.Name}] {c.TypeName}{(c.IsIdentity ? " IDENTITY(1,1)" : "")}{(c.IsNullable ? "" : " NOT NULL")}"))})";
                ExecutNonQuery(query);
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }

        public override void Uninstall()
        {
            Status = InstallerModuleStatus.Uninstalling;
            throw new NotImplementedException($"Due to security reason, this module doesn't support uninstalling. To uninstall it, remove table {myConfiguration.Tablename} manually.");
            //try
            //{
            //    ExecutNonQuery("drop table @TableName", new Parameter("TableName", myConfiguration.Tablename));
            //}
            //catch
            //{
            //    Status = InstallerModuleStatus.Error;
            //    throw;
            //}
        }


        private object ExecuteScalar(string query, params Parameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection)
                {
                })
                {
                    if (parameters != null && parameters.Length > 0) command.Parameters.AddRange(parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray());
                    return command.ExecuteScalar();
                }
            }
        }

        private int ExecutNonQuery(string query, params Parameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection)
                {
                })
                {
                    if (parameters != null && parameters.Length > 0) command.Parameters.AddRange(parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray());
                    return command.ExecuteNonQuery();
                }
            }
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

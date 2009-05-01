using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
namespace Kona.Infrastructure {



    public class DbCommon {
        string connString = "";
        public DbCommon(string connectionStringName) {
            connString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            string providerName = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            if (string.IsNullOrEmpty(providerName))
                providerName = "System.Data.SqlClient";
            _factory = DbProviderFactories.GetFactory(providerName);
        }
        private DbProviderFactory _factory;
        public DbConnection CreateConnection() {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();
            return conn;
        }

        public DbCommand CreateCommand(string sql) {
            DbConnection conn = CreateConnection();
            DbCommand cmd = _factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            return cmd; ;
        }
        public void AddParam(DbCommand cmd, string name, object value) {
            AddParam(cmd, name, DbType.Object);
        }
        public void AddParam(DbCommand cmd, string name, object value, DbType dbType) {
            
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            param.DbType = dbType;
            cmd.Parameters.Add(param);
        }

        public void ExecuteTransaction(SortedList<int,DbCommand> commands) {

            using (var conn = CreateConnection()) {
                using (DbTransaction trans = conn.BeginTransaction()) {
                    foreach (var cmd in commands.Values) {
                        cmd.Transaction = trans;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                conn.Close();
            }

        }

        public void ExecuteTransaction(params DbCommand[] commands) {
            
            using (var conn = CreateConnection()) {
                using (DbTransaction trans = conn.BeginTransaction()) {
                    foreach (var cmd in commands) {
                        cmd.Transaction = trans;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                conn.Close();
            }
            
        }
    }
}

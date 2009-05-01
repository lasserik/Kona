using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Kona.Infrastructure {
    public static class DbExtensions {

        public static DbParameter AddParameter(this DbCommand cmd, string name, object value) {

            return AddParameter(cmd, name, value, DbType.Object);

        }
        public static DbParameter AddParameter(this DbCommand cmd, string name, object value, DbType dbType) {

            DbParameter param = cmd.CreateParameter();

            param.ParameterName = name;

            param.Value = value;

            param.DbType = dbType;

            cmd.Parameters.Add(param);

            return param;

        }
    }
}

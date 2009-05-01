using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Common;

namespace Kona.Infrastructure {
    //TDOD: Clean this up :)
    public class ObjectStore : IObjectStore  {
        DbCommon _db;
        public ObjectStore() {
            _db = new DbCommon("ObjectStore");
        }
        int storeTries = 0;
        public void Store<T>(string key, string searchString, T value) where T : class, new() {
            
            //serialize to binaryasdd
            byte[] bytes = value.ToBinary();
            string sql = @"
IF NOT EXISTS(SELECT ID FROM ObjectStore WHERE ObjectKey=@Key AND SearchString=@SearchString )
            INSERT INTO ObjectStore(ObjectKey,SearchString,SystemType,Data)
            VALUES(@Key,@SearchString,@Type,@Data)
ELSE
            UPDATE ObjectStore SET ModifiedOn=getdate(),Data=@Data
            WHERE ObjectKey=@Key AND SearchString=@SearchString 

";

            using (DbCommand cmd = _db.CreateCommand(sql)) {

                _db.AddParam(cmd, "@Key", key, DbType.String);
                _db.AddParam(cmd, "@SearchString", searchString, DbType.String);
                _db.AddParam(cmd, "@Type", typeof(T).Name, DbType.String);
                _db.AddParam(cmd, "@Data", bytes, DbType.Binary);

                //execute
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

        }

        public void Store<T>(string key, T value) where T : class, new() {
            Store<T>(key, "", value);
        }

        public void Delete(string key, string searchString) {
            string sql = @"DELETE FROM ObjectStore WHERE ObjectKey=@key AND searchString=@searchString";

            using (DbCommand cmd = _db.CreateCommand(sql)) {
                _db.AddParam(cmd, "@Key", key, DbType.String);
                _db.AddParam(cmd, "@searchString", searchString, DbType.String);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(string key) {
            string sql = @"DELETE FROM ObjectStore WHERE ObjectKey=@key";

            using (DbCommand cmd = _db.CreateCommand(sql)) {
                _db.AddParam(cmd, "@Key", key, DbType.String);
                cmd.ExecuteNonQuery();
            }
        }
        public T Get<T>( string key) where T : class, new() {
            return Get<T>(key, "");
        }

        public IList<T> GetList<T>(string key) where T : class, new() {
            List<T> result = new List<T>();
            string sql = @"SELECT * FROM ObjectStore WHERE ObjectKey=@Key";
            byte[] bits = new byte[0];
            string sType = "";

            using (DbCommand cmd = _db.CreateCommand(sql)) {
                _db.AddParam(cmd, "@Key", key, DbType.String);
                //execute
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read()) {
                    sType = rdr["SystemType"].ToString();
                    bits = (byte[])rdr["Data"];


                    if (bits.Length > 0) {

                        //make sure the types match
                        if (!typeof(T).Name.Equals(sType)) {
                            throw new InvalidDataException("The type requested (" + typeof(T).Name + ") doesn't match the type stored: " + sType);
                        }

                        T item = default(T);
                        item = item.FromBinary(bits);
                        result.Add(item);
                    }


                }
                rdr.Close();
            }
            return result;

        }

        public T Get<T>(string key, string searchString) where T : class, new() {
            
            T result = default(T);
            //HACK: Select * Isn't good for perf - but the ObjectStore shouldn't be a high-search/hit
            string sql = @"SELECT * FROM ObjectStore WHERE ObjectKey=@Key AND SearchString=@SearchString";
            if (String.IsNullOrEmpty(searchString))
                sql = @"SELECT * FROM ObjectStore WHERE ObjectKey=@Key";
            
            byte[] bits=new byte[0];
            string sType="";

            using (DbCommand cmd = _db.CreateCommand(sql)) {
                _db.AddParam(cmd, "@Key", key, DbType.String);
                if(!string.IsNullOrEmpty(searchString))
                    _db.AddParam(cmd, "@searchString", searchString, DbType.String);

                //execute
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.Read()) {
                    sType = rdr["SystemType"].ToString();
                    bits = (byte[])rdr["Data"];

                }
                rdr.Close();
            }

            if (bits.Length > 0) {

                //make sure the types match
                if (!typeof(T).Name.Equals(sType)) {
                    throw new InvalidDataException("The type requested ("+typeof(T).Name+") doesn't match the type stored: "+sType);
                }
                
                try {
                    result = result.FromBinary(bits);
                } catch {
                    //if there's an error then the serialization was bad
                    //kill it
                    Delete(key, searchString);
                }
            }

            return result;
        }

    }

}

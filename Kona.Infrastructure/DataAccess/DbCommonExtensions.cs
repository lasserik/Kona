using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Kona.Infrastructure {
    public static class DbCommonExtensions {
        public static void Load<T>(this IDataReader rdr, T item) where T : new() {
            Type iType = typeof(T);

            //cache property info so we're not banging on reflection in a long loop
            PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
            FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];

            for (int i = 0; i < rdr.FieldCount; i++) {
                string pName = rdr.GetName(i);
                PropertyInfo pi = iType.GetProperty(pName);
                if (pi != null) {
                    cachedProps[i] = pi;
                } else {
                    FieldInfo fi = iType.GetField(pName);
                    cachedFields[i] = fi;
                }
            }
            //set the values        
            PropertyInfo prop;
            FieldInfo field;
            for (int i = 0; i < rdr.FieldCount; i++) {
                prop = cachedProps[i];
                field = cachedFields[i];
                if (prop != null && !DBNull.Value.Equals(rdr.GetValue(i)))
                    prop.SetValue(item, rdr.GetValue(i), null);
                else if (field != null && !DBNull.Value.Equals(rdr.GetValue(i))) {
                    object value = rdr.GetValue(i);
                    Type t = value.GetType();
                    if (t == typeof(SByte)) {
                        bool setting = value.ToString() == "0";
                        field.SetValue(item, setting);
                    } else {
                        //Type toFieldType = field.FieldType;
                        //value.ChangeTypeTo(toFieldType);
                        field.SetValue(item, value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a typed list from a DataReader
        /// </summary>
        public static List<T> ToList<T>(this IDataReader rdr) where T : new() {
            List<T> result = new List<T>();
            Type iType = typeof(T);

            //cache property info so we're not banging on reflection in a long loop
            PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
            FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];

            for (int i = 0; i < rdr.FieldCount; i++) {
                string pName = rdr.GetName(i);
                PropertyInfo pi = iType.GetProperty(pName);
                if (pi != null) {
                    cachedProps[i] = pi;
                } else {
                    FieldInfo fi = iType.GetField(pName);
                    cachedFields[i] = fi;
                }
            }
            //set the values        
            PropertyInfo prop;
            FieldInfo field;
            while (rdr.Read()) {
                T item = new T();
                rdr.Load(item);
                result.Add(item);
            }
            return result;
        }
    }
}

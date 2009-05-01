using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Kona.Infrastructure {
    public static  class Serializer {

        public static byte[] ToBinary<T>(this T o) where T: class, new() {
            byte[] bytes = null;
            DataContractSerializer dc = new DataContractSerializer(typeof(T));

            
            using (MemoryStream ms = new MemoryStream()) {
                //formatter.Serialize(ms, value);
                dc.WriteObject(ms, o);
                ms.Seek(0, 0);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public static TResult FromBinary<TResult>(this TResult input, byte[] bits) where TResult : class, new() {
            TResult result = default(TResult);
            DataContractSerializer dc = new DataContractSerializer(typeof(TResult));
            //IFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bits)) {
                result = (TResult)dc.ReadObject(ms);
            }

            return result;
        }

    }
}

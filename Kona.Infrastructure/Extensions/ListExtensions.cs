using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kona.Infrastructure {
    public static class ListExtensions {
        public static IList<T> RemoveDuplicates<T>(this IList<T> list) where T:class{
            IList<T> result = new List<T>();
            foreach (var item in list) {
                if (!result.Contains(item))
                    result.Add(item);
            }
            return result;
        }
    }
}

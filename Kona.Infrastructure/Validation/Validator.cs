using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kona.Infrastructure {
    public static class Validator {

        public static void CheckNullOrEmptyString<T>(Func<T, string> expression, T item) where T : class {
            string result = expression.Invoke(item);
            if (string.IsNullOrEmpty(result)) {
                throw new InvalidOperationException(typeof(T).Name + " cannot be null or empty");
            }
        }

        public static void CheckNull<T>(Func<T, object> expression, T item) where T : class {
            object result = expression.Invoke(item);
            if (result==null) {
                throw new InvalidOperationException(typeof(T).Name + " cannot be null or empty");
            }
        }
        public static void CheckNull(object item, string message) {
            if (item == null) {
                throw new InvalidOperationException(message);
            }
        }
        public static void CheckGreaterThan<T>(Func<T, decimal> expression, T item, decimal minValue) where T : class {
            decimal result = expression.Invoke(item);
            if (result < minValue) {
                throw new InvalidOperationException(item.ToString() + " must be greater than "+minValue);
            }
        }

    }
}

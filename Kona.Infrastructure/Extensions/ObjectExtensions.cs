using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kona.Infrastructure {
    public static class ObjectExtensions {

        /// <summary>
        /// Returns an Object with the specified Type and whose value is equivalent to the specified object.
        /// </summary>
        /// </remarks>
        public static object ChangeType(this object value, Type conversionType) {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
                throw new ArgumentNullException("conversionType");

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                if (value == null)
                    return null;
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return Convert.ChangeType(value, conversionType);
        }

    }
}

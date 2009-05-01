using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Kona.Data {
    public partial class Address {

        public string FullName {
            get {
                return FirstName + " " + LastName;
            }

        }

        public static Address SaveIfNotExists(Address address) {

            if (!Address.Exists(x => x.UserName == address.UserName &&
                x.Street1 == address.Street1 &&
                x.StateOrProvince == address.StateOrProvince)) {

                address.Add(address.UserName);

            }

            return address;

        }

        /// <summary>
        /// Formats an Address in a Postal-service, readable way
        /// </summary>
        /// <returns>System.String</returns>       
        public string ToReadable() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1}\r\n", this.FirstName, this.LastName);
            sb.AppendLine(this.Street1);
            if (!String.IsNullOrEmpty(this.Street2))
                sb.AppendLine(this.Street2);

            sb.AppendLine(this.City + ", " + this.StateOrProvince + " " + this.Zip + ", " + this.Country);
            return sb.ToString();
        }

    }
}

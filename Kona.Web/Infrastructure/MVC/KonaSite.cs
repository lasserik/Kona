using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Infrastructure;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Kona.Web {
    [DataContract()]
    public class KonaSite:Site {
        public KonaSite():base() {
            this.Plugins = new List<Plugin>();
        }
        [DataMember]
        public bool AcceptPayPal { get; set; }
        [DataMember]
        public string PayPalBusinessEmail { get; set; }
        [DataMember]
        public string PayPalPDTToken { get; set; }

        [DataMember]
        public bool AcceptCreditCards { get; set; }
        [DataMember]
        public bool AcceptPOs { get; set; }
        [DataMember]
        public string CreditCardProcessor { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }


        [DataMember]
        public string AddressForm { get; set; }
        [DataMember]
        public string CreditCardForm { get; set; }
        [DataMember]
        public string PayPalForm { get; set; }
        [DataMember]
        public string OrderHeaderView { get; set; }
        [DataMember]
        public string OrderDetailView { get; set; }

        [DataMember]
        public string TaxPlugin { get; set; }
        [DataMember]
        public string ShippingPlugin { get; set; }
        [DataMember]
        public IList<Plugin> Plugins { get; set; }

    }
}

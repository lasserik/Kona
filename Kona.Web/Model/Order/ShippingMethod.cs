using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class ShippingMethod {

        public decimal BaseRate { get; set; }
        public decimal RatePerPound { get; set; }
        public string ServiceName { get; set; }


        public decimal CalculateCost(ShoppingCart cart) {
            _cost = this.BaseRate + (this.RatePerPound * cart.Items.Sum(x => x.Product.WeightInPounds) * cart.Items.Count);
            return _cost;
        }

        decimal _cost;
        public decimal Cost {
            get {
                return _cost;
            }
        }

        public string Display {
            get {
                return this.ServiceName + " (" + this.Cost.ToString("C") + ")";
            }
        }

    }
}

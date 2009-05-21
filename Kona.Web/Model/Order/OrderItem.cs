using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class OrderItem {
        public decimal LineTotal {
            get {
                return this.Item.BasePrice * Quantity;
            }
        }
        public Product Item { get; set; }

    }
}

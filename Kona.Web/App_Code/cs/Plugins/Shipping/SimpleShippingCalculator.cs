using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Infrastructure;
using Kona.Data;

namespace Kona.Web.Plugins {
    
    public class SimpleShippingCalculator:Plugin {
        
        public IList<ShippingMethod> CalculateShipping(ShoppingCart cart) {
            //pull the rates
            var shippingMethods = ShippingMethod.All().ToList();

            //loop the methods and set the cost
            shippingMethods.ForEach(x => x.CalculateCost(cart));
            return shippingMethods;
        }
    }
}

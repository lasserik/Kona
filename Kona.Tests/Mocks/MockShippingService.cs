using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kona.Model;

namespace Tests.Mocks {
    public class MockShippingService:IShippingService {
        #region IShippingService Members

        public Shipment CalculateShipping(Order order) {

            Shipment result = new Shipment(order);
            result.ShippingOptions = new List<ShippingMethod>();
            result.ShippingOptions.Add(new ShippingMethod(1,"Test Carrier", "Overnight", 10, 1,5));
            result.ShippingOptions.Add(new ShippingMethod(2,"Test Carrier", "Next Day", 5, 2,10));
            result.ShippingOptions.Add(new ShippingMethod(3,"Test Carrier", "Ground", 2,  5,20));
            return result;
        }

        #endregion
    }
}

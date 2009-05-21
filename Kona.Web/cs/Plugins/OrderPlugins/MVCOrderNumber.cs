using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Infrastructure;
using Kona.Data;

namespace Kona.Web.Plugins {
    public class MVCOrderNumber:Plugin {

        public string CreateOrderNumber(Order order) {
            return  "MVC-"+Guid.NewGuid().ToString().Substring(0, 8);
        }

    }
}

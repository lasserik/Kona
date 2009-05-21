using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Data;
using Kona.Infrastructure;
using System.Web.Mvc;

    public static class CustomerExtensions {

        public static Customer CurrentCustomer(this ViewMasterPage pg) {
            return CurrentCustomer(pg.Page as IViewDataContainer);
        }

        public static Customer CurrentCustomer(this IViewDataContainer pg) {
            //this will never be null
            var result = (Customer)pg.ViewData["CurrentCustomer"];

            return result;
        }

        public static ShoppingCart CurrentCart(this ViewMasterPage pg) {
            return CurrentCart(pg.Page as IViewDataContainer);
        }
        public static ShoppingCart CurrentCart(this IViewDataContainer pg) {
            return CurrentCustomer(pg).Cart;
        }


    }

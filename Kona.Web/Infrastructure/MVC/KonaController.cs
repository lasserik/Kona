using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kona.Data;
using Kona.Infrastructure;

namespace Kona.Web {
    public class KonaController:Controller {

        string _themeName;
        public string ThemeName {
            get {
                if (String.IsNullOrEmpty(_themeName))
                    _themeName = "Default";
                return _themeName;
            }
            set {
                _themeName = value;

            }
        }

        IList<ShippingMethod> _shippingMethods;
        public IList<ShippingMethod> ShippingMethods {
            get {
                return _shippingMethods;
            }
            set {
                _shippingMethods = value;
                ViewData["ShippingMethods"] = _shippingMethods;
            }
        }

        public IList<Category> Categories {
            get {

                return (IList<Category>)ViewData["Categories"];
            }
            set {
                ViewData["Categories"] = value;
            }
        }

        public KonaSite SiteData {
            get {

                return (KonaSite)ViewData["SiteData"];
            }
            set {
                ViewData["SiteData"] = value;
            }
        }

        public ShoppingCart CurrentCart {
            get {

                return CurrentCustomer.Cart;
            }
            set {
                ViewData["CurrentCart"] = CurrentCustomer.Cart;
            }
        }
        public Category CurrentCategory {
            get {
                return (Category)ViewData["CurrentCategory"];
            }
            set {
                ViewData["CurrentCategory"] = value;
            }
        }
        public Customer CurrentCustomer {
            get {
                return (Customer)ViewData["CurrentCustomer"];
            }
            set {
                ViewData["CurrentCustomer"] = value;
            }
        }
        IList<Product> _products;
        public IList<Product> Products {
            get {
                return _products;
            }
            set {
                _products = value;
                ViewData["Products"] = _products;
            }
        }

        Product _product;
        public Product SelectedProduct {
            get {
                return _product;
            }
            set {
                _product = value;
                ViewData["SelectedProduct"] = _product;
            }
        
        }

    }
}

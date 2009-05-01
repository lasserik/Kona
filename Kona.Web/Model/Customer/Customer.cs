using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class Customer {

        public ShoppingCart Cart { get; set; }
        
        public void LoadCart() {
            this.Cart = new ShoppingCart(this.UserName);

           
            //load up the cart
            //a cart is just an Order, marked with "NotCheckedOut"
            //the following factory method will retrieve the current Order
            //or create a new one
            var existingOrder = Order.FindCurrentOrCreateNew(this.UserName);

            if (existingOrder != null) {
                //get the items
                //pull the products first
                var skus = from items in existingOrder.OrderItems
                           select items.SKU;

                var products = from p in Product.All()
                               join o in OrderItem.All() on p.SKU equals o.SKU
                               select p;

                foreach (var item in existingOrder.OrderItems) {
                    this.Cart.AddItem(products.SingleOrDefault(x => x.SKU == item.SKU), item.Quantity, item.DateAdded);
                }
            }

        }
        public string FullName {
            get {
                return this.First + " " + this.Last;
            }
        }
        Address _defaultAddress;
        public Address DefaultAddress {
            get {
                if (_defaultAddress == null) {
                    if(this.Addresses.Count() >0){
                        _defaultAddress = this.Addresses.SingleOrDefault(x => x.IsDefault) ?? this.Addresses.First();
                    }
                }
                return _defaultAddress;
            }
            set {
                _defaultAddress = value;
            }
        }

        public static Customer GetExistingOrCreate(string userName) {
            var result = Customer.SingleOrDefault(x => x.UserName == userName);
            if (result == null) {
                result = new Customer();
                result.UserName = userName;
                result.First = "Guest";
                result.Add(userName);
            }

            result.LoadCart();

            return result;
        }
    }
}

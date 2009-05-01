using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using Kona.Infrastructure;

namespace Kona.Data {
    
    public class ShoppingCartItem {


        int _quantity = 0;
        public int Quantity {
            get {
                return _quantity;
            }
        }

        Product _product;
        public Product Product {
            get {
                return _product;
            }
        }

        decimal _discount=0;
        public decimal Discount {
            get {
                return _discount;
            }
        }


        string _discountReason="";
        public string DiscountReason {
            get {
                return _discountReason;
            }
        }
        DateTime _dateAdded = DateTime.Now;
        public DateTime DateAdded {
            get {
                return _dateAdded;
            }
        }


        public decimal LineTotal {
            get {
                return Product.BasePrice * Quantity-_discount;
            }
        }
        public ShoppingCartItem() { }

        public ShoppingCartItem(Product product) : this(product, 1,DateTime.Now) { }

        public ShoppingCartItem(Product product, int quantity,DateTime dateAdded) {
            _product = product;
            _quantity = quantity;
            _dateAdded = dateAdded;
        }

        public void SetDiscount(decimal amount, string reason) {
            _discount = amount;
            _discountReason = reason;
        }

        public void AdjustQuantity(int newQuantity) {
            _quantity = newQuantity;
        }


    }
}

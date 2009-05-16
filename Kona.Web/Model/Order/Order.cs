using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Kona.Infrastructure;

namespace Kona.Data {

    public enum OrderStatus {
        New=1,
        Submitted=2,
        Verified=3,
        Charged=4,
        Packaging=5,
        Shipped=6,
        Returned=7,
        Cancelled=8,
        Refunded=9,
        Closed=10,
        NotCheckedOut=99
    }

    public partial class Order {

        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public static Order ReadyOrderForPayment(string userName, PaymentMethod payment) {
            Order result = Order.SingleOrDefault(x => x.UserName == userName && x.OrderStatusID == (int)OrderStatus.NotCheckedOut);
            if (result == null)
                throw new InvalidOperationException("There is no Order in queue for " + userName);
            
            result.PaymentMethod = payment;
            return result;


        }

        public static Order FindCheckedOutOrder(Guid orderID) {

            //load the order
            var order = Order.SingleOrDefault(x => x.OrderID == orderID);

            //if it's not null, pull the products
            if (order != null) {
                KonaDB db = new KonaDB();
                var qry = (from p in db.Products
                          join oi in db.OrderItems on p.SKU equals oi.SKU
                          where oi.OrderID == orderID
                          select p).ToList();

                foreach (var item in order.OrderItems) {
                    item.Item = qry.SingleOrDefault(x => x.SKU == item.SKU);
                }

                //get the addresses
                order.ShippingAddress = Address.SingleOrDefault(x => x.AddressID == order.ShippingAddressID);
                order.BillingAddress = Address.SingleOrDefault(x => x.AddressID == order.BillingAddressID);

            }

  
            return order;
        }

        public static Order FindCurrentOrCreateNew(string userName){
            
            var db = new KonaDB();

            Order result = Order.SingleOrDefault(x => x.UserName == userName && x.OrderStatusID == (int)OrderStatus.NotCheckedOut);

            if (result == null) {
                result = new Order();
                result.OrderID = Guid.NewGuid();
                result.UserName = userName;
                result.UserLanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                result.OrderStatusID = (int)OrderStatus.NotCheckedOut;
                result.Add("");
            } else {
                result.ShippingAddress = Address.SingleOrDefault(x => x.AddressID == result.ShippingAddressID);
                result.BillingAddress = Address.SingleOrDefault(x => x.AddressID == result.BillingAddressID);
            }
            return result;
        }

        public void SaveItems(IEnumerable<ShoppingCartItem> items) {

            //run this in a transaction
            var cmds = new List<DbCommand>();
            foreach (var item in this.OrderItems) {
                cmds.Add(item.GetDeleteCommand());
            }

            //now add in these items
            foreach (var item in items) {
                var newItem = new OrderItem();
                newItem.SKU = item.Product.SKU;
                newItem.OrderID = this.OrderID;
                newItem.Quantity = item.Quantity;
                newItem.DateAdded = item.DateAdded;
                newItem.Discount = item.Discount;
                newItem.DiscountReason = item.DiscountReason;
                cmds.Add(newItem.GetInsertCommand());
            }

            var db = new KonaDB();
            db.ExecuteTransaction(cmds);

        }

        public OrderStatus Status {
            get {
                return (OrderStatus)OrderStatusID;
            }
            set {
                OrderStatusID = (int)value;
            }
        }

        public decimal Total {
            get {
                return this.ShippingAmount + this.TaxAmount + this.SubTotal;
            }
        }

    }
}

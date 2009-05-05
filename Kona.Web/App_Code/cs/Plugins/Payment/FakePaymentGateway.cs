using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Infrastructure;
using Kona.Data;
using System.Runtime.Serialization;

namespace Kona.Web.Plugins {
    
    public class FakePaymentGateway:Plugin {

        public Transaction AuthorizeCreditCard(Order order) {
            
            //this is a fake processor for testing...
            //if there are transaction errors, 
            //pop them into the TransactionErrors on the Transaction object
            //for display to the end user
            string authCode = System.Guid.NewGuid().ToString().Substring(0, 10);

            Transaction t = Transaction.CreateTransaction(order, authCode, "FakePaymentGateway");

            return t;

        }

    }
}

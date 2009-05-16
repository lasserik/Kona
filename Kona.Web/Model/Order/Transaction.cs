using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class Transaction {

        public IList<string> TransactionErrors { get; set; }
        public static Transaction CreateTransaction(Order order, string authCode, string processor) {
            Transaction result=new Transaction();
            result.OrderID = order.OrderID;
            result.Amount = order.Total;
            result.AuthorizationCode = authCode;
            result.Processor = processor;
            result.TransactionDate = DateTime.Now;
            result.TransactionID = Guid.NewGuid();
            result.TransactionErrors = new List<string>();
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class Transaction {

        public IList<string> TransactionErrors { get; set; }
        public Transaction(Guid orderID, decimal total, string authCode, string processor) {
            this.OrderID = orderID;
            this.Amount = total;
            this.AuthorizationCode = authCode;
            this.Processor = processor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Infrastructure;
using Kona.Data;

namespace Kona.Web.Plugins.Order {
    public class SimpleOrderExecution:Plugin {

        public bool PreExecution(Order order) {
            return true;
        }

        public void AuthorizationSuccess(Order order) {

        }

        public void AuthorizationFailed(Order order) {

        }

        public void PostExecution(Order order) {

        }

    }
}

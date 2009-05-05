using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Kona.Data;
using Kona.Infrastructure;

namespace Kona.Web.Controllers {
    public class OrderController : KonaController {
        IPluginEngine _pluginEngine;

        public OrderController(IPluginEngine pluginEngine) {
            _pluginEngine = pluginEngine;
            this.CurrentCustomer = Customer.GetExistingOrCreate(this.GetCommerceUserName());
            this.SiteData = KonaSite.GetSite("/");
        }

        public ActionResult Index() {
            return View();
        }

        /*
        #region PayPal Display
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PayPal(int id) {
            //a shipping method was submitted
            //evaluate it
            if (id > 0) {
                //pull the shipping methods
                this.ShippingMethods = _pluginEngine.CalculateShipping(this.SiteData.ShippingPlugin, this.CurrentCart); ;
                var selectedShipping = this.ShippingMethods.SingleOrDefault(x => x.ID == id);
                this.CurrentCart.ShippingService = selectedShipping.ServiceName;
                this.CurrentCart.ShippingAmount = selectedShipping.Cost;
                this.CurrentCart.ShippingMethodID = selectedShipping.ID;

            }
            return View("PayPal");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PayPal() {

            //they've selected PayPal
            //so we need to defaul the shipping selection for them
            //make sure they can checkout
            if (this.CurrentCart.Items.Count <= 0) {
                //TODO - localize this and check to see if the order requires shipping
                ViewData["ErrorMessage"] = "You need to have at least one item in your cart and selected a shipping address in order to checkout";
                return View("Billing");
            }
            else {

                this.ShippingMethods = _pluginEngine.CalculateShipping(this.SiteData.ShippingPlugin, this.CurrentCart); ;
                this.CurrentCart.ShippingService = this.ShippingMethods[0].ServiceName;
                this.CurrentCart.ShippingAmount = this.ShippingMethods[0].Cost;
                this.CurrentCart.ShippingMethodID = this.ShippingMethods[0].ID;

            }
            return View("PayPal");
        }
        #endregion

        */

        public ActionResult Checkout() {

            if (this.CurrentCart.Items.Count == 0)
                return RedirectToAction("Show", "Cart");
            else if (User.Identity.IsAuthenticated)
                return RedirectToAction("Shipping");
            else
                return View("Checkout");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Shipping(int? id) {


            if (this.CurrentCart.Items.Count == 0)
                return RedirectToAction("Show", "Cart");

            id = id ?? 0;
            if (id > 0) {
                this.CurrentCustomer.DefaultAddress = this.CurrentCustomer.Addresses.Where(x => x.AddressID == (int)id).SingleOrDefault();
            }
            return View("Shipping");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Shipping(Kona.Data.Address address) {

            if (ModelState.IsValid) {

                //save the address
                this.CurrentCart.ShippingAddress = Address.SaveIfNotExists(address);

                //calc the tax
                this.CurrentCart.TaxAmount = _pluginEngine.CalculateTax(this.SiteData.TaxPlugin, this.CurrentCart);

                //save the cart
                //_customerService.SaveCustomer(this.CurrentCustomer);
                this.CurrentCart.Save();

                //send to billing
                return RedirectToAction("Billing");
            }
            else {
                //let error handling pick it up
                return View("Shipping");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Billing() {


            if (this.CurrentCart.Items.Count == 0)
                return RedirectToAction("Show", "Cart");
            return View("Billing");

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Billing(Kona.Data.Address address) {


            if (ModelState.IsValid) {

                //save the address
                this.CurrentCart.BillingAddress = Address.SaveIfNotExists(address); ;

                //set the shipping methods
                this.ShippingMethods = _pluginEngine.CalculateShipping(this.SiteData.ShippingPlugin, this.CurrentCart); ;

                //default to the first
                this.CurrentCart.SetSelectedShipping(this.ShippingMethods[0]);
                
                //save the cart
                this.CurrentCart.Save();

                //send them to Finalize
                return RedirectToAction("Finalize");
            }
            else {
                return View("Billing");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Payment(CreditCard card) {
            
            if (this.CurrentCart.Items.Count == 0)
                return RedirectToAction("Show", "Cart");

            //set the card
            this.CurrentCart.CreditCard = card;

            //create an order
            Order order = Order.ReadyOrderForPayment(this.CurrentCustomer.UserName, card);
            
            //validate what we have
            //TODO: - check boolean
            this.ValidateOrder(order);

            //execute the payment..
            Transaction trans = this.AuthorizeCreditCard(order);


            //on success send to Receipt
            if (trans.TransactionErrors.Count == 0) {

                //save it down
                //_orderRepository.Save(order, trans);

                return RedirectToAction("Receipt", new { id = order.OrderID.ToString() });
            }
            else {

                ViewData["ErrorMessage"] = trans.TransactionErrors[0];

                //let the View know
                return View("Finalize");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Finalize() {


            if (this.CurrentCart.Items.Count == 0)
                return RedirectToAction("Show", "Cart");

            this.ShippingMethods = _pluginEngine.CalculateShipping(this.SiteData.ShippingPlugin, this.CurrentCart); ;

            this.CurrentCart.SetSelectedShipping(this.ShippingMethods[0]);
            return View("Finalize");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Finalize(int? id) {

            if (id > 0) {
                this.ShippingMethods = _pluginEngine.CalculateShipping(this.SiteData.ShippingPlugin, this.CurrentCart); ;
                var selectedShipping = this.ShippingMethods.SingleOrDefault(x => x.ShippingMethodID == id);
                this.CurrentCart.SetSelectedShipping(selectedShipping);
            }
            return View("Finalize");
        }

        public ActionResult Receipt() {
            return View();
        }
    }
}

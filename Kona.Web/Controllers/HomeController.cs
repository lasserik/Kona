using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kona.Data;
using System.Data.Linq;
namespace Kona.Web.Controllers {
    [HandleError]
    public class HomeController : KonaController {

        public HomeController() {

            this.CurrentCustomer = Customer.GetExistingOrCreate(this.GetCommerceUserName());
            
            //TODO: This is for test/dev. Remove 
            this.SiteData = KonaSite.GetSite("/");

        }

        public ActionResult Login() {
            return View("Login");
        }
        public ActionResult Index(string slug) {

            slug = slug ?? "";
            this.CurrentCategory = Category.GetCategoryPage(slug);

            return View();
        }
        public ActionResult Show(string sku) {
            this.SelectedProduct = new Product(sku);
            return View("Detail");
        }

        public ActionResult Edit(string slug) {
            slug = slug ?? "";
            this.CurrentCategory = Category.GetCategoryPage(slug);
            return View("EditCategory",this.CurrentCategory);
        }

        public ActionResult About() {
            return View();
        }
    }
}

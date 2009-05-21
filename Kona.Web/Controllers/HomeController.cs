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
            this.Categories = Category.GetHierarchicalCategories();
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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string slug) {
            slug = slug ?? "";
            this.CurrentCategory = Category.GetCategoryPage(slug);
            return View("EditCategory",this.CurrentCategory);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string slug, FormCollection form) {

            //pull it - this is the localized stuff
            //user can't edit other things
            CategoryLocalized loc = new CategoryLocalized(x => x.Slug == slug);

            //update it - the only thing people can change is the title/slug
            //so make sure it gets saved to the localized stuff
            UpdateModel<CategoryLocalized>(loc);

            //save it
            loc.Update(User.Identity.Name);

            if (!Request.IsAjaxRequest()) {
                return RedirectToAction("Index", new { slug = slug });
            } else {
                return new EmptyResult();
            }
        }


        public ActionResult About() {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Data;
using Kona.Infrastructure;
using System.Web.Mvc;
using System.Web.Mvc.Html;

    public static class ProductExtensions {

        public static Product Product(this IViewDataContainer pg) {
            Product result = null;

            if (pg.ViewData["SelectedProduct"] != null)
                result = (Product)pg.ViewData["SelectedProduct"];
            return result;

        }

        public static Category CurrentCategory(this IViewDataContainer pg) {
            Category result = new Category();

            if (pg.ViewData["CurrentCategory"] != null) {
                result = (Category)pg.ViewData["CurrentCategory"];
            }
            return result;
        }
        public static PagedList<Product> Products(this IViewDataContainer pg) {
            PagedList<Product> result = new PagedList<Product>(new List<Product>(), 1, 1, 1);

            if (pg.ViewData["Products"] != null) {
                IList<Product> products = (IList<Product>)pg.ViewData["Products"];
                result = new PagedList<Product>(products.ToList(), products.Count, 0, 20);
            }
            return result;
        }

        //public static LocalizedCategory Category(this IViewDataContainer pg) {
        //    var result = new LocalizedCategory();

        //    if (pg.ViewData["SelectedCategory"] != null) 
        //        result = (LocalizedCategory)pg.ViewData["SelectedCategory"];

        //    return result;
        //}
        //public static IList<LocalizedCategory> Categories(this ViewMasterPage pg) {
        //    return Categories(pg.Page as ViewPage);

        //}
        //public static IList<LocalizedCategory> Categories(this IViewDataContainer pg) {
        //    var result = new List<LocalizedCategory>();

        //    if (pg.ViewData["Categories"] != null){
        //        var categories = (IList<LocalizedCategory>)pg.ViewData["Categories"];
        //        //drop these into a grouped list
        //        result = categories.ToList();

        //    }

        //    return result;
        //}

        public static string ProductImage(this HtmlHelper helper, string image) {
            string linkFormat = "~/Content/ProductImages/{0}";
            string linkUrl = string.Format(linkFormat,image);
            return System.Web.VirtualPathUtility.ToAbsolute(linkUrl);
        }

    }

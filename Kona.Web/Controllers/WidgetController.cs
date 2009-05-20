using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Data;
using System.Data.Common;
using Kona.Infrastructure;
using System.Text;

namespace Kona.Web.Controllers
{
    public class WidgetController : KonaController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Widget/Show/5

        public ActionResult Edit(string id)
        {
            
            //pull the widget
            Guid widgetID = new Guid(id);
            var widget = Widget.SingleOrDefault(x => x.WidgetID == widgetID);

            if (widget != null) {
                //get the products
                widget.LoadProducts();
            }
            
            return View(widget.ViewName+"Editor",widget);
        }

        //
        // GET: /Widget/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Widget/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {

            Widget newWidget = new Widget();
            UpdateModel<Widget>(newWidget);

            //the category ID should be sent in as "categoryid"
            string categoryID = form["CategoryID"];
 
            var widgetCommand = newWidget.GetInsertCommand();

            //save the category too
            Categories_Widget map = new Categories_Widget();
            int catid = 0;
            int.TryParse(categoryID, out catid);
            map.CategoryID = catid;
            map.WidgetID = newWidget.WidgetID;

            var mapCommand = map.GetInsertCommand();

            KonaDB db = new KonaDB();
            db.ExecuteTransaction(new List<DbCommand>() { widgetCommand, mapCommand });

            //return a json result with the ID
            return Json(new { ID = newWidget.WidgetID, Zone = newWidget.Zone });

        }

        //
        // POST: /Widget/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection collection)
        {
            Guid widgetID = new Guid(id);
            
            //pull the widget
            Widget w = Widget.SingleOrDefault(x =>x.WidgetID==widgetID);
            if (w != null) {

                //update it
                UpdateModel<Widget>(w);

                //save
                w.Update(User.Identity.Name);

            }
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void Delete(string id) {

            Guid widgetID = new Guid(id);

            //delete associations
            Categories_Widget.Delete(x => x.WidgetID == widgetID);

            //delete the widget
            Widget.Delete(widgetID);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void SaveWidgetOrder() {

            var commands = new List<DbCommand>();
            KonaDB db = new KonaDB();

            if (Request.Form["widgetid"] != null) {
                //pull out all widgetid's
                var ids = Request.Form.GetValues("widgetid").Where(x => !String.IsNullOrEmpty(x)).ToArray();
                int listOrder = 0;
                foreach (var id in ids) {
                    var widgetID = new Guid(id);
                    //subsonic style...
                    commands.Add(db.Update<Widget>().Set(x => x.ListOrder == listOrder).Where(x=>x.WidgetID==widgetID).GetCommand().ToDbCommand());
                    listOrder++;
                }
            }
            //transaction :)
            db.ExecuteTransaction(commands);

        }

        public string SkuList() {
            var pref = Request.QueryString["q"] ?? "";
            var products = Product.Find(x => x.SKU.StartsWith(pref, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.SKU).ToList();
            StringBuilder sb = new StringBuilder();
            products.ForEach(x => sb.Append(x.SKU + "\r\n"));
            return sb.ToString();
        }

        public ActionResult GetProduct(string id) {
            var product = Product.SingleOrDefault(x => x.SKU == id);
            if (product != null) {

                return Json(new
                {
                    SKU=product.SKU,
                    Photo=product.DefaultImageFile,
                    ProductName=product.ProductName,
                    Price=product.BasePrice.ToString("C")
                });
            } else {
                return new EmptyResult();
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveProductWidget() {

            var widgetID = Request.Form["widgetid"] ?? "";
            if (!string.IsNullOrEmpty(widgetID)) {

                //pull out all sku's
                var skus = Request.Form.GetValues("sku");
                var widget = new Widget(new Guid(widgetID));
                widget.SKUList = skus.ToString();
                widget.Update(User.Identity.Name);

            }
            return new EmptyResult();
        }
    }
}

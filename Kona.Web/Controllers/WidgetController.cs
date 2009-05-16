using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Data;
using System.Data.Common;

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

            //TODO: wrap me in a transaction yo!

            //the category ID should be sent in as "categoryid"
            string categoryID = form["CategoryID"];

            newWidget.WidgetID = Guid.NewGuid();
            newWidget.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            newWidget.ListOrder = 0;
            newWidget.Title = "";
            newWidget.Body = "";
            newWidget.SKUList = "";
 
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
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

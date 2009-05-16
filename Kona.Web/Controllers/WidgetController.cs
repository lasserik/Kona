using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Data;

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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

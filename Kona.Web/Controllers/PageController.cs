using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Data;
using Kona.Infrastructure;
using System.Text;
using System.IO;

namespace Kona.Web.Controllers
{

    [ValidateInput(false)]
    public class PageController : KonaController
    {
        //
        // GET: /Page/

        public PageController() {

        }

        public ActionResult New() {

            //pull a list of drafts
            var drafts = Kona.Data.Page.Find(x => x.PageStatusID == (int)PublishStatus.Draft);

            //create a new GUID for this page
            return View("NewPage",drafts);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Kona.Data.Page p) {
            if (String.IsNullOrEmpty(p.Title)) {
                this.ModelState.AddModelError("notitle", "The page must have a title");
                return View("NewPage");
            } else {
                p.PageID = Guid.NewGuid();
                p.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                p.Slug = p.Title.CreateSlug();
                p.CreatedBy = User.Identity.Name;
                p.ModifiedBy = User.Identity.Name;
                //p.Url = p.Slug;

                if (ModelState.IsValid) {
                    p.Add(User.Identity.Name);
                }
                return RedirectToAction("Edit", new { id = p.PageID });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveProductImage() {

            Guid widgetID=new Guid(Request.Form["widgetid"]);
            Guid pageID = new Guid(Request.Form["pageid"]);

            if (Request.Files.Count > 0) {
                var item = Request.Files[0] as HttpPostedFileBase;
                string fileExtension=Path.GetExtension(item.FileName);
                string fileName=Path.GetFileName(item.FileName);
                string filePath=Path.Combine(Server.MapPath("~/content/images"),fileName);
                item.SaveAs(filePath);

                //now that the file's uploaded, save the file name as the body of the widget
                var widget = new Widget(widgetID);
                widget.Body = fileName;
                widget.ModifiedOn = DateTime.Now;
                widget.Update(User.Identity.Name);


            }
            return RedirectToAction("Edit", new { id = pageID });
        }


        public string SkuList() {
            StringBuilder sb = new StringBuilder();
            var pref = Request.QueryString["q"] ?? "";
            //subsonic style
            using (var rdr = Product.SelectColumns("SKU").Where("SKU").StartsWith(pref).ExecuteReader()) {
                while (rdr.Read()) {
                    sb.AppendLine(rdr[0].ToString());
                }
            }
            return sb.ToString();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id) {
             //pull the page and widgets
            var pg = new Page(new Guid(id));
            if (pg != null) {
                return View("EditPage",pg);
            } else {
                return RedirectToAction("Create");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Page page) {

            if (page != null) {
                page.ModifiedBy = User.Identity.Name;
                page.ModifiedOn = DateTime.Now;
                page.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                page.Slug = page.Title.CreateSlug();

                page.Update(User.Identity.Name);

                return View("EditPage", page);
            } else {
                return RedirectToAction("Create");
            }
        }
        public ActionResult Preview(string id) {
            Guid pageID = new Guid(id);
            this.CurrentPage = new Page(pageID);
            return View("Preview");
        }

        public ActionResult SortPages() {
            if (Request.Form["pageid"] != null) {
                //pull out all pageid's
                var pages = Request.Form.GetValues("pageid").Where(x => !String.IsNullOrEmpty(x)).ToArray();
                int listOrder = 0;
                KonaDB db = new KonaDB();
                foreach (var pg in pages) {
                    //subsonic style :)
                    db.Update<Page>().Set(x => x.ListOrder == listOrder).Execute();
                    listOrder++;
                }
            }
            return new EmptyResult();
        }

        public ActionResult List() {
            var pages = Page.All();//_cmsRepository.GetPages();
            return View("Pages", pages);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SetStatus(string id, string status) {
            return null;
            Guid pageID = new Guid(id);
            ActionResult result;
            var pg = new Page(pageID);//_cmsRepository.GetPage(pageID);

            if (status == "Publish") {
                //pull the page
                pg.Status = PublishStatus.Published;
                pg.Update(User.Identity.Name);
                return RedirectToAction("Index", "Home", new { slug = pg.Slug });
            } else if (status == "Take Offline") {
                //offline
                pg.Status = PublishStatus.Offline;
                pg.Update(User.Identity.Name);
                return RedirectToAction("Edit", "Page", new { id = pg.PageID.ToString() });

            } else if (status == "Delete") {
                pg.Delete();
                return RedirectToAction("Index", "Home");
            } else {
                return RedirectToAction("Edit", "Page", new { id = pg.PageID.ToString() });

            }


        }


        public ActionResult Editor() {
            return View("TextEditor");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveWidget(string id) {
            if (!String.IsNullOrEmpty(id)) {
                //_cmsRepository.DeleteWidget(new Guid(id));
                Widget.Delete(new Guid(id));
            }
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddWidget(Widget widget) {
            widget.WidgetID = Guid.NewGuid();
            widget.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            widget.Title = widget.Title ?? "";
            widget.Body = widget.Body ?? "";
            //save em
            widget.Add(User.Identity.Name);
            return Json(widget);
        }

        public ActionResult RenderWidget(string id) {
            Guid widgetID = new Guid(id);
            Widget widget = new Widget(widgetID);

            return View(widget.WidgetDefinition+"Edit", widget);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveWidget(Widget widget) {

            //pull the widget to keep the data current
            Widget existing = new Widget(widget.WidgetID);

            existing.ModifiedOn = DateTime.Now;
            existing.ModifiedBy = User.Identity.Name;
            existing.Title = widget.Title;
            if (!string.IsNullOrEmpty(widget.Body))
                existing.Body = widget.Body;
            //_cmsRepository.SaveWidget(existing);
            existing.Update(User.Identity.Name);
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveProductWidget() {
            var id = Request.Form["widgetid"] ?? "";
            if (!string.IsNullOrEmpty(id)) {
                //pull out all sku's
                var skus = Request.Form.GetValues("sku");
                var widgetID=new Guid(id);
                Widgets_Product.Delete(x => x.WidgetID == widgetID);
                foreach (var sku in skus) {
                    Widgets_Product wp = new Widgets_Product();
                    wp.WidgetID = widgetID;
                    wp.SKU = sku;
                    wp.Add(User.Identity.Name);
                }
               
            }
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OrderWidgets() {
            if (Request.Form["widgetid"]!=null) {
                //pull out all widgetid's
                var ids = Request.Form.GetValues("widgetid").Where(x=>!String.IsNullOrEmpty(x)).ToArray();
                int listOrder = 0;
                KonaDB db = new KonaDB();
                foreach (var id in ids) {
                    var widgetID = new Guid(id);
                    //subsonic style...
                    db.Update<Widget>().Set(x => x.ListOrder == listOrder);
                    listOrder++;
                }
            }
            return new EmptyResult();
        }

        public ActionResult GetProduct(string id) {
            //this is for AJAX calls
            var product = new Product(new Guid(id));// _productRepository.GetProduct(id);
            if (product != null) {

                JsonProduct jsonProduct = new JsonProduct();
                jsonProduct.SKU = product.SKU;
                jsonProduct.Photo = product.DefaultImageFile;
                jsonProduct.ProductName = product.ProductName;
                jsonProduct.Price = product.BasePrice.ToString("C");

                return Json(jsonProduct);
            } else {
                return new EmptyResult();
            }
        }

    }
    public class JsonProduct {
        public string Photo { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string SKU { get; set; }
    }
}

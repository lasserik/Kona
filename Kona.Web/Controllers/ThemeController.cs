using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Model;
using System.IO;
using Kona.Infrastructure;

namespace Kona.Web.Controllers
{
    public class ThemeController : KonaController
    {

        ICMSRepository _cmsRepository;
        ICustomerRepository _customerRepository;
        IObjectStore _objectStore;

        public ThemeController(ICustomerRepository customerRepository,
            ICMSRepository cmsRepository,
            IObjectStore objectStore) : base(customerRepository,objectStore,cmsRepository) {
            
            _cmsRepository = cmsRepository;
            _objectStore = objectStore;
            _customerRepository = customerRepository;
            this.ThemeName = "Admin";
        }
        
        //
        // GET: /Theme/

        public ActionResult Index()
        {
            return View("Themes");
        }

        //
        // GET: /Theme/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Theme/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Theme/Create

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
        // GET: /Theme/Edit/5
 
        public ActionResult Edit(string id, string page)
        {
            
            //get theme directory
            string themeDirectory = Server.MapPath("~/Views/Themes");
            string selectedThemeDirectory = Path.Combine(themeDirectory, id);
            IList<string> themePages = new List<string>();
            string themeCode = "";

            if (Directory.Exists(selectedThemeDirectory)) {
                //loop out all the pages
                themePages = Directory.GetFiles(selectedThemeDirectory).ToList();

                //get the master if no page selected
                if (String.IsNullOrEmpty(page))
                    page = themePages.Where(x => x.ToLower().EndsWith(".master")).SingleOrDefault();
                else
                    page = themePages.Where(x => Path.GetFileName(x)==page).SingleOrDefault();

                //pull the code
                if (!string.IsNullOrEmpty(page))
                    themeCode = page.GetFileText();

            }

            ViewData["ThemeCode"] = themeCode;
            ViewData["ThemePages"] = themePages;
            ViewData["ThemePage"] = page;
            ViewData["ThemeId"] = id;
            return View("EditTheme");
        }

        //
        // POST: /Theme/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string code = collection["code"];
                string pageFile = collection["themePage"];
                if (!String.IsNullOrEmpty(code) && !String.IsNullOrEmpty(pageFile)) {
                    code.PutFileText(pageFile);

                }
                string pageName = Path.GetFileName(pageFile);
                return RedirectToAction("Edit", new { id = id,page=pageName });
            }
            catch
            {
                return View("EditTheme");
            }
        }
    }
}

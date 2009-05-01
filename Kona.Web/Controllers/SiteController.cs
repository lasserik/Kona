using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Infrastructure;
using Kona.Model;

namespace Kona.Web.App.Controllers
{
    public class SiteController : KonaController
    {
        ICMSRepository _cmsRepository;
        ICustomerRepository _customerRepository;
        IObjectStore _objectStore;

        public SiteController(ICustomerRepository customerRepository,
            ICMSRepository cmsRepository,
            IObjectStore objectStore) : base(customerRepository,objectStore,cmsRepository) {
            
            _cmsRepository = cmsRepository;
            _objectStore = objectStore;
            _customerRepository = customerRepository;
            this.ThemeName = "Admin";
        }

        public ActionResult Index()
        {
            
            return View(this.SiteData);
        }

        public ActionResult Update(KonaSite site) {

            string url = Url.GetSiteUrl();
            _objectStore.Delete("Site", url);
            _objectStore.Store<KonaSite>("Site",url, site);
            this.SiteData = site;
            ViewData["message"] = "Site Updated";
            //}

            return View("Index");

        }

        public ActionResult Config() {
            return View("Configuration");
        }
    }
}

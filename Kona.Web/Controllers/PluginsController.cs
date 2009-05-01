using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.Infrastructure;
using Kona.Model;
using Microsoft.Web.Commerce.Plugins;

namespace Kona.Web.Controllers
{
    [ValidateInput(false)]
    public class PluginsController : KonaController
    {
        ICMSRepository _cmsRepository;
        ICustomerRepository _customerRepository;
        IObjectStore _objectStore;

        public PluginsController(ICustomerRepository customerRepository,
            ICMSRepository cmsRepository,
            IObjectStore objectStore) : base(customerRepository,objectStore,cmsRepository) {
            
            _cmsRepository = cmsRepository;
            _objectStore = objectStore;
            _customerRepository = customerRepository;
            this.ThemeName = "Admin";
        }

        //
        // GET: /Plugins/
        void SetPlugins() {
            //load the Plugins
            var plugs = Plugin.Plugins.ToList();

            //get the settings for each plugin
            //plugs.ForEach(x => x.Settings = _objectStore.Get<PluginSetting>("PluginSetting", x.GetType().Name) ?? new PluginSetting());

            ViewData["Plugins"] = plugs;

        }
        public ActionResult Index()
        {
            SetPlugins();
            
            return View("Plugins");
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string id, FormCollection collection) {

            PluginSetting setting = new PluginSetting();
            setting.PluginName = id;
            for (int i = 0; i < collection.Count; i++) {
                if (collection.Keys[i] != "id") {
                    string settingName = collection.GetKey(i);
                    object settingValue = collection[i];
                    try {
                        Plugin.ValidateSetting(id, settingName, settingValue);
                        setting.Settings.Add(collection.GetKey(i), collection[i]);
                        ViewData["Message"] = id + " saved";
                    } catch (Exception x) {
                        
                        ViewData["Message"] = x.Message;
                        break;
                    }
                }
            }
            _objectStore.Store<PluginSetting>("PluginSetting", id, setting);
            SetPlugins();
            return View("Plugins");

        }

        public ActionResult Edit(string id) {
            //load the Plugins
            var plugs = Plugin.Plugins.ToList();

            //get the one we're looking for
            var thisPlug = plugs.Where(x => x.PluginName == id).SingleOrDefault();

            ViewData["SelectedPlugin"] = thisPlug;
            
            //show it
            return View("EditPlugin");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection collection) {
            //load the Plugins
            var plugs = Plugin.Plugins.ToList();

            //get the one we're looking for
            var thisPlug = plugs.Where(x => x.PluginName == id).SingleOrDefault();

            ViewData["SelectedPlugin"] = thisPlug;

            //save the new text
            string code = collection["code"];
            if (!string.IsNullOrEmpty(code)) {
                code.PutFileText(thisPlug.FilePath);
            }


            //save the settings
            PluginSetting setting = new PluginSetting();
            setting.PluginName = id;
            for (int i = 0; i < collection.Count; i++) {
                if (collection.Keys[i] != "id" && collection.Keys[i] !="code") {
                    string settingName = collection.GetKey(i);
                    object settingValue = collection[i];
                    try {
                        Plugin.ValidateSetting(id, settingName, settingValue);
                        setting.Settings.Add(collection.GetKey(i), collection[i]);
                        ViewData["Message"] = id + " saved";
                    } catch (Exception x) {

                        ViewData["Message"] = x.Message;
                        break;
                    }
                }
            }
            _objectStore.Store<PluginSetting>("PluginSetting", id, setting);



            //show it
            return RedirectToAction("Edit", new { id = id });
        }

        //
        // GET: /Plugins/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Plugins/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Plugins/Create

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


    }
}

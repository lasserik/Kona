
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using Kona.Infrastructure;
namespace Kona.Web {
    public class KonaViewEngine : WebFormViewEngine {

        public KonaViewEngine() {
            ViewLocationFormats = new[] { 
                "~/{0}.aspx",
                "~/{0}.ascx",
                "~/Views/{1}/{0}.aspx",
                "~/Views/{1}/{0}.ascx",
                "~/Views/Shared/{0}.aspx",
                "~/Views/Shared/{0}.ascx",
                "~/Views/Plugins/{0}.ascx",
            };

            MasterLocationFormats = new[] {
                "~/{0}.master",
                "~/Shared/{0}.master",
                "~/Views/{1}/{0}.master",
                "~/Views/Shared/{0}.master",
            };
            PartialViewLocationFormats = ViewLocationFormats;

        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {
            ViewEngineResult result = null;
            var request = controllerContext.RequestContext;
            if (controllerContext.Controller.GetType().BaseType == typeof(KonaController)) {
                var orchardController = controllerContext.Controller as KonaController;
                string template = orchardController.ThemeName;
                string templatedViewName = string.Format(CultureInfo.InvariantCulture, "~/Views/Themes/{0}/{1}.aspx", template, viewName);

                masterName = string.IsNullOrEmpty(masterName) ? "Theme.master" : masterName;

                result = base.FindView(controllerContext, templatedViewName, "~/Views/Themes/" + template + "/" + masterName, useCache);
                controllerContext.HttpContext.Items["template"] = template;
            } else {
                result = base.FindView(controllerContext, viewName, masterName, useCache);
            }

            return result;
        }

    }
}
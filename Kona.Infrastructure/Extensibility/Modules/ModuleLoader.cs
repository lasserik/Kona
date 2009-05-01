using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using Microsoft.Web.Mvc;

namespace System.Web.Mvc {
    public static class ModuleLoader {

        public static void LoadModules(this HtmlHelper helper, string moduleType) {
            //take a peak in the Modules directory
            string moduleDirectory=Path.Combine(helper.ViewContext.HttpContext.Server.MapPath("~/App/Views/Modules/"),moduleType);
            if(!Directory.Exists(moduleDirectory))
                throw new InvalidOperationException("There is no "+moduleType+" module directory - spelling error?");

            //get a list of the modules in there
            string[] moduleDirectories = Directory.GetDirectories(moduleDirectory);

            //by convention, the name of the directory MUST be the name of the controller
            //so it can be rendered
            foreach (string dir in moduleDirectories) {

                string stub = Path.GetFileNameWithoutExtension(dir);
                var tempData=helper.ViewContext.Controller.TempData;
                if(!dir.Contains(".svn"))
                    helper.RenderAction("Index", stub);
            }
        
        }

    }
}

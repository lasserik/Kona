using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kona.Infrastructure {
    public static  class CSSExtensions {
        public static string ClassForMatchingRequest(this UrlHelper url, string action, string controllerName, string classIfMatch) {
            return ClassForMatchingRequest(url, action, controllerName, classIfMatch, "");
        }
        public static string ClassForMatchingRequest(this UrlHelper url, string action, string controllerName, string classIfMatch, string classIfNoMatch) {
            string result = "";

            string thisUrl = url.RequestContext.HttpContext.Request.Url.AbsolutePath;
            string actionUrl = url.Action(action, controllerName);
            if (actionUrl.StartsWith("."))
                actionUrl = actionUrl.Substring(1, actionUrl.Length - 1);
            if (thisUrl.Equals(actionUrl)) {
                result = string.Format(" class=\"{0}\" ", classIfMatch);
            } else if (!String.IsNullOrEmpty(classIfNoMatch)) {
                result = string.Format(" class=\"{0}\" ",classIfNoMatch);

            }

            return result;
        }

    }
}

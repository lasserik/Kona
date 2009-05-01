using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc {
    public static class UrlHelperExtensions {

        public static string AbsoluteAction(this UrlHelper helper, string actionName) {
            string relUrl = helper.Action(actionName);
            return string.Format("{0}/{1}", GetSiteUrl(helper), relUrl);
        }
        public static string AbsoluteAction(this UrlHelper helper, string actionName, string controllerName) {
            string relUrl = helper.Action(actionName, controllerName);
            return string.Format("{0}/{1}", GetSiteUrl(helper), relUrl);
        }

        public static string AbsoluteAction(this UrlHelper helper, string actionName, string controllerName, object values) {
            string relUrl = helper.Action(actionName, controllerName,values);
            
            return string.Format("{0}/{1}", GetSiteUrl(helper), relUrl);
        }
        
        public static string GetSiteUrl(this UrlHelper helper) {
            var ctx = HttpContext.Current;//helper.RequestContext.HttpContext;
            string Port = ctx.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;

            string Protocol = ctx.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            string appPath = ctx.Request.ApplicationPath;
            if (appPath == "/")
                appPath = "";

            string sOut = Protocol + ctx.Request.ServerVariables["SERVER_NAME"] + Port + appPath;
            return sOut;
        }
        public static string GetSiteUrl(this HttpContext context) {
            HttpContextWrapper wrapper = new HttpContextWrapper(context);
            return GetSiteUrl(wrapper as HttpContextBase, true);
        }
        public static string GetSiteUrl(HttpContextBase context, bool usePort) {
            string Port = context.Request.ServerVariables["SERVER_PORT"];
            if (usePort) {
                if (Port == null || Port == "80" || Port == "443")
                    Port = "";
                else
                    Port = ":" + Port;
            }
            string Protocol = context.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";

            string appPath = context.Request.ApplicationPath;
            if (appPath == "/")
                appPath = "";

            string sOut = Protocol + context.Request.ServerVariables["SERVER_NAME"] + Port + appPath;
            return sOut;

        }

        public static string GetSiteUrl(this ViewPage pg) {
            return GetSiteUrl(pg.Url);
        }
        public static string GetSiteUrl(this ViewMasterPage pg) {
            return GetSiteUrl(pg.Url);
        }

        public static string GetReturnUrl(this UrlHelper helper) {
            return GetReturnUrl(helper.RequestContext.HttpContext);

        }
        
        public static string GetReturnUrl(this ViewMasterPage pg) {
            return GetReturnUrl(pg.ViewContext.HttpContext);
        }

        public static string GetReturnUrl(this ViewUserControl pg) {
            return GetReturnUrl(pg.ViewContext.HttpContext);
        }
        /// <summary>
        /// Creates a ReturnUrl for use with the Login page
        /// </summary>
        public static string GetReturnUrl(this ViewPage pg) {
            return GetReturnUrl(pg.ViewContext.HttpContext);
        }

        static string GetReturnUrl(HttpContextBase ctx) {
            string returnUrl = "";

            if (ctx.Request.QueryString["ReturnUrl"] != null) {
                returnUrl = ctx.Request.QueryString["ReturnUrl"];
            } else {
                returnUrl = ctx.Request.Url.AbsoluteUri;
            }
            return returnUrl;
        }

    }


}

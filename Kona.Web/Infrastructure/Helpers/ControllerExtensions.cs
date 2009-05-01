using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace System.Web.Mvc {
    public static class ControllerExtensions {

        public static string GetCommerceUserName(this Controller controller) {

            return GetCommerceUserName(HttpContext.Current);
        }

        public static string GetCommerceUserName(HttpContext context) {
            string result = Guid.NewGuid().ToString();

            if (context != null) {

                //if not, do we know who they are?
                if (context.User.Identity.IsAuthenticated) {
                    result = context.User.Identity.Name;

                    //do they have a cookie?
                } else if (context.Request.Cookies["shopper"] != null) {
                    result = context.Request.Cookies["shopper"].Value;

                }

                //hate to do it this way, but Response.Cookies isn't implemented
                //in Controller.HttpContextBase.Response
                context.Response.Cookies["shopper"].Value = result;
                context.Response.Cookies["shopper"].Expires = DateTime.Now.AddMonths(12);
        }
            return result;
        }

        public static void RememberCommerceUser(this Controller controller, string userName) {
            //hate to do it this way, but Response.Cookies isn't implemented
            //in Controller.HttpContextBase.Response
            if (HttpContext.Current != null) {
                HttpContext.Current.Response.Cookies["shopper"].Value = userName;
                HttpContext.Current.Response.Cookies["shopper"].Expires = DateTime.Now.AddMonths(12);
                HttpContext.Current.Response.Cookies["shopper"].HttpOnly=true;;
            }
        }

        public static void StoreTemp(this Controller controller, string key, object value) {
            
            controller.TempData[key] = value;
        }
        public static T GetTemp<T>(this Controller controller, string key) where T : class, new() {
            T result = default(T);
            if (controller.TempData[key] != null)
                result = (T)controller.TempData[key];

            //TempData only holds the bits for one request
            //so put it back in to be sure that we can get it again
            StoreTemp(controller, key, result);
            return result;

        }

        // Site wide messaging APIs.
        public static void DisplayMessages(this Controller controller) {
            DisplayMessages(controller, null);
        }

        public static void DisplayMessages(this Controller controller, string messageCss) {
            if (controller.ViewData["ErrorMessage"] != null) {
                List<string> messages = (List<string>)controller.ViewData["ErrorMessage"];
                string defaultCss = "background-color:#FF3300; text-align:center; border:1px solid black; padding:5px;";
                foreach (string s in messages) {
                    string message =
                        "<div style=\"" +
                        (messageCss == null ? defaultCss : messageCss)  +
                        "\">" +
                        s +
                        "</div>";
                    HttpContext.Current.Response.Write(message);
                }
            }
            if (controller.ViewData["WarningMessage"] != null) {
                List<string> messages = (List<string>)controller.ViewData["WarningMessage"];
                string defaultCss = "background-color:#FF9933; text-align:center; border:1px solid black; padding:5px;";
                foreach (string s in messages) {
                    string message =
                        "<div style=\"" +
                        (messageCss == null ? defaultCss : messageCss) +
                        "\">" +
                        s +
                        "</div>";
                    HttpContext.Current.Response.Write(message);
                }
            }
            if (controller.ViewData["InformationMessage"] != null) {
                List<string> messages = (List<string>)controller.ViewData["InformationMessage"];
                string defaultCss = "background-color:#99CC66; text-align:center; border:1px solid black; padding:5px;";
                foreach (string s in messages) {
                    string message =
                        "<div style=\"" +
                        (messageCss == null ? defaultCss : messageCss) +
                        "\">" +
                        s +
                        "</div>";
                    HttpContext.Current.Response.Write(message);
                }
            }
        }

        public static void AddErrorMessage(this Controller controller, string message) {
            if (controller.ViewData["ErrorMessage"] == null) {
                controller.ViewData["ErrorMessage"] = new List<string>();
            }
            List<string> l = (List<string>)controller.ViewData["ErrorMessage"];
            l.Add(message);
            controller.ViewData["ErrorMessage"] = l;
        }

        public static void AddWarningMessage(this Controller controller, string message) {
            if (controller.ViewData["WarningMessage"] == null) {
                controller.ViewData["WarningMessage"] = new List<string>();
            }
            List<string> l = (List<string>)controller.ViewData["WarningMessage"];
            l.Add(message);
            controller.ViewData["WarningMessage"] = l;
        }

        public static void AddInformationMessage(this Controller controller, string message) {
            if (controller.ViewData["InformationMessage"] == null) {
                controller.ViewData["InformationMessage"] = new List<string>();
            }
            List<string> l = (List<string>)controller.ViewData["InformationMessage"];
            l.Add(message);
            controller.ViewData["InformationMessage"] = l;
        }

        public static bool RemoveMessage(this Controller controller, string message) {
            if (controller.ViewData["ErrorMessage"] != null) {
                List<string> l = (List<string>)controller.ViewData["ErrorMessage"];
                if (l.Contains(message)) {
                    l.Remove(message);
                    return true;
                }
            }
            if (controller.ViewData["WarningMessage"] != null) {
                List<string> l = (List<string>)controller.ViewData["WarningMessage"];
                if (l.Contains(message)) {
                    l.Remove(message);
                    return true;
                }
            }
            if (controller.ViewData["InformationMessage"] != null) {
                List<string> l = (List<string>)controller.ViewData["InformationMessage"];
                if (l.Contains(message)) {
                    l.Remove(message);
                    return true;
                }
            }
            return false;
        }

    }
}

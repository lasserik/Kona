using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kona.Infrastructure;
using Kona.Data;
using Kona.Web;

    public static class PluginExtensions {
        //TODO: Unhack this - I'm using ViewData and it's not shared between 
        //child and parent for some F'ing reason
        public static void AddressPlugin(this ViewUserControl pg) {
            string result = pg.SiteData().AddressForm;
            if (string.IsNullOrEmpty(result))
                result = "USDefault";

            pg.Html.RenderPartial(PluginLink("Address", result), pg.ViewData);
        }
        public static void AddressPlugin(this ViewPage pg) {

            //render out the AddressForm based on what's in the SiteData
            string result = pg.SiteData().AddressForm;
            if (string.IsNullOrEmpty(result))
                result = "USDefault";

            pg.Html.RenderPartial(PluginLink("Address", result),pg.ViewData);

        }

        public static void CreditCardPlugin(this ViewPage pg) {

            //render out the AddressForm based on what's in the SiteData
            string result = pg.SiteData().CreditCardForm;
            if (string.IsNullOrEmpty(result))
                result = "DefaultCreditCard";

            pg.Html.RenderPartial(PluginLink("CreditCard", result), pg.ViewData);

        }

        public static void PayPalPlugin(this ViewPage pg) {

            //render out the AddressForm based on what's in the SiteData
            string result = pg.SiteData().PayPalForm;
            if (string.IsNullOrEmpty(result))
                result = "DefaultPayPalStandard";

            pg.Html.RenderPartial(PluginLink("PayPal", result), pg.ViewData);

        }


        static string PluginLink(string type, string pluginName) {
            string linkFormat = "~/Views/Shared/{0}/{1}";

            if (!pluginName.EndsWith(".aspx") | !pluginName.EndsWith(".ascx")) {
                linkFormat += ".ascx";
            }

            return string.Format(linkFormat, type, pluginName);
        }

        public static IList<string> InstalledDisplayPlugins(this HtmlHelper helper, string type) {
            string pluginDirectory = helper.ViewContext.HttpContext.Server.MapPath("~/Views/Shared");
            string selectedDir = System.IO.Path.Combine(pluginDirectory, type);

            List<string> result = new List<string>();
            foreach (string s in System.IO.Directory.GetFiles(selectedDir).Where(x=>x.EndsWith(".ascx"))) {
                result.Add(System.IO.Path.GetFileNameWithoutExtension(s));
            }
            return result;

        }
        public static IList<string> InstalledCodePlugins(this HtmlHelper helper, string type) {
            string pluginDirectory = helper.ViewContext.HttpContext.Server.MapPath("~/App_Code/");
            string selectedDir = System.IO.Path.Combine(pluginDirectory, type);

            List<string> result = new List<string>();
            foreach (string s in System.IO.Directory.GetFiles(selectedDir).Where(x => x.EndsWith(".cs"))) {
                result.Add(System.IO.Path.GetFileNameWithoutExtension(s));
            }
            return result;

        }


        public static decimal CalculateTax(this IPluginEngine engine, string pluginName, ShoppingCart cart) {

            decimal result = Plugin.Execute<decimal>(pluginName, "CalculateTax", cart);
            return result;
        }


        public static IList<ShippingMethod> CalculateShipping(this IPluginEngine engine, string pluginName, ShoppingCart cart) {
            return Plugin.Execute<IList<ShippingMethod>>(pluginName, "CalculateShipping", cart);
        }

        public static string CreateOrderNumber(this KonaController controller, Order order) {
            return Plugin.ExecuteFirst<string>("CreateOrderNumber", order);
        }

        public static Transaction AuthorizeCreditCard(this KonaController controller, Order order) {
            return Plugin.ExecuteFirst<Transaction>("AuthorizeCreditCard", order);
        }
        public static bool ValidateOrder(this KonaController controller, Order order) {
            return Plugin.ExecuteFirst<bool>("ValidateOrder", order);
        }

        public static Plugin SelectedPlugin(this ViewPage pg) {
            Plugin result = null;
            if(pg.ViewData["SelectedPlugin"]!=null)
                result=(Plugin)pg.ViewData["SelectedPlugin"];
            return result;
        }

        public static IList<Plugin> Plugins(this ViewPage pg) {
            IList<Plugin> result = null;
            if (pg.ViewData["Plugins"] != null)
                result = (IList<Plugin>)pg.ViewData["Plugins"];
            return result;

        }

        public static IList<Plugin> PaymentGatewayPlugins(this ViewPage pg) {
            IList<Plugin> result = null;

            //result=pg.Plugins().Where(x=>x.

            if (pg.ViewData["Plugins"] != null)
                result = (IList<Plugin>)pg.ViewData["Plugins"];
            return result;
        }

    }

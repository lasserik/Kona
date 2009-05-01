using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using Kona.Data;
using Kona.Infrastructure;

    public static class ThemeExtensions {
        public static KonaSite SiteData(this IViewDataContainer pg) {
            return pg.ViewData["SiteData"] as KonaSite;
        }

        public static KonaSite SiteData(this ViewMasterPage pg) {
            return pg.ViewData["SiteData"] as KonaSite;
        }


        public static void ProductListView(this ViewUserControl pg, IList<Product> data) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "ProductList"), data);
        }

        public static void ProductListView(this ViewPage pg, IList<Product> data) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "ProductList"),data);
        }

        public static void CategoryListView(this ViewPage pg) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "CategoryListDisplay"));
        }

        public static void ProductDetailView(this ViewPage pg) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "ProductDetailDisplay"));

        }

        public static void UserSummaryView(this ViewPage pg) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "UserSummaryDisplay"));

        }
        public static void UserSummaryView(this ViewMasterPage pg) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "UserSummaryDisplay"));
        }


        public static void LoginView(this ViewPage pg) {
            pg.Html.RenderPartial(ThemeExtensions.TemplatedLink(pg.ViewContext, "LoginForm"));
        }

        public static string Metas(this HtmlHelper helper, KonaSite site) {
            string metaformat = "<meta name=\"{0}\" content=\"{1}\" />\r\n";
            StringBuilder sb = new StringBuilder();

            //keywords
            sb.AppendFormat(metaformat, "keywords", site.MetaKeywords);
            sb.AppendFormat(metaformat, "description", site.MetaDescription);
            foreach (string s in site.MetaKeywordsParsed.Keys) {
                sb.AppendFormat(metaformat, s, site.MetaKeywordsParsed[s]);
            }
            return sb.ToString();
        }

        static string TemplatedLink(ViewContext context, string partial) {
            string linkFormat = "~/Views/Themes/{0}/Shared/{1}";
            object themeName = context.HttpContext.Items["template"] ?? "Default";

            if (!partial.EndsWith(".aspx") | !partial.EndsWith(".ascx")) {
                linkFormat += ".ascx";
            }

            return string.Format(linkFormat, themeName.ToString(), partial);
        }

        public static string ThemeImage(this HtmlHelper helper, string image) {
            object themeName = helper.ViewContext.HttpContext.Items["template"] ?? "Default";
            string linkFormat = "~/Views/Themes/{0}/images/{1}";
            string linkUrl = string.Format(linkFormat, themeName, image);
            string rootRelUrl=System.Web.VirtualPathUtility.ToAbsolute(linkUrl);
            return rootRelUrl;
        }

        public static IList<string> InstalledThemes(this ViewPage pg) {
            string themeDirectory = pg.ViewContext.HttpContext.Server.MapPath("~/Views/Themes");

            List<string> result = new List<string>();
            foreach (string s in System.IO.Directory.GetDirectories(themeDirectory)) {
                string themeName = System.IO.Path.GetFileNameWithoutExtension(s).Trim();
                if (!themeName.Equals("admin", StringComparison.InvariantCultureIgnoreCase) &! string.IsNullOrEmpty(themeName))
                    result.Add(themeName);
            }
            return result;

        }


        public static IList<string> ThemePages(this ViewPage pg) {
            List<string> themePages = new List<string>();
            if (pg.ViewData["ThemePages"] != null)
                themePages = (List<string>)pg.ViewData["ThemePages"];
            return themePages;

        }
        public static string ThemeCode(this ViewPage pg) {
            string themeCode = "";
            if (pg.ViewData["ThemeCode"] != null)
                themeCode = pg.ViewData["ThemeCode"].ToString();
            return themeCode;

        }
    }


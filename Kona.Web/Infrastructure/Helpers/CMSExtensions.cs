using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Data;
using System.Web.Mvc;
using Kona.Infrastructure;

    public static class CMSExtensions {

        public static Page CurrentPage(this ViewMasterPage pg) {
            return CurrentPage(pg.Page as ViewPage);
        }

        public static Page CurrentPage(this ViewUserControl pg) {
            return CurrentPage(pg.Page as ViewPage);
        }
        public static Page CurrentPage(this ViewPage pg) {
            Page result = null;
            if (pg.ViewData["CurrentPage"] != null) {
                result = (Page)pg.ViewData["CurrentPage"];
            }
            return result;
        }
        public static IList<Page> SitePages(this ViewMasterPage pg) {
            return SitePages(pg.Page as ViewPage);
        }
        public static IList<Page> SitePages(this ViewPage pg) {
            IList<Page> result = null;
            if (pg.ViewData["SitePages"] != null) {
                result = (IList<Page>)pg.ViewData["SitePages"];
            }
            return result;

        }

    }

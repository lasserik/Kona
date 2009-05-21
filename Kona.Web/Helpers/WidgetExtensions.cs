using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kona.Data;
using System.Web.Mvc.Html;
using Kona.Infrastructure;
    public static class WidgetExtensions {

        public static void RenderLeftWidgets(this ViewPage pg) {
            pg.RenderWidgets("left");
        }
        public static void RenderRightWidgets(this ViewPage pg) {
            pg.RenderWidgets("right");

        }
        public static void RenderCenterWidgets(this ViewPage pg) {
            pg.RenderWidgets("center");

        }
        public static void RenderWidgets(this ViewMasterPage pg, string zone) {
            RenderWidgets(pg.Page as ViewPage, zone);
        }
        public static void RenderWidgets(this ViewPage pg, string zone) {
            if (pg.CurrentPage() != null) {
                RenderWidgets(pg, pg.CurrentPage(),false, zone);
            }
        }

        public static void RenderWidgets(this ViewPage pg,Page page, bool useEditor, string zone) {
            if (page != null) {


                foreach (Widget widget in page.Widgets.Where(x => x.Zone.Equals(zone, StringComparison.InvariantCultureIgnoreCase))) {

                    string viewName = useEditor ? widget.WidgetDefinition+"Edit" : widget.WidgetDefinition;

                    if (widget.WidgetDefinition != null) {
                        pg.Html.RenderPartial(viewName, widget);
                    } else if (!string.IsNullOrEmpty(widget.Title)) {
                        pg.Html.RenderPartial("TitleAndText", widget);
                    } else {
                        pg.Html.RenderPartial("TextOnly", widget);
                    }
                }
            }
        }



        public static void RenderAddressDisplay(this ViewPage pg, Address address) {
            pg.Html.RenderPartial("AddressDisplay", address);
        }
        public static void RenderAddressEntry(this ViewPage pg) {
            pg.Html.RenderPartial("AddressEntry");
        }
        public static void RenderCreditCard(this ViewPage pg) {
            pg.Html.RenderPartial("CreditCard");
        }
    }

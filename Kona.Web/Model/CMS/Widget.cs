using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Kona.Data {

    public partial class Widget{


        public IList<Product> Products { get; set; }

        public void LoadProducts() {
            this.Products = (from p in _db.Products
                               join wp in _db.Widgets_Products on p.SKU equals wp.SKU
                               where wp.WidgetID==this.WidgetID
                               select p).ToList();

        }

        public void SaveProductAssociations(string[] skus) {
            
            //delete the associations for this Widget
            Widgets_Product.Delete(x => x.WidgetID == this.WidgetID);
            foreach (var sku in skus) {
                Widgets_Product map = new Widgets_Product();
                map.SKU = sku;
                map.WidgetID = this.WidgetID;
                map.Add("");
            }
        }

    }

    public static class WidgetExtensions {
        public static void RenderWidgetsToZone(this ViewPage pg, string zone) {
            var widgets = pg.CurrentPage().Widgets.Where(x => x.Zone.StartsWith(zone)).OrderBy(x => x.ListOrder);
            foreach (var widget in widgets) {
                pg.Html.RenderPartial(widget.WidgetDefinition,widget);
            }
        }
    }

}

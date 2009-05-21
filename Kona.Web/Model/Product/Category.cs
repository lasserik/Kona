using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Data {
    public partial class Category {

        public List<Category> SubCategories { get; set; }

        public static List<Category> GetHierarchicalCategories() {
            var db=new KonaDB();

            var allCategories = db.Select.From<Category>().InnerJoin<CategoryLocalized>().Where("LanguageCode")
                .IsEqualTo(System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                .ExecuteTypedList<Category>();

            //var allCategories = from c in db.Categories
            //           join cl in db.CategoryLocalizeds on c.CategoryID equals cl.CategoryID
            //           where cl.LanguageCode == System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            //           select new Category {
            //               ParentID = c.ParentID,
            //               CategoryID = c.CategoryID,
            //               LocalizedName = cl.Name,
            //               Slug = cl.Slug,

            //           };
            List<Category> result = allCategories.Where(x => x.ParentID == null).ToList();

            result.ForEach(x => x.SubCategories = 
                allCategories.Where(y => y.ParentID == x.CategoryID).ToList());

            return result;
        }

        public static Category GetCategoryPage(string slug){

            //pull the category
            CategoryLocalized localized=null;
            if(!string.IsNullOrEmpty(slug))
                localized=CategoryLocalized.SingleOrDefault(x=>x.Slug.ToLower()==slug.ToLower() && x.LanguageCode==System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            
            //default is to pull the home page
            if(localized==null){
                //it's a home page - pull em
                localized=CategoryLocalized.SingleOrDefault(x=>x.IsHome && x.LanguageCode==System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }

            //shouldn't be null - if it is then we have an issue
            if (localized == null)
                throw new Exception("There is no home category defined for the site - better set 'em");

            Category result = Category.SingleOrDefault(x => x.CategoryID == localized.CategoryID);

            result.Name = localized.Name;
            result.Slug = localized.Slug;
            //set the products
            KonaDB db = new KonaDB();
            result.Products = (from p in db.Products
                               join ci in db.Categories_Products on p.SKU equals ci.SKU
                               select p).Distinct().ToList() ?? new List<Product>();
            
            //pull the widgets
            result.Widgets = (from w in db.Widgets
                              join cw in db.Categories_Widgets on w.WidgetID equals cw.WidgetID
                              select w).ToList() ?? new List<Widget>();


            //set the products for the Widget
            foreach (var widget in result.Widgets) {
                widget.Products = new List<Product>();
                foreach (string sku in widget.GetSkuArray()) {
                    var widgetProduct = result.Products.SingleOrDefault(x => x.SKU.Equals(sku, StringComparison.InvariantCultureIgnoreCase));
                    if (widgetProduct != null)
                        widget.Products.Add(widgetProduct);
                }
            }


            return result;
        }
        public string Name { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Widget> Widgets { get; set; }
        public string Slug { get; set; }
    }
}

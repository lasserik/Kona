using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Kona.Data {

    public partial class Widget{

        public string[] GetSkuArray() {
            string[] result = new string[0];
            
            if(!String.IsNullOrEmpty(this.SKUList))
                result= this.SKUList.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);

            return result;
        }
        public IList<Product> Products { get; set; }

        public void LoadProducts() {

            //use some SubSonic love here
            if (!String.IsNullOrEmpty(this.SKUList)) {
                KonaDB db = new KonaDB();
                string[] skus=this.GetSkuArray();
                var products = db.Select.From<Product>().Where("sku").In(skus).ExecuteTypedList<Product>();
            
                //order it - since IN isn't a very good ordering thinger
                this.Products = new List<Product>();
                foreach (string s in skus) {
                    this.Products.Add(products.Single(x => x.SKU == s));
                }
            }

        }

        partial void OnCreated() {
            this.WidgetID = Guid.NewGuid();
            this.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            this.ListOrder = 0;
            this.Title = "";
            this.Body = "";
            this.SKUList = "";
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
        }
    }

}

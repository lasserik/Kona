using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Mvc.Html;

namespace Kona.Infrastructure {
    public static class HtmlExtensions {

        public static string IsChecked(this HtmlHelper helper, object listItem, object checkAgainst){

            return  listItem.Equals(checkAgainst) ? "checked=\"checked\"" : "";

        }
        public static string IsChecked(this HtmlHelper helper,bool checkValue) {

            return checkValue ? "checked=\"checked\"" : "";

        }

        public static string IsSelected(this HtmlHelper helper, object listItem, object checkAgainst) {

            return listItem.Equals(checkAgainst) ? "selected=\"selected\"" : "";
            
        }

        public static string ForProperty(this HtmlHelper helper, PropertyInfo prop, object value) {
            value = value ?? "";
            string result = helper.TextBox(prop.Name, value.ToString(), new { size = 40 });
            //take a look at the type
            Type t = prop.PropertyType;
            //the difference here will be for numbers, dates, and booleans
            if (t==typeof(bool)) {
                bool isChecked = false;
                bool.TryParse(value.ToString(), out isChecked);
                string checkFlag = "";
                if(isChecked)
                    checkFlag = "checked=\"checked\"";

                result = string.Format("<input name=\"{0}\" type=\"checkbox\" value=\"true\" {1} />", prop.Name,checkFlag); ;

            } else if (t ==typeof(DateTime)) {
                //tricky jQuery
                result = helper.TextBox(prop.Name, value.ToString(), new { size = 15, @class="datetime" });
            } else if (IsNumeric(t)) {
                result = helper.TextBox(prop.Name, value.ToString(), new { size = 4 });
            }
            return result;
        }

        static bool IsNumeric(Type t) {

            return t == typeof(int) ||
                t == typeof(decimal) ||
                t == typeof(double);
        }

    }
}

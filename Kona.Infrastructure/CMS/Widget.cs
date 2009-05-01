using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace Kona.Infrastructure {

    public class Widget : IWidget {
        public Guid PageID { get; set; }
        public Guid ID { get; set; }
        public Widget() { }

        public Widget(Guid pageID, string title, string viewName, string editorName, int listOrder, string zone, string body, string languageCode) {
            this.Title = title;
            this.ListOrder = listOrder;
            this.Zone = zone;
            this.ViewName = viewName;
            this.EditorName = editorName;
            this.PageID = pageID;
            this.Body = body;
            this.LanguageCode = "en";
            this.ID = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
        }
        public string Title { get; set; }
        public string Body { get; set; }

        public int ListOrder { get; set; }
        public string Zone { get; set; }
        public string ViewName { get; set; }
        public string LanguageCode { get; set; }
        public string EditorName { get; set; }
        public bool IsTyped { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
       
    }

    [DataContract]
    public class Widget<T> : Widget {

        public T Data { get; set; }

    }
}

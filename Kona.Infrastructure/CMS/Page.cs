using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Infrastructure {


    public enum PublishStatus {
        Draft=1,
        Published=2,
        Offline=3
    }

    public partial class Page {

        public Page() {
            this.PageID = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
            this.Status = PublishStatus.Draft;
            this.Widgets = new List<IWidget>();
            this.ModifiedBy = "";
            this.CreatedBy = "";
            this.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
       }

        public IList<IWidget> Widgets { get; set; }
        public string Url { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public Guid PageID { get; set; }
        public string Slug { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PublishStatus Status { get; set; }
        public int ListOrder { get; set; }
    }
}

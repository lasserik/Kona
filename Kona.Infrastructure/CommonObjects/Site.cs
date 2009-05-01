using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Kona.Infrastructure {
    [DataContract()]
    //TODO:Rename this to a more descriptive term
    public partial class Site {
        public Site() {
        }

        public Site(string siteUrl, string siteHost, string owner, string siteName, string adminEmail, string languageCode) {
            this.SiteID = Guid.NewGuid();
            this.SiteUrl = siteUrl;
            this.SiteHost = siteHost;
            this.Owner = owner;
            this.SiteName = siteName;
            this.AdminEmail = adminEmail;
            this.LanguageCode = languageCode;
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
        }

        [DataMember]
        public Dictionary<string, string> Localized { get; set; }
        [DataMember]
        public Guid SiteID { get; set; }
        [DataMember]
        public string SiteUrl { get; set; }
        [DataMember]
        public string SiteHost { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string SiteName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string AdminEmail { get; set; }
        [DataMember]
        public string SMTPServer { get; set; }
        [DataMember]
        public string SMTPLogin { get; set; }
        [DataMember]
        public string SMTPPassword { get; set; }
        [DataMember]
        public string LanguageCode { get; set; }
        [DataMember]
        public string ThemeName { get; set; }
        [DataMember]
        public bool IsPublic { get; set; }
        [DataMember]
        public string MetaKeywords { get; set; }
        [DataMember]
        public string MetaDescription { get; set; }
        [DataMember]
        public string MetaTags { get; set; }
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public string RPXAPIKey { get; set; }
        [DataMember]
        public string RPXNowUrl { get; set; }

        public string Localize(string phraseKey){
            return Localized.Where(x => x.Key == phraseKey).SingleOrDefault().Value;
        }

        //TODO: clearly, rewrite this with REGEX :p
        public Dictionary<string, string> MetaKeywordsParsed {
            get {
                Dictionary<string, string> result = new Dictionary<string, string>();
                if (!String.IsNullOrEmpty(this.MetaTags)) {
                    string[] items = this.MetaTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in items) {
                        string[] metas = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (metas.Length > 0)
                            result.Add(metas[0], metas[1]);
                    }
                }
                return result;
            }
        }
    }
}

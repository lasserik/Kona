using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Kona.Infrastructure;
using System.Data.Common;

namespace Kona.Data {

    public enum PublishStatus {
        Draft = 1,
        Published = 2,
        Offline = 3
    }

    public partial class Page {

        public static Page GetDraft(Guid id) {
            return Page.All().SingleOrDefault(x => x.PageID == id && x.PageStatusID==(int)PublishStatus.Draft);
        }
        
        partial void OnCreated(){
            this.PageID = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
            this.Status = PublishStatus.Draft;
            this.ModifiedBy = "";
            this.CreatedBy = "";
            this.Slug = "";
            this.LanguageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        }

        
        public static IEnumerable<Page> GetParentPages() {
            //Home is the root page, and "top level" pages are considered the child of the home page
            //return home and it's droogs
            var home = Page.SingleOrDefault(x => x.Slug == "");
            List<Kona.Data.Page> result = new List<Page>();
            result.Add(home);

            ////add the child pages
            result.AddRange(Page.Find(x => x.ParentPageID == home.PageID));
            return result;
        }

        public void LoadWidgets() {
            var allProducts = (from p in _db.Products
                               join wp in _db.Widgets_Products on p.SKU equals wp.SKU
                               join w in _db.Widgets on wp.WidgetID equals w.WidgetID
                               where w.PageID == this.PageID
                               select p).ToList();


            var widgetProducts = (from wp in _db.Widgets_Products
                                 join w in _db.Widgets on wp.WidgetID equals w.WidgetID
                                 where w.PageID == this.PageID
                                 select wp).ToList();

            foreach (var widget in this.Widgets) {

                widget.Products = (from p in allProducts
                                   join wp in widgetProducts on p.SKU equals wp.SKU
                                   where wp.WidgetID == widget.WidgetID
                                   select p).ToList();

                widget.Products = widget.Products.RemoveDuplicates();
            }

        }

        public static Page GetPage(string slug) {
            var cmsPage = Kona.Data.Page.SingleOrDefault(x => x.PageStatusID==(int)PublishStatus.Published
                && x.Slug == slug
                && x.LanguageCode == System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            return cmsPage;
        }

        public IList<Widget> GetWidgetByZone(string zone) {
            return this.Widgets.Where(x => x.Zone.Equals(zone, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public PublishStatus Status {
            get {
                return (PublishStatus)this.PageStatusID;
            }
            set {
                this.PageStatusID = (int)value;
            }
        }

        public void DeletePage() {
            var commands = new List<DbCommand>();
            
            //re-position the children
            //var parent = new Page(this.ParentPageID);
            
            //if (parent != null) {
            //    if (Page.All().Any(x => x.ParentPageID == this.PageID)) {
            //        IList<Page> children = Page.All().Where(x => x.ParentPageID == this.PageID).ToList();
            //        foreach (var child in children) {
            //            child.ParentPageID = parent.PageID;
            //            //add to the command stack
            //            commands.Add(child.GetUpdateCommand());
            //        }
            //    }
            //}

            ////delete the draft
            //if (this.PrimaryOrDraftPageID.HasValue) {
            //    var delete = new Page(this.PrimaryOrDraftPageID.Value);
            //    commands.Add(delete.GetDeleteCommand());
            //}
            ////delete the parent
            //commands.Add(this.GetDeleteCommand());

            //var db = new KonaDB();
            //db.ExecuteTransaction(commands);
        }

        public void SetAsPublished() {
            this.Status = PublishStatus.Published;
            //this.IsDraftPage = false;
            //this.PrimaryOrDraftPageID = null;

        }

        public static IList<Page> GetDrafts() {
            return Page.Find(x => x.PageStatusID == (int)PublishStatus.Draft);
        }

        public static bool SlugExists(string slug) {
            return Page.All().Any(x => x.Slug == slug);
        }

        public void Validate() {
            //need a title
            if (String.IsNullOrEmpty(this.Title))
                throw new InvalidOperationException("Title cannot be null");

            //need a slug
            //if (String.IsNullOrEmpty(this.Slug) &! this.IsHomePage)
            //    throw new InvalidOperationException("Slug cannot be null");


            //need a template
            //if (String.IsNullOrEmpty(this.ViewName))
            //    throw new InvalidOperationException("Template cannot be null");
            
            //slug can't exist
            if(Page.SlugExists(this.Slug))
                throw new InvalidOperationException("This slug is already taken");
        }

    }
}

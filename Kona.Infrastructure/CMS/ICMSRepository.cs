using System;
using System.Collections.Generic;
using Kona.Infrastructure;
namespace Kona.Model {
    public interface ICMSRepository {
        Page GetPage(string slug, string languageCode);
        Page GetPage(Guid id);
        IWidget GetWidget(Guid id);
        void Update(Page pg);
        void Create(Page pg);
        void SaveWidget(IWidget widget);
        IList<Page> GetPages();
        IList<Page> GetUnpublishedPages();
        IList<Page> GetPages(PublishStatus status);
        void DeletePage(Guid id);
        void DeletePage(string slug);
        void DeleteWidget(Guid id);
        IList<IWidget> GetWidgets(string zone, Guid pageID);
        void SaveProductWidget(Guid widgetID, string[] skus);
        void OrderWidgets(string[] widgetIDs);
        void OrderPages(string[] pageIDs);
    }
}

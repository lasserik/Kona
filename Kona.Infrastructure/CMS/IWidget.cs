using System;
namespace Kona.Infrastructure {
    public interface IWidget {

        Guid PageID { get; set; }
        Guid ID { get; set; }
        string Body { get; set; }
        string LanguageCode { get; set; }
        int ListOrder { get; set; }
        string Title { get; set; }
        string ViewName { get; set; }
        string EditorName { get; set; }
        string Zone { get; set; }
        bool IsTyped { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}

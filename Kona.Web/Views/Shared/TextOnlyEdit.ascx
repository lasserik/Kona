<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
<div>
    <textarea rows=7 cols=18 style="size:10pt;border:1px dashed #990000" 
        onblur="saveWidgetBody('<%=Model.ID %>', this.value);"><%=Model.Body%></textarea>
</div>

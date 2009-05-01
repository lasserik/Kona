<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
<label>Enter HTML text:</label>
<textarea name="body_<%=Model.ID %>" id="body_<%=Model.ID %>"
    style="width:500px;height:150px;" 
    onblur="saveWidgetBody('<%=Model.ID %>', this.value)"><%=Model.Body%></textarea>

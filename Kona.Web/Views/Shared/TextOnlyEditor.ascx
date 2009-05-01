<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
            
 <div id='<%=Model.ID%>'>
        <div style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand" class="sorthandle">
            <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.ID%>')"/>
        </div>
        <div>
            <textarea rows=7 cols=18 style="size:10pt;border:1px dashed #990000" onblur="saveWidget('<%=Model.ID %>','',this.value);"><%=Model.Body %></textarea>
        </div>
  </div>
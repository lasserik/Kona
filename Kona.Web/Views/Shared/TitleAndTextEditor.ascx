<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
 <div id='<%=Model.ID%>'>
        <div style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand">
            <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.ID%>')"/>
        </div>
        <fieldset>
            <legend><%=Model.Title %></legend>
            <%=Model.Body %>
        </fieldset>
 </div>
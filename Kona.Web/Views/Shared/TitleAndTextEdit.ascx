<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
 <div id='<%=Model.ID%>'>
        <fieldset>
            <legend><%=Model.Title %></legend>
            <%=Model.Body %>
        </fieldset>
 </div>
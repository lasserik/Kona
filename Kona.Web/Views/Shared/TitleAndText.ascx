<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
            <fieldset>
                <legend><%=Model.Title %></legend>
                <%=Model.Body %>
            </fieldset>
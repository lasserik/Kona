<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>

    <h3><%=Model.Title %></h3>
    <%for (int i = 1; i <= 10; i++) { %>
    <li><a href=#>Top Search <%=i %></a></li>
    <%} %>
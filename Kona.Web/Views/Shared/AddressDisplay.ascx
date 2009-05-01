<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Address>" %>
<%if(Model!=null){ %>
<b><%=Model.FullName%></b><br />
<%=Model.Street1 %><br />
<%if (!String.IsNullOrEmpty(Model.Street2)) { %>
<%=Model.Street2%><br />
<%} %>
<%=Model.City%>, <%=Model.StateOrProvince%><br />
<%=Model.Country%>
<%} %>
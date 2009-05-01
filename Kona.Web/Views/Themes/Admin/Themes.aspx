<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Themes</h2>

    <%foreach (string theme in this.InstalledThemes()) { %>
    <fieldset>
    
        <legend><%=theme%></legend>
        <a href="<%=Url.Action("Edit","Theme",new {id=theme}) %>">Edit</a>
    </fieldset>

    <%} %>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<title><%=this.SiteData().SiteName %>: Plugin Administration</title>
    <link rel="Stylesheet" type="text/css" href="/scripts/jquery-ui.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/scripts/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/scripts/jquery-ui.js"></script>
    <script type="text/javascript">
       $(document).ready(function() {
        $(".datetime").datepicker();

       });
    </script>
    <h2>Plugins</h2>
    
    <%if(ViewData["Message"]!=null) {%>
        <h1><%=ViewData["Message"].ToString() %></h1>
    <%}%>

   <%foreach (var plug in this.Plugins()) {%>
    <fieldset>
    
        <legend><%=plug.PluginName%></legend>
        <a href="<%=Url.Action("Edit","Plugins",new {id=plug.PluginName}) %>">Edit</a>
    </fieldset>
    
    <%} %>

</asp:Content>


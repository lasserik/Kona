<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Themes/Admin/Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<title><%=this.SiteData().SiteName %>: Edit <%=this.SelectedPlugin().PluginName %></title>
    <link rel="Stylesheet" type="text/css" href="/scripts/jquery-ui.css" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/scripts/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/scripts/jquery-ui.js"></script>
    <script type="text/javascript" src="/scripts/edit_area/edit_area_full.js"></script>
    <%var plug = this.SelectedPlugin();
      string excludeProps = "Settings,Code,FilePath,PluginName";
      string extension = System.IO.Path.GetExtension(plug.FilePath);
    %>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".datetime").datepicker();
            editAreaLoader.init({
                id: "code",
                start_highlight: true,
                syntax: "<%= (new Dictionary<string, string>() {
                    // Ouch, but this will do for now...
                    {".cs", "c"},
                    {".js", "js"},
                    {".vb", "vb"},
                    {".rb", "ruby"},
                    {".py", "python"}
                })[extension] %>"
            });
        });
    </script>
 
 
    <h2><a href="<%=Url.Action("Index")%>">Plugins</a> >>> Edit Plugin: <%=this.SelectedPlugin().PluginName %></h2>
      <%using (Html.BeginForm()) {
        System.Reflection.PropertyInfo[] props = plug.GetType().GetProperties();
        if(props.Length>0){%>
      
       <h3>Properties</h3>
       <table>
       <%foreach (var prop in props.Where(x => !excludeProps.Contains(x.Name))) {%>
        <tr>
            <td><%=prop.Name %></td>
            <td><%=Html.ForProperty(prop,plug.GetSetting(prop.Name))%></td>
        </tr>
        <%}%>
    <%}%> 
    </table>
    <h3>Code</h3>
        <input type=hidden name="id" value="<%=plug.PluginName%>" />
        <textarea style="width:100%" name="code" id="code"><%=plug.Code%></textarea><br />
        <input type=submit value="Save" />
    <%} %>
</asp:Content>


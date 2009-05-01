<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/scripts/edit_area/edit_area_full.js"></script>
    <script type="text/javascript">
        editAreaLoader.init({
            id: "code",
            start_highlight: true,
            syntax: "<%= (new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
                // Ouch, but this will do for now...
                {".cs", "c"},
                {".js", "js"},
                {".vb", "vb"},
                {".rb", "ruby"},
                {".py", "python"},
                {".htm", "html"},
                {".html", "html"},
                {".aspx", "html"},
                {".ascx", "html"},
                {".master", "html"},
                {".xml", "xml"},
                {".css", "css"}
            })[System.IO.Path.GetExtension((string)ViewData["ThemePage"])] %>"
        });
    </script>
    <h2><a href="<%=Url.Action("Index","Theme") %>">Themes</a> >>> Edit <%=ViewData["ThemeId"]%> </h2>
    <%using (Html.BeginForm()) { %>
    <table cellpadding=4>
        <tr>
            <td valign=top>
                <ul>
                <%foreach (string page in this.ThemePages()) {
                      string pageName = System.IO.Path.GetFileName(page);
                %>
                   
                    <li><a href="<%=Url.Action("Edit","Theme",new{id=ViewData["ThemeId"],page=pageName}) %>"><%=pageName%></a></li>
                    
                <%} %>
                </ul>
            </td>
            <td>
                 <textarea style="width:100%" name="code" id="code"><%=this.ThemeCode()%></textarea>
                 <input type=hidden value="<%=ViewData["ThemePage"]%>" name="themePage"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="save" /></td>
       </tr>
    </table>
    <%} %>
</asp:Content>



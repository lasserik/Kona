<%@ Page ValidateRequest="false" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage<IList<Kona.Infrastructure.Page>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript" src="/scripts/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/scripts/jquery.contextMenu.js"></script>

<div id="bdy">
    <%=Html.ValidationSummary() %>
    <form action="<%=Url.Action("Create","Page") %>" method="post" id="pageForm">
    <div class="cmsedit"><h2>Parent</h2>
        <select name="parent">
            <option value="0">-- none --</option>
           <%foreach (Kona.Infrastructure.Page pg in this.SitePages() ?? new List<Kona.Infrastructure.Page>()) { %>
            <option value="<%=pg.PageID %>"><%=pg.Title %></option>
            <%} %>
        </select>
    </div>    
    <br />
    <div class="cmsedit">
        <input type="text" name="title" id="title" value="" size="40" />
        <input type=submit value="go" />
        <div class="smalltext">This is the title that will appear at the top, and is optional</div>
    </div>

    <div id="rightcol">
        <fieldset>
            <legend>Current Drafts</legend>
            <%foreach (Kona.Infrastructure.Page pg in Model) { %>
            <li><a href="<%=Url.Action("Edit","Page", new {id=pg.PageID}) %>"><%=pg.Title %></a></li>
            <%} %>
        </fieldset>
    
    </div>
</div>
</form>

   
</asp:Content>


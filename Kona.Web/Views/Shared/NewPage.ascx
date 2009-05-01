<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kona.Infrastructure.Page>" %>

<form id="Editor" name="create" method="post" class="LE" action="<%=Url.Action("Create", "Page") %>">
    <div class="error"><%= Html.ValidationSummary() %></div>
    <div style="float:left;">
        <label for="ParentPageID">Parent:</label>
        <select name="ParentPageID" id="Select4" size="6">
                    <%foreach (PageNode pageNode in this.SitePages().GetPageHierarchy().GetFlatList()) { %>
                        <option value="<%= pageNode.Page.PageID %>">
                                <%= new string('-', pageNode.Depth) %> <%= pageNode.Page.Title %>
                        </option>
                    <%} %>
        </select>
    </div>

    <div>
        <label for="title">Title:</label>
        <input type="text" name="title" id="Text3" class="textEntry" size="40" value=""/>

        <label for="slug">Slug:</label>
        <input type="text" name="slug" id="Text4" class="textEntry" size="40" value=""/>

        <label for="ViewName">Template:</label>
        <select name="ViewName" id="Select1">
            <%foreach (string viewName in Kona.Infrastructure.Theme.GetCMSViewNames(this.SiteData().ThemeName)) { %>
                <option value="<%=viewName%>"><%=viewName%></option>
            <%} %>
        </select>
    </div>
</form>

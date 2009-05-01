<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kona.Infrastructure.Page>" %>

<form id="Editor" name="edit" class="LE" method="post" action="<%=Url.Action("Update", "Page") %>">       
<div class="error"><%= Html.ValidationSummary() %></div>

<div style="float:left;">
<label for="ParentPageID">Parent:</label>
<select name="ParentPageID" id="Select1" size="6"
    <% if (Model != null && (Model.IsHomePage || !CMSExtensions.IsPageInEditMode(this))) { %> disabled="disabled" <% } %> >
                    <% if (Model != null && Model.IsHomePage) { %>
                        <option value="">-- none --</option>
                    <% } %>
                    
                    <%foreach (PageNode pageNode in this.SitePages().GetPageHierarchy().GetFlatList(Model)) { %>
                        <option
                            value="<%= pageNode.Page.PageID %>"
                            <% if (Model != null && Model.ParentPageID == pageNode.Page.PageID) { %> selected="selected" <% } %> >
                                <%= new string('-', pageNode.Depth) %> <%= pageNode.Page.Title %>
                        </option>
                    <%} %>
</select>
</div>

<div>

<label for="title">Title:</label>
<input type="text" name="title" id="Text2" class="textEntry" size="40"
    value="<%if (Model != null) {Response.Write(Model.Title);} %>" <% if (Model != null && !CMSExtensions.IsPageInEditMode(this)) { %> disabled="disabled" <% } %> />


<label for="slug">Slug:</label>
<input type="text" name="slug" id="Text4" class="textEntry" size="40" 
    value="<%if (Model != null) {Response.Write(Model.Slug);} %>"
    <% if (Model != null && (Model.IsHomePage || !CMSExtensions.IsPageInEditMode(this))) { %> disabled="disabled" <% } %>/>


<label for="ViewName">Template:</label>
<select name="ViewName" id="Select2" <% if (Model != null && !CMSExtensions.IsPageInEditMode(this)) { %> disabled="disabled" <% } %>>
    <%foreach (string viewName in Kona.Infrastructure.Theme.GetCMSViewNames(this.SiteData().ThemeName)) { %>
        <option
            value="<%=viewName%>"
            <% if (Model != null && Model.ViewName == viewName) { %> selected="selected" <% } %> >
                <%=viewName%>
        </option>
                    <%} %>
</select>
</div>

</form>

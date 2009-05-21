<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: <%=this.CurrentCategory().LocalizedName %></title>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy" class="bkgrnd3">
    <div id="leftcolumn">
        <%this.RenderWidgets("sidebar1", false); %>
    </div>
  
    <div class="colmiddle fltleft">
        <%this.RenderWidgets("center",false); %>
    </div>
  
    <div class="colright fltright">
        <%//if (this.user() != null) { %>
        <div>
            <div class="sidebarContent">
                <ul class="leftnavList">
                    <li><img src="/content/icons/cog_edit.png" /> <a href="<%//=Url.Action("Index","Site") %>">Site Config</a></li>
                    <li><img src="/content/icons/page_edit.png" /> <a href="<%=Url.Action("Edit","Home", new{slug=this.CurrentCategory().Slug})%>">Edit This Page</a></li>
                    <li><img src="/content/icons/page_add.png" /> <a href="<%//=Url.Action("New","Page") %>">New Page</a></li>
                    <li><img src="/content/icons/page_paintbrush.png" /> <a href="<%//=Url.Action("List","Page") %>">Listing of Pages</a></li>
                </ul>
            </div>
        </div>
        <%//} %>
       <div class="sidebarWidget bkgrnd4"><h5>Search</h5>
           <div class="sidebarContent"><form><input type="text" /><br /><input type="submit" value="Search" /></form></div>
       </div>
      
        <%this.RenderWidgets("sidebar2", false); %>
    </div>


</div>
</asp:Content>

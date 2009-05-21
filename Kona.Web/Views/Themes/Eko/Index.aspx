<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName%>: <%=this.CurrentCategory().Name%></title>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="/scripts/aria/fg.menu.js"></script>
    
<link type="text/css" href="/scripts/aria/fg.menu.css" media="screen" rel="stylesheet" />
<link type="text/css" href="/scripts/aria/theme/ui.all.css" media="screen" rel="stylesheet" />
    <script type="text/javascript">    
    $(function(){
    	
    	// MENUS    	
		$('#flat').menu({ 
			content: $('#flat').next().html(), // grab content from this page
			showSpeed: 400 
		});
		

    });
    </script>
<div id="bdy" class="bkgrnd3">
    <div id="leftcolumn">
        <div class="leftmainnav">
            <h2 >Find It Here</h2>
            <ul class="sf-menu">
            <%foreach (var category in this.Categories()) { %>
                <li><a href="#"><%=category.Name%></a>
                </li>
            <%} %>
            </ul>  
        </div>  
        <%this.RenderWidgets("sidebar1", false); %>
    </div>
  
    <div id="center">
        <div class="article_wrapper">
        <%this.RenderWidgets("center", false); %>
        </div>
    </div>
  
    <div id="rightcolumn">
        <%//if (this.user() != null) { %>
        <div class="article_wrapper">
            <ul class="leftnavList">
                <li><img src="/content/icons/cog_edit.png" /> <a href="<%//=Url.Action("Index","Site") %>">Site Config</a></li>
                <li><img src="/content/icons/page_edit.png" /> <a href="<%=Url.Action("Edit","Home", new{slug=this.CurrentCategory().Slug})%>">Edit This Page</a></li>
                <li><img src="/content/icons/page_add.png" /> <a href="<%//=Url.Action("New","Page") %>">New Page</a></li>
                <li><img src="/content/icons/page_paintbrush.png" /> <a href="<%//=Url.Action("List","Page") %>">Listing of Pages</a></li>
            </ul>
        </div>
        <%//} %>
        <div class="article_wrapper">
        <%this.RenderWidgets("sidebar2", false); %>
        </div>
    </div>


</div>
</asp:Content>

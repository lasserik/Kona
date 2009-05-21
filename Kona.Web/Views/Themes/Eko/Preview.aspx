<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <title><%=this.SiteData().SiteName %>:</title>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy" class="leftNav">
    <div class="colleft fltleft"> 
        <%this.RenderWidgets("sidebar1"); %>
    </div>
  
  <div class="colmiddle fltleft">
    <%this.RenderCenterWidgets(); %>
  </div>
  
  <div class="colright fltright">
    <div class="sidebarWidget"><h5>Admin</h5>
        <div class="sidebarContent">
            <ul class="leftnavList">
            <li><input type=button onclick="location.href='<%=Url.Action("Edit","Page",new {id=this.CurrentPage().PageID}) %>'" value="Return"/></li>
            <li>
            <form action="<%=Url.Action("SetStatus","Page",new {id=this.CurrentPage().PageID}) %>" method="post">
                <input type=submit value="Publish" name="status" />
            </form>
            </li>
            </ul>
        </div>
   </div>
   
   <div class="sidebarWidget"><h5>Search</h5>
   <div class="sidebarContent"><form><input type="text" /><br /><input type="submit" value="Search" /></form></div></div>
  
   <%this.RenderWidgets("sidebar2"); %>
</div>   
</div>
</asp:Content>

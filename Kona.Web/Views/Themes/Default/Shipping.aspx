<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Shipping</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy">
<div class="colmiddle780 cntr">

    <h4>Shipping Information</h4><hr />
    <p>
    
      <%if (User.Identity.IsAuthenticated) { %>
      <div class="addressBook">
        <%foreach (Address add in this.CurrentCustomer().AddressBook) { %>
            <%using(Html.BeginForm()){ %>
            
            <input type="hidden" name="id" value="<%=add.AddressID %>" />
            
            <div style="margin-bottom:20px">
                <b><%=add.FullName%></b><br />
                <%=add.Street1%><br />
                <%if (!String.IsNullOrEmpty(add.Street2)) { %>
                <%=add.Street2%><br />
                <%} %>
                <%=add.City%>, <%=add.StateOrProvince%><br />
                <%=add.Country%><br />
                
                <input type="submit" value="Use this address" />
            </div>
            
            <%} %>
        <% } %>
      </div>
     <%} %>
    <form action="<%=Url.Action("Shipping") %>" method="post">
        <%this.RenderAddressEntry(); %>
          <p class="action">
        <input type="submit" value="Billing &gt;&gt;&gt;" />
      </p>
    </form>
    </p>
</div>
</div>

</asp:Content>

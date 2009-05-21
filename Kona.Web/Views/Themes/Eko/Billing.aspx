<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Billing</title>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy">
<div class="colmiddle780 cntr">

   <%if (this.SiteData().AcceptPayPal) { %>
    <div class="paypal">
      <h4>Pay with PayPal</h4><hr />
      <p>
          <a href="<%=Url.Action("paypal","Order") %>" >
              <img src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" />
          </a>
      </p>
    </div> 
    <%} %>
    
    <%if (this.SiteData().AcceptCreditCards) { %>
      <%using (Html.BeginForm("Billing", "Order")) { %>

        
        
        <div>
        <h4>Billing Information</h4><hr />
            <%this.RenderAddressEntry();%>
          <p class="action">
            <input type="submit" value="Finalize &gt;&gt;&gt;" />
          </p>
        </div>
        
        <%} %>
    <%} %>
    </div>
 </div>
</asp:Content>

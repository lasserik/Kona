<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Checkout with PayPal</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy">
    <div class="colmiddle780 cntr">
        <h2>Pay with PayPal</h2>
            <div class="wrap">
                You are about to be redirected to PayPal.com to purchase this order. 
                Once payment is completed, please be sure to let PayPal redirect you 
                back to this site. When PayPal redirects you, they send along your transaction 
                information which we need in order to complete and reconcile your payment.
            </div>
        <h2>Select Shipping</h2>
        <%using (Html.BeginForm()) { %>
            <%foreach (ShippingMethod m in this.ShippingMethods().OrderBy(x=>x.Cost)) { %>
                
                <li><input onclick="this.form.submit();" type="radio" value="<%=m.ID %>" name="id" <%=Html.IsChecked(m.ID, this.CurrentCart().ShippingMethodID)%>> <%=m.Display %></li>
        
            <%} %>
        <%} %>
        <div class="wrap">
        <h2>Order Details</h2>

        <table width="400">
            <tr>
                <td width="50%">
                    <p><b>Shipping To:</b></p>
                    <p>
                    <%Html.RenderPartial("AddressDisplay",this.CurrentCart().ShippingAddress); %>
                    </p>
                </td>
                 <td width="50%">
                    <p><b>Billing To:</b></p>
                    <p>
                    
                    <%Html.RenderPartial("AddressDisplay", this.CurrentCart().BillingAddress); %>

                    </p>
                 </td>
            </tr>
        </table>        
        <hr />
        
        <table cellspacing="0" cellpadding="5" width="100%">
            <tr>
                <td><b>Quantity</b></td>
                <td ><b>Item</b></td>
                <td  align="right"><b>Regular</b></td>
                <td  align="right"><b>Total</b></td>
            </tr>

            <%foreach(ShoppingCartItem item in this.CurrentCart().Items){%>
            <tr>
                <td ><%=item.Quantity %></td>
                <td ><%=item.Product.Name%></td>
                <td  align="right"><%=item.Product.Price.ToString("C")%></td>
                <td  align="right"><%=item.LineTotal.ToString("C") %></td>
            </tr>

            <%} %>
             <tr>
                <td colspan="4"><hr /></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Subtotal</td>
                <td align="right"><%= this.CurrentCart().SubTotal.ToString("C")%></td>
             </tr>
              <tr>
                <td colspan="3" align="right">Tax</td>
                <td align="right"><%= this.CurrentCart().TaxAmount.ToString("C")%></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Shipping (<%= this.CurrentCart().ShippingService%>)</td>
                <td align="right"><%= this.CurrentCart().ShippingAmount.ToString("C")%></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Discount:</td>
                <td align="right">-<%= this.CurrentCart().DiscountAmount.ToString("C")%></td>
             </tr>
              <tr>
                <td colspan="3" align="right">Grand Total</td>
                <td align="right"><b><%= this.CurrentCart().Total.ToString("C")%></b></td>
             </tr>
        </table>
        </div>
        <form id="Paypal" name="Paypal" action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">

            <%=Html.Hidden("cmd", "_cart")%>
            <%=Html.Hidden("upload", "1")%>
            <%=Html.Hidden("business", this.SiteData().PayPalBusinessEmail)%>
            <%=Html.Hidden("custom", this.CurrentCart().UserName.ToString())%>
            <%=Html.Hidden("tax_cart", this.CurrentCart().TaxAmount.ToLocalCurrency())%>
            <%=Html.Hidden("currency_code", this.SiteData().CurrencyCode)%>
            
            <%=Html.Hidden("return", Url.Action("pdt","paypal"))%>
            <%=Html.Hidden("cancel_return", Url.Action("Show","Order"))%>
      
        <%if(this.CurrentCart().ShippingAddress!=null){ %>

            <%=Html.Hidden("first_name", this.CurrentCart().ShippingAddress.FirstName)%>
            <%=Html.Hidden("last_name", this.CurrentCart().ShippingAddress.LastName)%>
            <%=Html.Hidden("address1", this.CurrentCart().ShippingAddress.Street1)%>
            <%=Html.Hidden("address2", this.CurrentCart().ShippingAddress.Street2)%>
            <%=Html.Hidden("city", this.CurrentCart().ShippingAddress.City)%>
            <%=Html.Hidden("state", this.CurrentCart().ShippingAddress.StateOrProvince)%>
            <%=Html.Hidden("country", this.CurrentCart().ShippingAddress.Country)%>
            <%=Html.Hidden("zip", this.CurrentCart().ShippingAddress.Zip)%>

        <%} 
        int itemIndex = 1;
        foreach(var item in this.CurrentCart().Items){ %>

            <%=Html.Hidden("item_name_"+itemIndex, item.Product.Name)%>
            <%=Html.Hidden("amount_" + itemIndex, item.Product.Price.ToLocalCurrency())%>
            <%=Html.Hidden("item_number_" + itemIndex, item.Product.SKU)%>
            <%=Html.Hidden("quantity_" + itemIndex, item.Quantity.ToString())%>
            <%=Html.Hidden("shipping_" + itemIndex, (this.CurrentCart().ShippingAmount / this.CurrentCart().Items.Count()).ToLocalCurrency())%>
           
            <%itemIndex++;
        } %>
        <div class="checkout-button">
        <input type="image" src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" align="left" style="margin-right:7px;" />
        </div>
        </form>
        </div>
    </div>
</div>
</asp:Content>

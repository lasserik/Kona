<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Finalize</title>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="bdy">
    <div class="colmiddle780 cntr">
        <h4>Billing and Order Summary</h4><hr />
        <%this.RenderCreditCard(); %>
        
        <fieldset>
            <legend>Select Shipping</legend>
            <p>
        <%using (Html.BeginForm("Finalize","Order")) { %>
            <%foreach (ShippingMethod m in this.ShippingMethods().OrderBy(x=>x.Cost)) { %>
                
                <input onclick="this.form.submit();" type="radio" value="<%=m.ShippingMethodID %>" name="id" <%=Html.IsChecked(m.ShippingMethodID, this.CurrentCart().ShippingMethodID)%>/> <%=m.Display %><br />
        
            <%} %>
        <%} %>
            </p>
        </fieldset>
        
        <fieldset>
            <legend>Order Details</legend>
            <table style="width: 40em;">
                <thead>
                    <tr>
                        <th>
                            Shipping To:
                        </th>
                        <th>
                            Billing To:
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="width: 50%;">
                           <%this.RenderAddressDisplay(this.CurrentCart().ShippingAddress); %>
                        </td>
                        <td style="width: 50%;">
                            <%this.RenderAddressDisplay(this.CurrentCart().BillingAddress); %>
                        </td>
                    </tr>
                </tbody>
            </table>
            
            <table cellspacing="0" cellpadding="5" width="100%">
            <thead>
            <tr>
                <th><b>Quantity</b></th>
                <th><b>Item</b></th>
                <th style="text-align:right"><b>Regular</b></th>
                <th style="text-align:right"><b>Total</b></th>
            </tr>
            </thead>
            <%foreach(ShoppingCartItem item in this.CurrentCart().Items){%>
            <tr>
                <td ><%=item.Quantity %></td>
                <td ><%=item.Product.ProductName%></td>
                <td  align="right"><%=item.Product.BasePrice.ToString("C")%></td>
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
            
            
        </fieldset>
        

        
        

        <%using (Html.BeginForm("Payment","Order")) { %>
        <div style="text-align:right">
            <input type="submit" value="Place Order" />
        </div>
        <%} %>
  </div>

</asp:Content>

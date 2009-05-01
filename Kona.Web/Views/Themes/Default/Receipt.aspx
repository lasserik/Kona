<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage<Order>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Receipt for <%=Model.OrderNumber%> </title>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="bdy">
    <div class="colmiddle780 cntr">
        
        <fieldset>
            <legend>Thank You! Your Order Number: <%=Model.OrderNumber %></legend>
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
                           <%this.RenderAddressDisplay(Model.ShippingAddress); %>
                        </td>
                        <td style="width: 50%;">
                            <%this.RenderAddressDisplay(Model.BillingAddress); %>
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
            <%foreach(OrderLine item in Model.Items){%>
            <tr>
                <td ><%=item.Quantity %></td>
                <td ><%=item.Item.Name%></td>
                <td  align="right"><%=item.Item.Price.ToString("C")%></td>
                <td  align="right"><%=item.LineTotal.ToString("C") %></td>
            </tr>

            <%} %>
             <tr>
                <td colspan="4"><hr /></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Subtotal</td>
                <td align="right"><%= Model.SubTotal.ToString("C")%></td>
             </tr>
              <tr>
                <td colspan="3" align="right">Tax</td>
                <td align="right"><%= Model.TaxAmount.ToString("C")%></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Shipping (<%= Model.ShippingService%>)</td>
                <td align="right"><%= Model.ShippingAmount.ToString("C")%></td>
             </tr>
             <tr>
                <td colspan="3" align="right">Discount:</td>
                <td align="right">-<%= Model.DiscountAmount.ToString("C")%></td>
             </tr>
              <tr>
                <td colspan="3" align="right">Grand Total</td>
                <td align="right"><b><%= Model.Total.ToString("C")%></b></td>
             </tr>
        </table>
            
            
        </fieldset>
        </div>
<!--Clear out three column layout-->
<div class="clearLayout"></div>
<!--End bdy-->
</div>
</asp:Content>
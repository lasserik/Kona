<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Cart (<%=this.CurrentCart().Items.Count %> items)</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div id="bdy">
<div class="colmiddle780 cntr">
<h4>Your Cart</h4><hr />

    <%if (this.CurrentCart().Items.Count == 0) { %>
    <h4><a href="/catalog">There's nothing here! Go buy something...</a></h4>
    <%} else { %>

    <table width="100%">
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    Item
                </th>
                <th>
                    Price
                </th>
                <th>
                    Quantity
                </th>
                <th>
                    Remove
                </th>
                <th>
                    Total
                </th>
            </tr>
        </thead>
        <tbody>
        <%foreach (ShoppingCartItem item in this.CurrentCart().Items) { %>
            <tr>
                <td>
                    <img src="<%=Html.ProductImage(item.Product.DefaultImageFile) %>" alt="<%=item.Product.ProductName %>" width="80" height="80" />
                </td>
                <td>
                    <a href="<%=Url.Action("Show","Home",new{sku=item.Product.SKU}) %>"><%=item.Product.ProductName%></a><br />
                    Added on <%=item.DateAdded.ToString()%>
                </td>
                <td>
                    <%= item.Product.BasePrice.ToString("C") %>
                </td>
                <td>
                    <%using (Html.BeginForm("UpdateItem", "Cart")) {%>
                    <input type="hidden" name="id" value="<%=item.Product.SKU %>" />
                    <input type="text" name="Quantity" value="<%=item.Quantity.ToString() %>" size="2" length="2" onchange="this.form.submit();" />
                    <%} %>
                
                </td>
                <td>
                    <%using (Html.BeginForm("RemoveItem", "Cart")) {%>
                    <input type="hidden" name="id" value="<%=item.Product.SKU %>" />
                    <input type="image" src="<%=Html.ThemeImage("delete.gif")%>" />
                    <%} %>
                </td>
                <td>
                    <%= item.LineTotal.ToString("C") %>
                </td>
            </tr>
         <%} %>
            <tr>
                <td align="right" colspan="6">
                    <form action="<%=Url.Action("ApplyCoupon") %>" method="post">
                        Enter discount coupon
                        <input type="text" name="couponCode" />
                        <input type="submit" value="go" />
                    </form>                
                </td>
            </tr>
            <!--NOTE TO JON: Phony button style inside of a table cell - move to css file and/or find a different solution-->
            <tr>
                <td align="right" colspan="6">
                    <div style="font-weight: 500; width: 10em; padding: .4em; margin: 1em 0em 1em 0em;
                        color: #333; text-align: center; border-top: 1px solid #CCC; border-right: 1px solid #a8aeb6;
                        border-bottom: 1px solid #a8aeb6; border-left: 1px solid #CCC; background: #dcdfe2;
                        cursor: pointer;">
                       <a href="<%=Url.Action("Checkout","Order") %>">Checkout &gt;</a></div>
                </td>
            </tr>
        </tbody>
    </table>
    <%} %>
</div>

</div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: <%=this.Product().ProductName%></title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="bdy">
    <div class="colmiddle780 cntr">
        <div class="prodWidget">
        
        <h4><%=this.Product().ProductName%></h4>
        <hr />
        <div class="fltleft">
            <img src="<%=Html.ProductImage(this.Product().DefaultImageFile)%>" alt="<%=this.Product().ProductName %>"/>
        </div>
        <div class="fltleft">
            <%using (Html.BeginForm("AddItem", "Cart")) {%>
            <input type="hidden" value="<%=this.Product().SKU %>" name="sku" />
            <h6>Price:  <%=this.Product().BasePrice.ToString("C")%></h6>
            <input type="submit" value="Add Item To Cart">
            <%} %>
        </div>
        <div class="clearLayout">
        
        <%foreach (ProductDescriptor pd in this.Product().ProductDescriptors) {%>
           <h6><%=pd.Title %></h6>
           <p>
                <%=Plugin.Process<string>("ProcessHtml",pd.Body) %>
           </p>
        <%}%>
        </div>
    </div>
    </div>
</div>
</asp:Content>

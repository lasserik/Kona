<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: <%=this.Product().Name %></title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="bdy">
    <div class="colmiddle780 cntr">
        <div class="prodWidget">
        
        <h4><%=this.Product().Name %></h4>
        <hr />
        <div class="fltleft">
            <img src="<%=Html.ProductImage(this.Product().DefaultImage.ThumbnailPhoto)%>" alt="<%=this.Product().Name %>"/>
        </div>
        <div class="fltleft">
            <%using (Html.BeginForm("AddItem", "Cart")) {%>
            <input type="hidden" value="<%=this.Product().SKU %>" name="sku" />
            <h6>Price:  <%=this.Product().Price.ToString("C")%></h6>
            <input type="submit" value="Add Item To Cart">
            <%} %>
        </div>
        <div class="clearLayout">
        
        <%foreach (ProductDescriptor pd in this.Product().Descriptors) {%>
           <h6><%=pd.Title %></h6>
           <p>
                <%=Plugin.Process<string>("ProcessHtml",pd.Body) %>
           </p>
        <%}%>
        </div>
    </div>

        <%if (this.Product().Recommended.Count>0) { %>
        <div class="product-results">
        <h3>Others who bought <%=this.Product().Name%> also bought:</h3>
            <ul class="product-results">
                <%this.ProductListView(this.Product().Recommended); %>
            </ul>
        </div>
        <%}%>

        <%if(this.Product().CrossSells.Count>0){ %>
        <div id="related-products">
          <h3>You Might Also Like</h3>
          <ul>
          <%foreach (Product p in this.Product().CrossSells) { %>
              <li>
                  <a href="<%=Url.Action("show","home",new{sku=p.SKU})%>"> <img src="<%=Html.ProductImage(p.DefaultImage.ThumbnailPhoto)%>" alt="<%=p.Name %>"/></a>
                  <a href="<%=Url.Action("show","home",new{sku=p.SKU})%>"><%=p.Name %></a>
              </li>
          <%} %>
          </ul>
        </div>
       <%} %>
    </div>
</div>
</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>
 
 
 
            
            <%var product = Model.Products.Count > 0 ? Model.Products[0] : new Product("");%>
            
            <%if (Model.Products.Count > 0) { %>
            <div class="sidebarWidget">
                <h5><%=Model.Title %></h5>
                <div class="sidebarContent">
                    <a href="<%=Url.Action("Show","Home",new {sku=product.SKU}) %>">
                        <img style="height:80px" class="fltleft" src="<%=Html.ProductImage(product.DefaultImageFile) %>" /></a>
                        <p class="fltright">
                        <a href="<%=Url.Action("Show","Home",new {sku=product.SKU}) %>">
                        <%=Model.Body %> 
                        </a>
                        </p>
                    </div>
                <div class="clearLayout"></div>
            </div>
           
            <%} %>


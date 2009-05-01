<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<div class="prodWidget">
    <h4><%=Model.Title %></h4><hr/>
    <ul id="imgholder_<%=Model.WidgetID %>" class="prodWidget">
    
        <%foreach (Product p in Model.Products) { %>
        <div class="fltleft prodItem">
            <a href="<%=Url.Action("show","home",new{sku=p.SKU})%>" title="Go to <%= p.ProductName %> Details Page">
            <img src="<%=Html.ProductImage(p.DefaultImageFile) %>" alt="<%=p.ProductName %>"/>
            <p>
            <%=p.ProductName%><br />
            <%=p.BasePrice.ToString("C")%>
            </p>
            </a>
        </div>
        <%} %>
        <div class="clearLayout"></div>
    </ul>
</div>

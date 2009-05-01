<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<%
    if(Model.Products.Count>0){
        var Product = Model.Products[0]; %>
        <a href="<%=Url.Action("Show","Home",new{sku=Product.SKU}) %>">
             <img src="/content/images/<%=Model.Body%>" />
        </a>
    <%}else{ %>
     
     <img src="/content/images/<%=Model.Body == "" ? "placeholder.png" : Model.Body %>" />
    <%} %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget<IList<Product>>>" %>

    <script type="text/javascript">
        $('#sku_<%=Model.WidgetID%>').autocomplete("/page/skulist/", {
            mustMatch: 1
        });
        $('#imgholder_<%=Model.WidgetID%>').sortable({
            stop: function(event, ui) {
                processList();
            }
        });

        function addPic() {
            var sku = $('#sku_<%=Model.WidgetID %>').val();
            $.getJSON("/page/getproduct/?id=" + sku,
                function(data) {
                    var product = "<li id='" + sku + "'><img src='/content/productimages/" + data.Photo + "' /><br />";
                    product += "<a href='/home/show/?sku=" + sku + "' >" + data.ProductName + "</a>: <br />" + data.Price + "</li>";
                    $('#imgholder_<%=Model.WidgetID %>').append(product);
                    processList();
                }
            );
            
        }
        function processList() {
            var itemArray = [];
            $('#imgholder_<%=Model.WidgetID %> li').each(function(i, item) {
                itemArray[i] = item.id;
            })
            saveProductArray('<%=Model.WidgetID %>', itemArray);
        }
    </script>
<div id='<%=Model.WidgetID%>' class="nocontext">
    <div class="sorthandle" style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand">
        <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.WidgetID%>')"/>
    </div>
    <div >
        <div >
            <h4><input type="text" size=40 name="title_<%=Model.WidgetID %>" id="title_<%=Model.WidgetID %>" value="<%=Model.Title ?? "[Title goes here]" %>" style="border:1px dashed #cccccc; padding:5px" onblur="saveWidget('<%=Model.WidgetID %>',this.value,'')"/></h4>
            <span class=smalltext>select the sku of the product you wish to add to the list the click "go"</span><br />
            <input id="sku_<%=Model.WidgetID %>" name="sku_<%=Model.WidgetID %>" type="text" class="auto"/>
            <input type=button value="add" onclick="addPic()" /><br />
            
            <ul id="imgholder_<%=Model.WidgetID %>"  class="product-results" >
                <%foreach (Product p in Model.Products) { %>
                <li id="<%=p.SKU %>" >
                    <img src="/content/productimages/<%=p.DefaultImageFile%>" /><br />
                    <a href="<%=Url.Action("Show","Home",new{sku=p.SKU}) %>"><%=p.ProductName %></a>
                </li>
                <%} %>
            </ul>
        </div>    
        <div class="clearLayout"></div>
    </div>
</div>
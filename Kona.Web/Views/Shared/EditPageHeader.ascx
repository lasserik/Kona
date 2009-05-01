<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kona.Model.Page>" %>

<% if (this.IsPageInEditMode()) { %>
    <!-- Add scripting support for Edit Mode
    ---------------------------------------------------------->
    <script type="text/javascript" src="/scripts/jquery.contextMenu.js"></script>
    <script type="text/javascript" src="/scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
    <link rel=Stylesheet href="/scripts/jquery.autocomplete.css" />

    <script type="text/javascript">
       var loadedWidget="";

        // Called by the "Add" button in the product list widget
        // TODO: Move to a location specific to the product list widget
        function addSkuToProductList(widgetId, parentNodeId, skuInputId) {
            var parentNode = $("#" + parentNodeId);
            var sku = $("#" + skuInputId).val();
            $.getJSON("/Page/GetProduct",
                { sku: sku },
                function(data) {
                    var product = "<li id='" + sku + "'><img src='/content/productimages/" + data.Photo + "' /><br />";
                    product += "<a href='/home/show/?sku=" + sku + "' >" + data.ProductName + "</a>: <br />" + data.Price + "</li>";
                    parentNode.append(product);
                    processList(widgetId, parentNodeId);
                }
            );
        }
        
        function processList(widgetId, parentNodeId) {
            var itemArray = [];
            $("#" + parentNodeId + " li").each(function(i, item) {
                itemArray[i] = item.id;
            })
            saveProductArray(widgetId, itemArray);
        }

       function saveProductArray(widgetId, skus) {
            $.post("/Page/SaveProductWidget", {
                sku: skus,
                widgetid: widgetId
            });
        }

        function saveProduct(widgetId,sku) {
            var itemArray = [];
            itemArray[0] = sku;
            alert(sku);
            if(itemArray[0]!=""){
                $.post("/page/saveproductwidget", {
                    sku: itemArray,
                    widgetid: widgetId
                });
            }
        }

        $(document).ready(function() {
            if($('#title').val()=="Home"){
                $('#title').attr("disabled", true);
                $('#parentid').attr("disabled", true);
            }

            <% foreach(var widget in Model.WidgetEngine.Widgets){ %>
                loadWidget('<%=widget.ID%>','<%=widget.Zone%>');
            <%}%>
            
        });
   </script>
   
   <ul id="sidemenu" class="contextMenu">
        <li class="recent">
            <a href="#RecentlyViewed">Recently Viewed</a>
        </li>
        <li class="favorite">
            <a href="#Favorites">Favorites</a>
        </li>
        <li class="textonly">
            <a href="#TextOnly">Text</a>
        </li>
        <li class="ad">
            <a href="#ProductAd">Advertisement</a>
        </li>
        
    </ul>
    
    <ul id="centermenu" class="contextMenu">
        <li class="featured">
            <a href="#FeaturedProduct">Featured Product</a>
        </li>
        <li class="productlist">
            <a href="#ProductList">Product List</a>
        </li>
        <li class="textonly">
            <a href="#Html">HTML</a>
        </li>
    </ul>
<% } %>


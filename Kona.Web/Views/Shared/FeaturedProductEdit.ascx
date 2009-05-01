<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<% 
    string sku = Model.Products.Count > 0 ? Model.Products[0].SKU : "";
%>

<script type="text/javascript">
    $('#sku_<%=Model.WidgetID%>').autocomplete("/page/skulist/", {
        mustMatch: 1,
        minChars: 2,
        onItemSelect: function(li) {
            saveProduct();
        }
    }); 
    
    function saveProduct() {
        var itemArray = [];
        itemArray[0] = $('#sku_<%=Model.WidgetID %>').val();
        
        if(itemArray[0]!=""){
            $('#currentSku_<%=Model.WidgetID %>').html(itemArray[0]);
            $.post("/page/saveproductwidget", {
                sku: itemArray,
                widgetid: '<%=Model.WidgetID %>'
            });
        }
    }
</script>

<div id='<%=Model.ID%>' >
    
    <form action="<%=Url.Action("SaveProductImage","Page") %>" method="post" enctype="multipart/form-data">
        <input type="hidden" name="widgetid" value="<%=Model.WidgetID %>" />
        <input type="hidden" name="pageid" value="<%=Model.PageID %>" />

        <div>
             <p>Select SKU<input id="sku_<%=Model.WidgetID %>" name="sku" type="text" class="auto" style="font-size:8pt" />
             <b><div id="currentSku_<%=Model.WidgetID %>"><%=sku %></b></div>
             </p>
            
            <img src="/content/images/<%=Model.Body == "" ? "placeholder.png" : Model.Body %>" />
        </div>
	    <input type=file name="featuredproduct"  size=50 />
	    <input type="submit" value="go" />
    </form>
</div>
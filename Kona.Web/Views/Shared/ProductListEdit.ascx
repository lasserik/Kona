<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<span class="smalltext">Select the sku of the product you wish to add to the list then click "Add"</span><br />
<input id="sku_<%=Model.WidgetID %>" name="sku_<%=Model.WidgetID %>" type="text" class="auto"/>
<input type="button" value="Add" onclick="addSkuToProductList('<%=Model.WidgetID%>', 'imgholder_<%=Model.WidgetID%>', 'sku_<%=Model.WidgetID%>')" /><br />

<script type="text/javascript">
    $('#' + 'sku_<%=Model.WidgetID%>').autocomplete('/Page/SkuList', {
        mustMatch: 1
    });
</script>

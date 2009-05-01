<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<% 
    string currentSKU = Model.Products.Count > 0 ? Model.Products[0].SKU : "";
%>
<script type="text/javascript">

    $('#sku_<%=Model.WidgetID%>').autocomplete("/page/skulist/", {
        onItemSelect: function(li) {
            var itemArray = [];
            itemArray[0] = $('#sku_<%=Model.WidgetID %>').val();
            saveProductArray('<%=Model.WidgetID %>', itemArray);
            $('#currentSku_<%=Model.WidgetID %>').html(itemArray[0]);
        }
    });
</script>

<span class="smalltext">Select the product this ad is for</span>
<input id="sku_<%=Model.WidgetID %>" name="_<%=Model.WidgetID %>" type="text"  style="font-size:8pt"/>
<br />

<b><div id="currentSku_<%=Model.WidgetID %>"><%=currentSKU%></div></b>
<br/>

<span class="smalltext">Enter the text of the ad below</span>
<textarea rows=7 cols=18 style="size:10pt;border:1px dashed #990000" id="body_<%=Model.WidgetID %>" 
    onblur="saveWidgetBody('<%=Model.WidgetID %>', this.value);"><%=Model.Body ?? "" %></textarea>
<br />

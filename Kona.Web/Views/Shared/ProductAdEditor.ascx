<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget>" %>

<% 
    string sku = Model.Products.Count > 0 ? Model.Products[0].SKU : "";
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


 <div id='<%=Model.ID%>'>
        <div style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand" class="sorthandle">
            <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.WidgetID%>')"/>
        </div>
        <input type=text id="title_<%=Model.WidgetID %>" value="<%=Model.Title ?? "Title Goes Here" %>" style="border:1px dashed #990000; font-size:8pt;"   onblur="saveWidget('<%=Model.WidgetID %>',this.value,$('#body_<%=Model.WidgetID %>').val())"/><br />
        <span class="smalltext">
            select the product this ad is for
        </span>
        <input id="sku_<%=Model.WidgetID %>" name="_<%=Model.WidgetID %>" type="text"  style="font-size:8pt"/><br />
        <b><div id="currentSku_<%=Model.WidgetID %>"><%=sku %></div></b><br />
        <span class="smalltext">
            enter the text of the ad below
        </span>
        <textarea rows=7 cols=18 style="size:10pt;border:1px dashed #990000" id="body_<%=Model.WidgetID %>" onblur="saveWidget('<%=Model.WidgetID %>',$('#title_<%=Model.WidgetID %>').val(),this.value);"><%=Model.Body ?? "" %></textarea><br />
 </div>
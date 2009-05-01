<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Widget<IList<Product>>>" %>

<% 
string sku=Model.Data.Count>0 ? Model.Data[0].SKU : "";
%>
<script type="text/javascript">

    $('#sku_<%=Model.ID%>').autocomplete("/page/skulist/", {
        onItemSelect: function(li) {
            var itemArray = [];
            itemArray[0] = $('#sku_<%=Model.ID %>').val();
            saveProductArray('<%=Model.ID %>', itemArray);
            $('#currentSku_<%=Model.ID %>').html(itemArray[0]);
        }
    });
        

</script>


 <div id='<%=Model.ID%>'>
        <div style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand" class="sorthandle">
            <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.ID%>')"/>
        </div>
        <input type=text id="title_<%=Model.ID %>" value="<%=Model.Title ?? "Title Goes Here" %>" style="border:1px dashed #990000; font-size:8pt;"   onblur="saveWidget('<%=Model.ID %>',this.value,$('#body_<%=Model.ID %>').val())"/><br />
        <span class="smalltext">
            select the product this ad is for
        </span>
        <input id="sku_<%=Model.ID %>" name="_<%=Model.ID %>" type="text"  style="font-size:8pt"/><br />
        <b><div id="currentSku_<%=Model.ID %>"><%=sku %></div></b><br />
        <span class="smalltext">
            enter the text of the ad below
        </span>
        <textarea rows=7 cols=18 style="size:10pt;border:1px dashed #990000" id="body_<%=Model.ID %>" onblur="saveWidget('<%=Model.ID %>',$('#title_<%=Model.ID %>').val(),this.value);"><%=Model.Body ?? "" %></textarea><br />
 </div>
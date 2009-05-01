<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>
<div id='<%=Model.ID%>' class="nocontext">
    <div  class="sorthandle" style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand">
        <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.ID%>')"/>
    </div>   
    <div >
        <textarea name="body_<%=Model.ID %>" id="body_<%=Model.ID %>" style="width:98%;height:300px" onblur="saveWidget('<%=Model.ID %>','',this.value)"><%=Model.Body %></textarea>
    </div>
</div>
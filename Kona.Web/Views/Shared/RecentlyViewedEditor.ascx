<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IWidget>" %>

<div id='<%=Model.ID%>' class="sorthandle">
    <div style="height:15px; background-color:#f5f5f5;text-align:right;padding:2px;cursor:hand">
        <img src="/content/icons/cancel.png" onclick="removeWidget('<%=Model.ID%>')"/>
    </div>
    <div class="leftnavMod">
        <h6>Recently Viewed</h6>
        <ul class="leftnavList">
            <li><a href="#">Donec lacus</a></li>
            <li><a href="#">Aenean id nunc</a></li>
            <li><a href="#">Fusce non nibh</a></li>
            <li><a href="#">Ut aliquet dictum</a></li>
            <li><a href="#">Etiam eu est</a></li>
            <li><a href="#">Aenean hendrerit</a></li>
            <li><a href="#">Maecenas dapibus</a></li>
        </ul>
    </div>
</div>
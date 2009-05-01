<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kona.Infrastructure.Page>" %>
<style type="text/css">

 /* Isolation CSS
----------------------------------------------------------*/
.LE html, .LE body, .LE div, .LE span, .LE applet, .LE object, .LE iframe, .LE table, .LE caption, .LE tbody, .LE tfoot, .LE thead, .LE tr, .LE th, .LE td, .LE del, .LE dfn, .LE em, .LE font, .LE img, .LE ins, .LE input, .LE select, .LE textarea, .LE kbd, .LE q,.LE s, .LE samp, .LE small, .LE strike, .LE strong, .LE sub, .LE sup, .LE tt, .LE var, .LE h1, .LE h2, .LE h3, .LE h4, .LE h5, .LE h6,.LE p, .LE blockquote, .LE pre, .LE a, .LE abbr, .LE acronym, .LE address, .LE big, .LE cite, .LE code, .LE dl, .LE dt, .LE dd,.LE ol, .LE ul, .LE li, .LE fieldset, .LE form, .LE label, .LE legend, .LE :focus, .LE :visited {
            list-style: disc;
            background: white;
            color: black;
            direction: ltr;
            font-style: normal;
            font-family: Times;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            font-size: 12pt;
            letter-spacing: normal;
            line-height: normal;
            text-align: left;
            text-decoration: none;
            text-indent: 0px;
            text-transform: none;
            unicode-bidi: normal;
            vertical-align: baseline;
            white-space: normal;
            word-spacing: normal;
            padding: 0px;
            margin: 0px;
            border: 0px;
            outline: 0px;
            box-sizing: content-box;
            clear: none;
            empty-cells: hide;
            float: none;
            table-layout: auto;
            clip: auto;
            display: inline;
            height: auto;
            width: auto;
            left: auto;
            top: auto;
            right: auto;
            bottom: auto;
            overflow: auto;
            position: static;
            visibility: visible;
            z-index: auto;
            cursor: auto;
            outline: invert none medium;
        }
        .LE :before, .LE :after 
        {
        	content: "";
        }
        .LE a 
        {
        	text-decoration: underline;
        	color: Blue;
        }
.LE address, .LE blockquote, .LE body, .LE center, .LE dd, .LE dir, .LE div, .LE dl, .LE dt, .LE fieldset, .LE form, .LE hn, .LE hr,.LE iframe, .LE legend, .LE listing, .LE menu, .LE ol, .LE p, .LE pre, .LE ul 
        {
        	display: block;
        }
        .LE frame 
        {
        	display: none;
        }
        .LE li 
        {
        	display: list-item;
        }
        .LE table 
        {
        	display: table;
        }
        .LE tbody 
        {
        	display: table-row-group;
        }
        .LE tr 
        {
        	display: table-row;
        }
        .LE td, .LE th 
        {
        	display: table-cell;
        }
        .LE thead 
        {
        	display: table-header-group;
        }
        .LE tfoot 
        {
        	display: table-footer-group;
        }
        .LE colgroup 
        {
        	display: table-column-group;
        }
        .LE col 
        {
        	display: table-column;
        }

/*toolbar text attributes  form id="pageEditor" class="LE"
----------------------------------------------------------*/

ul.toolBarLeft.LE li a, ul.toolBarRight.LE li a {font-family:"Segoe UI","trebuchet ms",Verdana,Arial,Helvetica,sans-serif; font-weight:normal; font-style: normal; font-size:14px; text-decoration:none; color:#f1f1f1;}

ul.tab.LE li a {font-family:"Segoe UI","trebuchet ms",Verdana,Arial,Helvetica,sans-serif; font-weight:normal; font-style: normal; font-size:12px; text-decoration:none; color:#f1f1f1;}

/*From text attributes
----------------------------------------------------------*/

#Editor.LE label, #Editor.LE input, #Editor.LE input.textEntry, #Editor.LE select {font-family:"Segoe UI","trebuchet ms",Verdana,Arial,Helvetica,sans-serif; font-style: normal; font-size:14px; text-decoration:none; color:#333;}

#Editor.LE label {font-weight:600;}

#Editor.LE .error {color:Red; display:none;}

/*From layout attributes
----------------------------------------------------------*/

#Editor.LE, #Editor.LE div {background:#ebebeb; padding:6px 0px 6px 20px;}

#Editor.LE label {display:block; padding:6px 0px 0px 0px; background-color:Transparent;}

#Editor.LE input.textEntry {border:1px solid #828790; margin:0px 8px 2px 0px; padding:2px;}

#Editor.LE select {border:1px solid #828790;}


/* toolbar bottom layout attributes
----------------------------------------------------------*/

#editBar.LE {position:fixed; bottom: 0px; width:100%; min-width:900px; padding:4px; background:#333 url('/Views/Shared/images/liveToolbar/toolBarBackground.gif') repeat-x left top; z-index:1001; left: 0px;}

ul.toolBarLeft.LE, ul.toolBarRight.LE {list-style:none; float:left; float:left;overflow:hidden; background-color:Transparent;}

ul.toolBarRight.LE {float:right; padding-right:16px;}

/* Toolbar li styles */
ul.toolBarLeft.LE li, ul.toolBarRight.LE li {float:left; padding:2px; background-color:Transparent;}
ul.toolBarLeft.LE li a, ul.toolBarRight.LE li a {vertical-align:top;text-align:center;font-family:"Segoe UI","trebuchet ms",Verdana,Arial,Helvetica,sans-serif; font-weight:normal; font-style: normal; font-size:14px; text-decoration:none; color:#f1f1f1; display:block; padding:2px 8px; margin: 0px; float:left; border:1px #f1f1f1 solid; background-color:Transparent;}
ul.toolBarLeft.LE li input, ul.toolBarRight.LE li input {height:25px;vertical-align:top;text-align:center;font-family:"Segoe UI","trebuchet ms",Verdana,Arial,Helvetica,sans-serif; font-weight:normal; font-style: normal; font-size:14px; text-decoration:none; color:#f1f1f1; display:block; padding:2px 8px; margin: 0px; float:left; border:1px #f1f1f1 solid; background-color:Transparent;}
ul.toolBarLeft.LE li a:hover, ul.toolBarRight.LE li a:hover, ul.toolBarLeft.LE li input:hover, ul.toolBarRight.LE li input:hover {color:#fdcc64; border:1px #fdcc64 solid; cursor:pointer;}

/* Admin link classes */
ul.toolBarRight.LE li.admin a {display:block; padding:3px 12px 0px 12px; border-style:none; float:left; background-color:Transparent;} 
ul.toolBarRight.LE li.admin a:hover {border-style:none;}

/* Disabled tab classes */
ul.toolBarLeft.LE li .disabled, ul.toolBarRight.LE li .disabled {color:#6c6c6c; border:1px #6c6c6c solid;} 
ul.toolBarLeft.LE li .disabled:hover, ul.toolBarRight.LE li .disabled:hover {color:#6c6c6c; border:1px #6c6c6c solid; cursor:default;}

/* Active tab class */
ul.toolBarLeft.LE li .checked, ul.toolBarRight.LE li .checked {color:#ff9933; border:1px #ff9933 solid;}

/* tabBar
----------------------------------------------------------*/

#tabBar.LE {position:fixed; bottom:36px; width:100%; z-index:1000; overflow:hidden;}

ul.tab.LE {list-style:none; margin:0px 0px -2px 0px; padding:4px 4px 0px 4px; background:url(/Views/Shared/images/liveToolbar/tabBackground.gif) no-repeat top right; width:180px; overflow:hidden;}

/* Tab default state */
ul.tab.LE li.Off {float:left; background:url(/Views/Shared/images/liveToolbar/tabRight2.gif) no-repeat top right; overflow:hidden; cursor:pointer; list-style:none;}
ul.tab.LE li.Off a {display:block; float:left; padding:4px 16px 4px 16px; text-decoration:none; text-align:center; color:#f1f1f1; background:url(/Views/Shared/images/liveToolbar/tabLeft2.gif) no-repeat top left; overflow:hidden; cursor:pointer;}

/* Tab hover state */
ul.tab.LE li a:hover {color:#fdcc64; text-decoration:underline; cursor:pointer;}

/* Tab selected state */
ul.tab.LE li.On {float:left; background:url(/Views/Shared/images/liveToolbar/tabRight2On.gif) no-repeat top right; overflow:hidden; color:#353232; cursor:pointer; list-style:none;}
ul.tab.LE li.On a {display:block; padding:4px 16px 4px 16px; text-decoration:none; font-weight:600; text-align:center; color:#f1f1f1; background:url(/Views/Shared/images/liveToolbar/tabLeft2On.gif) no-repeat top left; float:left; overflow:hidden; color:#000; cursor:pointer;}

ul.tab.LE li.closeUp {background:url(/Views/Shared/images/liveToolbar/closeGlyphUp.gif) no-repeat bottom #1d1a1a; height:11px; cursor:pointer; overflow:hidden; margin:0px;}

ul.tab.LE li.closeDown {background:url(/Views/Shared/images/liveToolbar/closeGlyphDown.gif) no-repeat bottom #1d1a1a; height:11px; overflow:hidden; cursor:pointer;}

#newTabContent.LE {border:2px #2e3031 solid; margin:0px; padding-top:3px; background:#767676; overflow:hidden;display:none;}

#widgetTabContent.LE {border:2px #2e3031 solid; margin:0px; padding-top:3px; background:#767676; overflow:hidden;display:none;}

#editTabContent.LE {border:2px #2e3031 solid; margin:0px; padding-top:3px; background:#767676; overflow:hidden;display:none;}

</style>

<!-- Empty dummy form containing the pageid field used by AJAX calls -->
<form action="">
    <input type="hidden" name="id" id="pageid" value="<%= Model.PageID %>" />
</form>

<script type="text/javascript">
    $(document).ready(function() {
        $(".closeUp").click(function() {
            $("#newTabContent").hide();
            $("#widgetTabContent").hide();
            $("#editTabContent").slideToggle('slow');
            $(".closeUp").toggleClass("closeDown");
            return false;
        });
    });
    var operation;
    function show_edit() {
        $(".closeUp").click();
        operation = 'edit';
    }
    function show_create() {
        $(".closeDown").click();
        $("#widgetTabContent").hide();
        var e = document.getElementById('newTabContent');
        if (e.style.display == 'block')
            e.style.display = 'none';
        else
            e.style.display = 'block';
        operation = 'create';
    }
    function show_widget() {
        $(".closeDown").click();
        $("#newTabContent").hide();
        var e = document.getElementById('widgetTabContent');
        if (e.style.display == 'block')
            e.style.display = 'none';
        else
            e.style.display = 'block';
        operation = 'widget';
    }
    function submit_form() {
        var form = document.forms[operation],
            jsonForm = {},
            elts = form.elements;

        for (var i = 0; i < elts.length; i++) {
            var field = form.elements[i];
            jsonForm[field.name] = field.value;
        }
        $.ajax({
            type: "POST",
            url: form.action,
            dataType: "json",
            data: jsonForm,
            success: function(data) {
                if (data.action == "redirect") {
                    document.location.href = data.url;
                }
            }
        });
    }
</script>

<div id="tabBar" class="LE">
<ul class="tab LE">
<li class="On"><a href="javascript:show_edit()">Page</a></li>
<li class="Off"><a href="javascript:show_widget()">Widgets</a></li>
<li class="closeUp LE"><a href=""></a></li>
</ul>
<div id="newTabContent" class="LE"> 
<%Html.RenderPartial("NewPage"); %>
</div>
<div id="editTabContent" class="LE">
<%Html.RenderPartial("PageEditor"); %>
</div>
<div id="widgetTabContent" class="LE" style="overflow:auto;"> 
<%Html.RenderPartial("WidgetEditor");%>
</div>
</div>

<div id="editBar" class="LE">
<ul class="toolBarLeft LE">
<li><a href="javascript:show_create()">New Page</a></li>
<%if (Model != null) { %> 
<li><a href="<%=Url.Action("Edit","Page", new {id=Model.PageID}) %>" <% if (Model.Status != PublishStatus.Published) { %> class="disabled" <% } %>>Edit Page</a></li>
<li><a href="<%=Url.Action("Delete","Page", new {id=Model.PageID}) %>">Delete Page</a></li>
<% } %>
</ul>
<ul class="toolBarRight LE">
<li><input type="button" value="Save" onclick="javascript:submit_form()" /></li>
<% if (Model != null) { %> 
<li><a href="<%=Url.Action("Publish", "Page", new {id=Model.PageID}) %>" <% if (Model.Status == PublishStatus.Published) { %> class="disabled" <% } %>>Publish</a></li>
<li><a href="<%=Url.Action("Revert", "Page", new {id=Model.PageID}) %>" <% if (Model.Status == PublishStatus.Published) { %> class="disabled" <% } %>>Revert</a></li>
<% } %>
<li class="admin"><a href="<%=Url.Action("Index","Site") %>">Admin &#155</a></li>
</ul>
</div>
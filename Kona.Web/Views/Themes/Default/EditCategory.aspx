<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage<Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript" src="/scripts/jquery.contextMenu.js"></script>
    <script type="text/javascript" src="/scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="/scripts/tiny_mce/tiny_mce.js"></script>
    <link rel=Stylesheet href="/scripts/jquery.autocomplete.css" />

<div id="bdy">

    <h4>Page Editor: <%=Model.LocalizedName %></h4>

    <fieldset>
    <legend>Title</legend>
        <input type="text" name="name" id="name" value="<%=Model.LocalizedName %>" size="40" onblur="postIt();"/>        
        <span style="padding:5px; font-family:arial; font-size:14pt;font-weight:bold">Parent:</span>
        <select name="parentid" id="parentid">
            <option value="0">-- none --</option>
           <%//foreach (Kona.Data.Page pg in this.SitePages()) { %>
            <option value="<%//=pg.PageID %>"><%//=pg.Title %></option>
            <%//} %>
        </select>
        <div class="smalltext">This is the title that will appear at the top, and is optional</div>
    </fieldset>
      
      
      <table width=100%>
        <tr>
            <td valign=top style="width:200px">
             <div id="sidebar1" class="sidelistcontext" style="border:2px dashed #f5f5f5;">
                <span class="smalltext">Right-click to add content</span> 
                <ul id="sidebar1list" class="editorlist">
                
                </ul>    
             </div> 
            
            </td>
            <td valign=top >
                <div id=center class="centercontext" style="border:2px dashed #f5f5f5;">
                    <span class="smalltext">Right-click to add content</span> 
                    <ul id="centerlist" class="editorlist">
                    
                    
                    </ul>   
                </div>
            
            </td>
            <td valign=top style="width:200px">
               <fieldset>
                <legend>Admin</legend>
                <form action="<%=Url.Action("SetStatus","Page") %>" method="post">
                    <input type="hidden" name="categoryid" id="categoryid" value="<%=Model.CategoryID %>" />

                <%//if (Model.Status == PublishStatus.Draft) { %>
                    <p><input type=submit value="Publish" name="status"/></p>
                    <p><input type=button value="Preview"onclick="location.href='<%=Url.Action("Preview","Home",new {id=Model.CategoryID}) %>'"/></p>
                <%//} else if(Model.Status== PublishStatus.Published) { %>
                    <p><input type=submit value="Take Offline" name="status" /></p>
                <%//}else{ %>
                    <p><input type=submit value="Publish" name="status"/></p>
                    
                <%//} %>
                <p><input type=submit value="Delete" name="status"/></p>
                </form>
               </fieldset>
               <br />
                <div  id="sidebar2" style="border:2px dashed #f5f5f5" class="sidelistcontext">
                    <span class="smalltext">Right-click to add content</span>   
                    <ul id="sidebar2list" class="editorlist">
                    
                    </ul>
                    <div >
                    </div>
                </div>          
            </td>
        </tr>
     
     </table>
</div>
   <script type="text/javascript">
       function postIt(){

          //save the title, parent
          $.post("/home/edit/",{
            Name: $('#name').val(),
            PageID: $('#pageid').val(),
            ParentID: $('#parentid').val()
          });
       }
       var loadedWidget="";
       function removeWidget(widgetid){
           $.post('/widget/delete', {
               id:widgetid
           });       
           $('#'+widgetid).remove();
       }
       function createWidget(viewName,zone) {
           $.post('/widget/create', {
               ViewName: viewName,
               CategoryID: $('#categoryid').val(),
               Zone: zone
           },
               function(data) {
                   arrangeWidgets(zone);
                   loadWidget(data.ID,data.Zone);
               }, "json"
           );
       }
       
       function saveWidget(id,title,body) {
            $.post('/widget/edit/', {
                ID: id,
                Body: body,
                Title: title
            });
        }
       
      function saveProductArray(id,skus) {

            $.post("/widget/edit", {
                SKUList: skus.join(";"),
                id: id
            });
        }
       function saveProduct(id,sku) {
            var itemArray = [];
            itemArray[0] = sku;
            alert(sku);
            if(itemArray[0]!=""){
                $.post("/widget/edit", {
                    sku: itemArray,
                    id: id
                });
            }
        }
        function loadWidget(widgetid, zone) {
            $.get("/widget/edit/?id="+widgetid,
                function(data) {
                    var html="<li id="+widgetid+" class='widgetframe'>"+data+"</li>";
                    $('#' + zone+"list").append(html);
                }
            );
          
        }

        function arrangeWidgets(zone) {
            var itemArray = new Array();
            $('#'+zone+'list li.widgetframe').each(function(i, item) {
                itemArray[i] = item.id;
            })
            $.post("/widget/SaveWidgetOrder", {
                widgetid:itemArray
            });
        }


        $(document).ready(function() {
            if($('#title').val()=="Home"){
                $('#title').attr("disabled", true);
                $('#parentid').attr("disabled", true);
           }
        
            $('#centerlist').sortable({
                 handle:'.sorthandle',
               stop: function(event, ui) {
                    arrangeWidgets('center');
                }
            });

            $('#sidebar1list').sortable({
                  handle:'.sorthandle',
                stop: function(event, ui) {
                    arrangeWidgets('sidebar1');
                }
            });
            
            $('#sidebar2list').sortable({
                handle:'.sorthandle',
                stop: function(event, ui) {
                    arrangeWidgets('sidebar2');
                }
            });


           <%
            //load em if ya got em
            foreach(var widget in Model.Widgets){
            %>
                loadWidget('<%=widget.WidgetID%>','<%=widget.Zone%>');
            <%}%>
            
            $(".sidelistcontext").contextMenu({
                menu: 'sidemenu'
            },

            function(action, el, pos) {
                zone = $(el).attr('id');
                createWidget(action, zone);
            });

            $(".centercontext").contextMenu({
                menu: 'centermenu'
            },
            function(action, el, pos) {
                zone = $(el).attr('id');
                createWidget(action, zone);
            });

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

</asp:Content>



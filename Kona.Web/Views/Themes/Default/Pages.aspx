<%@ Page ValidateRequest="false" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage<IList<Kona.Infrastructure.Page>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div id="bdy">
    <div class="colmiddle780 cntr">
   
        <h5>Current Drafts</h5><hr />
    
         <%foreach (Kona.Infrastructure.Page pg in Model.Where(x=>x.Status==PublishStatus.Draft)) { %>
           <fieldset>
                <legend><a href="<%=Url.Action("Edit","Page", new {id=pg.PageID}) %>"><%=pg.Title %></a></legend>
                <i><b>Created by</b> <%=pg.CreatedBy %> on <%=pg.CreatedOn.ToLongDateString() %></i><br />
                <i><b>Last Modified By</b> <%=pg.ModifiedBy %> on <%=pg.ModifiedOn.ToLongDateString() %></i>
           </fieldset>
        <%} %>
    
  
         <h5>Published</h5><hr />
    
         <%foreach (Kona.Infrastructure.Page pg in Model.Where(x=>x.Status==PublishStatus.Published)) { %>
           <fieldset>
                <legend><a href="<%=Url.Action("Edit","Page", new {id=pg.PageID}) %>"><%=pg.Title %></a></legend>
                <i><b>Created by</b> <%=pg.CreatedBy %> on <%=pg.CreatedOn.ToLongDateString() %></i><br />
                <i><b>Last Modified By</b> <%=pg.ModifiedBy %> on <%=pg.ModifiedOn.ToLongDateString() %></i>
           </fieldset>
        <%} %>
  
  
          <h5>Offline</h5><hr />
    
         <%foreach (Kona.Infrastructure.Page pg in Model.Where(x=>x.Status==PublishStatus.Offline)) { %>
           <fieldset>
                <legend><a href="<%=Url.Action("Edit","Page", new {id=pg.PageID}) %>"><%=pg.Title %></a></legend>
                <i><b>Created by</b> <%=pg.CreatedBy %> on <%=pg.CreatedOn.ToLongDateString() %></i><br />
                <i><b>Last Modified By</b> <%=pg.ModifiedBy %> on <%=pg.ModifiedOn.ToLongDateString() %></i>
           </fieldset>
        <%} %>
  </div>
</div>
</asp:Content>


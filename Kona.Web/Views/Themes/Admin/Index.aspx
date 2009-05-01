<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage<Kona.Web.KonaSite>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<fieldset>
    <legend>Happening Now</legend>
    <ul>
        <li><strong>There are <a href=#>24 Orders</a> that need to be captured and processed</b></strong>
        <li>There are <a href=#>81 shoppers online</a></li>
        <li>There are <a href=#>2 Requests</a> for customer service</li>
        <li>There are <a href=#>12 Orders</a> ready for pickup by shipping</li>
        <li>There is an update to the software -  <a href=#>click here</a> to update to version 2.1</li>
    </ul>
    
    
    
</fieldset>
<fieldset>

    <table width=100%>
    
        <tr>
            <td>
                <h2>Sales by Category</h2>
                <img src="/Views/Themes/Admin/charting/pies.png" />
            </td>
            <td>
                <h2>Sales by Region</h2>
                <img src="/Views/Themes/Admin/charting/pie2.png" />
            </td>
        </tr>
        <tr>
            <td>
                <h2>Product Performance</h2>
                <img src="/Views/Themes/Admin/charting/lines.png" />
            </td>
            <td>
                <h2>Campaign Effectiveness</h2>
                <img src="/Views/Themes/Admin/charting/bars.png" />
            </td>
        </tr>
   </table>


    
    
    
</fieldset>

</asp:Content>



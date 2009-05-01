<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script src="/scripts/StateDropDown.js" type="text/javascript"></script>
    <%
        Address thisAddress = this.CurrentCustomer().DefaultAddress ?? new Address();
        
    %>
<fieldset class="address-form">
  <%=Html.Hidden("UserName",this.CurrentCustomer().UserName) %>
  <legend>Address</legend>
  <p class="formEntry">
      <label>First/Last</label>
      <%=Html.TextBox("FirstName", thisAddress.FirstName ?? "")%>
      <%=Html.TextBox("LastName", thisAddress.LastName ?? "")%>
  </p>
  <p class="formEntry">
      <label>Email</label>
      <%=Html.TextBox("Email", thisAddress.Email ?? "")%>
  </p>
  <p class="formEntry">
      <label>Address</label>
      <%=Html.TextBox("Street1", thisAddress.Street1 ?? "")%>
  </p>
  <p class="formEntry">
      <label>Address 2</label>
      <%=Html.TextBox("Street2", thisAddress.Street2 ?? "")%>
  </p>
  <p class="formEntry">
      <label>City</label>
      <%=Html.TextBox("City", thisAddress.City ?? "")%>
  </p>
  <p class="formEntry">
      <label>Country and State</label>
      <script type="text/javascript">
          var postState = '<%=thisAddress.StateOrProvince%>';
          var postCountry = '<%=thisAddress.Country%>';
      </script>
      <select id='countrySelect' name='country' onchange='populateState()'>
      </select>
      <select id='stateSelect' name='stateorprovince'>
      </select>
      <script type="text/javascript">initCountry('US'); </script>
  </p>
  <p class="formEntry">
      <label>Postal Code</label>
      <%=Html.TextBox("Zip", thisAddress.Zip ?? "")%>
  </p>
</fieldset>

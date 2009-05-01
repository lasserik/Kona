<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

      <fieldset>
        <legend>Credit Card</legend>
        <p class="formEntry">
            <label>Credit Card Type</label>
            <select name="CardType">
                <option value="Visa" selected="selected">Visa</option>
                <option value="MasterCard">MasterCard</option>
                <option value="Amex">Amex</option>
            </select>
          </p>
          <p class="formEntry">
            <label>Credit Card Number</label>
            <%=Html.TextBox("AccountNumber", "4586 9748 7358 4049", new { size = 40, maxlength = 40 })%>
          </p>
          <p class="formEntry">
            <label>Security Code</label>
            <%=Html.TextBox("VerificationCode", "000", new { size = 3, maxlength = 3 })%>
          </p>
          <p class="formEntry">
            <label>Expiration</label>
            <select name="ExpirationMonth">
                <%for (int i = 1; i <= 12; i++) { %>
                <option value="<%=i%>"><%=i%></option>
                <%} %>
            </select>
            <select name="ExpirationYear">
                <%for (int i = 0; i <= 6; i++) { %>
                <option value="<%=DateTime.Now.Year+i%>"><%=DateTime.Now.Year + i%></option>
                <%} %>
            </select>
          </p>
        </fieldset>

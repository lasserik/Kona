<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Configuration</h2>

    <form action="<%=Url.Action("Update") %>" method="post">
    		<h1 class="title">Admin <span>Site Editor</span></h1>
			<div class="content">
				<form id="form1" method="post" action="<%=Url.Action("Update") %>">
     
				
				    <div class="editrow">
				        <label for="SiteName">Site <span>Name:</span></label><br />
				        <input id="SiteName" type="text" name="SiteName" value="<%=this.SiteData().SiteName %>" />
					</div>       
				
				    <div class="editrow">
				        <label for="Description">Description:</label><br />
				        <input id="Description" type="text" name="Description" value="<%=this.SiteData().Description %>" />
					</div>          
				
					<div class="editrow">
				        <label for="OwnerName">Owner <span>Name:</span></label><br />
				        <input id="OwnerName" type="text" name="OwnerName" value="<%=this.SiteData().OwnerName %>" />
					</div>       
				
				    <div class="editrow">
				        <label for="AdminEmail">Admin <span>Email</span></label><br />
				        <input id="AdminEmail" type="text" name="AdminEmail" value="<%=this.SiteData().AdminEmail %>" />
					</div>   
				    <div class="editrow">
				        <label for="CurrencyCode">Currency <span>Code</span></label><br />
				        <input id="Text1" type="text" name="CurrencyCode" value="<%=this.SiteData().CurrencyCode %>" size=3 maxlength=3/>
					</div>
				
				
				    <div class="editrow">
				        <label for="AcceptCreditCards">Accept <span>Credit Cards</span></label><br />
				        <input id="AcceptCreditCards" type="checkbox" value="true" name="AcceptCreditCards" <%=Html.IsChecked(this.SiteData().AcceptCreditCards) %> />
					</div>       
				
				
				    <div class="editrow">
				        <label for="CreditCardProcessor">Credit Card <span>Processor</span></label><br />
				        <select name="CreditCardProcessor">
				            <option><%=this.SiteData().CreditCardProcessor %></option>
				        </select>
				        
					</div>       
				    <div class="editrow">
				        <label for="TaxCalculator">Tax Calculator</label><br />
				        <select name="TaxCalculator">
				            <option>USTaxCalculator</option>
				        </select>
				        
					</div> 
				    <div class="editrow">
				        <label for="TaxCalculator">Shiping Methods</label><br />
				        <select name="TaxCalculator">
				            <option>simpleShippingCalculator</option>
				        </select>
				        
					</div> 				
				    <fieldset>
				    <legend>PayPal</legend>
				    <div class="editrow">
				        <label for="AcceptPayPal">Accept <span>PayPal</span></label><br />
				        <input id="AcceptPayPal" type="checkbox" value="true" name="AcceptPayPal" <%=Html.IsChecked(this.SiteData().AcceptPayPal) %> />
					</div>       
				
				
				    <div class="editrow">
				        <label for="PayPalBusinessEmail">PayPal Business <span>Email</span></label><br />
				        <input id="Text2" type="text" name="PayPalBusinessEmail" value="<%=this.SiteData().PayPalBusinessEmail %>" size=30/>
					</div>       
				
				
				    <div class="editrow">
				        <label for="PayPalPDTToken">PayPal <span>PDT Token</span></label><br />
				        <input id="Text3" type="text" name="PayPalPDTToken" value="<%=this.SiteData().PayPalPDTToken %>" size=60/>
					</div>  
				    </fieldset>
				
				
				
			    <fieldset>
				    <legend>Open ID</legend>
				    <div class="editrow">
				        <label for="RPXAPIKey">RPX <span>API Key</span> :</label><br />
				        <input id="RPXAPIKey" type="text" name="RPXAPIKey" value="<%=this.SiteData().RPXAPIKey %>"  size=60/>
					</div>  
				    <div class="editrow">
				        <label for="RPXNowUrl">RPX <span>Url</span></label><br />
				        <input id="RPXNowUrl" type="text" name="RPXNowUrl" value="<%=this.SiteData().RPXNowUrl %>"  size=60/>
					</div> 				    
				</fieldset>
			
				


                <fieldset>
                <legend>Mail Server</legend>
				    <div class="editrow">
				        <label for="SMTPServer">SMTP <span>Server</span></label><br />
				        <input id="SMTPServer" type="text" name="SMTPServer" value="<%=this.SiteData().SMTPServer %>" />
					</div>       
				
				
				    <div class="editrow">
				        <label for="SMTPLogin">SMTP <span>Login</span></label><br />
				        <input id="SMTPLogin" type="text" name="SMTPLogin" value="<%=this.SiteData().SMTPLogin %>" />
					</div>       
				                
  				    <div class="editrow">
				        <label for="SMTPPassword">SMTP <span>Password</span></label><br />
				        <input id="SMTPPassword" type="text" name="SMTPPassword" value="<%=this.SiteData().SMTPPassword %>" />
					</div>       
                 </fieldset>
				
    
			
					
					<div class="editrow">
					    <input type="submit" name="cmdSave" value="Save" />
					</div>        
				</form>
			</div>

        
    
    </form>
</asp:Content>


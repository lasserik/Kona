<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Checkout</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


<div id="bdy">
    <div class="colmiddle780 cntr">
        <h4>An account is not required to check out</h4><hr />
        <p>If you are a new customer and would like to create an AdventureWorks account, <a href=#>Start here.</a> If you would like to check out as a guest, click the Continue button to complete your purchase.</p>
 
        <p>With an <%=this.SiteData().SiteName%> account you can:</p>
            <ul class="bulletList">
            <li>Save time - checking out is faster.</li> 
            <li>Access your order history.</li>
            </ul>

        <input type="button" name="anon" value="Continue &gt;&gt;&gt;" onclick="location.href='<%=Url.Action("Shipping","Order") %>'" />
        <br />

        
        <h4>... You can use your Open ID</h4><hr />
 	    <!-- Simple OpenID Selector -->
	    <link rel="stylesheet" href="/content/openid.css" />
	    <script type="text/javascript" src="/scripts/jquery-1.3.2.min.js"></script>
	    <script type="text/javascript" src="/scripts/openid-jquery.js"></script>
	    <script type="text/javascript">
	    $(document).ready(function() {
	        openid.init('openid_identifier');
	    });
	    </script>
        <p>
            <!-- Simple OpenID Selector -->
            <form action="<%=Url.Action("OpenIdLogin","Authentication") %>?ReturnUrl=<%=Url.Action("Checkout","Order")%>" method="get" id="openid_form">
	            <input type="hidden" name="action" value="verify" />

	            <fieldset style="width:500px">
    		            <legend>Sign-in or Create New Account</legend>
                		
    		            <div id="openid_choice">
	    		            <p>Please click your account provider:</p>
	    		            <div id="openid_btns"></div>
			            </div>
            			
			            <div id="openid_input_area">
				            <input id="openid_identifier" name="openid_identifier" type="text" value="http://" />
				            <input id="openid_submit" type="submit" value="Sign-In"/>
			            </div>
			            <noscript>
			            <p>OpenID is service that allows you to log-on to many different websites using a single indentity.
			            Find out <a href="http://openid.net/what/">more about OpenID</a> and <a href="http://openid.net/get/">how to get an OpenID enabled account</a>.</p>
			            </noscript>
	            </fieldset>
            </form>
        </p>

        <h4>... Or login with an AdventureWorks account</h4><hr />
        <form method="post" action="<%=Url.Action("Login","Authentication")%>?ReturnUrl=<%=Url.GetSiteUrl()+"/"+Url.Action("Shipping","Order")%>">
        <input type="hidden" name="ReturnUrl" value="<%=Url.AbsoluteAction("Shipping")%>" />
        <table>
            <tr>
                <td>Login:</td>
                <td><%= Html.TextBox("login") %></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td><%= Html.Password("password") %></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="checkbox" name="rememberMe" value="true" /> Remember me?</td>
            </tr>
            <tr>
                <td colspan=2>
                    <input type="submit" value="Login" />
                </td>
            </tr>
        </table>
        </form>   
  </div>

<!--Clear out three column layout-->
<div class="clearLayout"></div>
<!--End bdy-->
</div>
</asp:Content>

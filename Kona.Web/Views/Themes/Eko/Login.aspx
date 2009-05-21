<%@ Page Title="" Language="C#" MasterPageFile="Theme.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=this.SiteData().SiteName %>: Login</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


<div id="bdy">
    <div class="colmiddle780 cntr">
        <h4>Login with your Open ID</h4><hr />
 	    <!-- Simple OpenID Selector -->
	    <link rel="stylesheet" href="/content/openid.css" />
	    <script type="text/javascript" src="/scripts/jquery-1.3.2.min.js"></script>
	    <script type="text/javascript" src="/scripts/openid-jquery.js"></script>
	    <script type="text/javascript">
	    $(document).ready(function() {
	        openid.init('openid_identifier');
	    });
	    </script>
	    <!-- /Simple OpenID Selector -->
        <p>

        <!-- Simple OpenID Selector -->
        <form action="<%=Url.Action("OpenIdLogin","Authentication") %>" method="get" id="openid_form">
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

        <h4>... Or login with your password</h4><hr />
        <form method="post" action="<%=Url.Action("Login","Authentication")%>">
        <input type="hidden" name="ReturnUrl" value="" />
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

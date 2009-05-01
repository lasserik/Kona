using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using System.Xml.Linq;
using Kona.Data;
using Kona.Infrastructure;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;

namespace Kona.Web.Controllers {
    public class AuthenticationController : KonaController {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService) {
            _authService = authService;
        }

        public ActionResult Login() {
            string login = Request.Form["login"];
            string password = Request.Form["password"];
            string remember = Request.Form["rememberMe"];
            bool setCookie = false;
            if (!string.IsNullOrEmpty(remember))
                setCookie = remember == "true";

            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password)) {
                bool isValid = _authService.IsValidLogin(login, password);

                //log them in 
                if (isValid) {
                    if (setCookie)
                        FormsAuthentication.SetAuthCookie(login, true);
                    return AuthAndRedirect(login, login);
                }
            }
            return View();
        }

        public ActionResult OpenIdLogin() {
            string returnUrl = VirtualPathUtility.ToAbsolute("~/");
            var openid = new OpenIdRelyingParty();
            var response = openid.GetResponse();
            if (response == null) {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(Request["openid_identifier"], out id)) {
                    try {
                        IAuthenticationRequest req = openid.CreateRequest(Request["openid_identifier"]);

                        var fetch = new FetchRequest();
                        //ask for more info - the email address
                        var item = new AttributeRequest(WellKnownAttributes.Contact.Email);
                        item.IsRequired = true;
                        fetch.Attributes.Add(item);
                        req.AddExtension(fetch);

                        return req.RedirectingResponse.AsActionResult();
                    } catch (ProtocolException ex) {
                        ViewData["Message"] = ex.Message;
                        return View("Logon");
                    }
                } else {
                    ViewData["Message"] = "Invalid identifier";
                    return View("Logon");
                }
            } else {
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status) {
                    case AuthenticationStatus.Authenticated:

                        var fetch = response.GetExtension<FetchResponse>();
                        string name = response.FriendlyIdentifierForDisplay;
                        if (fetch != null) {
                            IList<string> emailAddresses = fetch.Attributes[WellKnownAttributes.Contact.Email].Values;
                            string email = emailAddresses.Count > 0 ? emailAddresses[0] : null;
                            //don't show the email - it's creepy. Just use the name of the email
                            name = email.Substring(0, email.IndexOf('@'));
                        } else {

                            name = name.Substring(0, name.IndexOf('.'));
                        }

                        //FormsAuthentication.SetAuthCookie(name, false);
                        SetCookies(name, name);
                        AuthAndRedirect(name, name);

                        if (!string.IsNullOrEmpty(returnUrl)) {
                            return Redirect(returnUrl);
                        } else {
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("Logon");
                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("Logon");
                }
            }
            return new EmptyResult();

        }

        ActionResult AuthAndRedirect(string userName, string friendlyName) {
            string returnUrl = Request["ReturnUrl"];
            SetCookies(userName, friendlyName);

            if (!String.IsNullOrEmpty(returnUrl)) {
                return Redirect(returnUrl);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        void SetCookies(string userName, string friendlyName) {
            Response.Cookies["shopper"].Value = userName;
            Response.Cookies["shopper"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["shopper"].HttpOnly = true;

            Response.Cookies["shopperName"].Value = friendlyName;
            Response.Cookies["shopperName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["shopperName"].HttpOnly = true;

            FormsAuthentication.SetAuthCookie(userName, true);
        }

        public ActionResult Logout() {
            Response.Cookies["shopper"].Value = null;
            Response.Cookies["shopper"].Expires = DateTime.Now.AddDays(-1);

            Response.Cookies["shopperName"].Value = null;
            Response.Cookies["shopperName"].Expires = DateTime.Now.AddDays(-1);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}

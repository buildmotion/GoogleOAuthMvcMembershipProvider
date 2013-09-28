#region

using System;
using System.Web.Mvc;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

#endregion

namespace BuildMotion.Proofs.WebSite.Controllers
{
	public class SecurityController : BuildMotionControllerBase
	{
		private readonly WebServerClient GoogleClient = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="SecurityController"/> class.
		/// </summary>
		public SecurityController()
		{
			this.GoogleClient = MembershipService.CreateGoogleClient(this.MembershipService.RetrieveAuthServerDescription());
		}

		[AllowAnonymous]
		public ActionResult Index(string returnUrl)
		{
			if (string.IsNullOrEmpty(Request.QueryString["code"]))
			{
				Session["ReturnUrl"] = returnUrl;
				return InitAuth();
			}
			else
			{
				returnUrl = Session["ReturnUrl"] != null ? Session["ReturnUrl"].ToString() : string.Empty;
				return OAuthCallback(returnUrl);
			}
		}

		[AllowAnonymous]
		public ActionResult OAuth2Callback(string message)
		{
			ViewBag["Message"] = message;
			return View();
		}

		[AllowAnonymous]
		private ActionResult InitAuth()
		{
			var state = new AuthorizationState();
			if (Request.Url != null)
			{
				var uri = Request.Url.AbsoluteUri;
				uri = RemoveQueryStringFromUri(uri);
				state.Callback = new Uri(uri);
			}

			state.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
			state.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
			state.Scope.Add("https://www.googleapis.com/auth/calendar");

			var outgoingWebResponse = GoogleClient.PrepareRequestUserAuthorization(state);
			return outgoingWebResponse.AsActionResult();
		}

		private static string RemoveQueryStringFromUri(string uri)
		{
			int index = uri.IndexOf('?');
			if (index > -1)
			{
				uri = uri.Substring(0, index);
			}
			return uri;
		}

		[AllowAnonymous]
		private ActionResult OAuthCallback(string redirectUrl)
		{
			var authorizationState = GoogleClient.ProcessUserAuthorization(this.Request); // retrieve [AuthToken] using Request;
			if (authorizationState != null && !string.IsNullOrEmpty(authorizationState.AccessToken) && !string.IsNullOrEmpty(authorizationState.RefreshToken))
			{
				bool tokenIsValid = this.MembershipService.ValidateToken(authorizationState.AccessToken);

				if (tokenIsValid)
				{
					UserInformation userInformation = this.MembershipService.RetrieveGoogleUserInformation(authorizationState.AccessToken);
					if (this.MembershipService.UserDomainIsValid(userInformation.Domain))
					{
						AuthorizationInformation authInfo = new AuthorizationInformation
						{
							AccessToken = authorizationState.AccessToken,
							AccessTokenExpirationUtc = authorizationState.AccessTokenExpirationUtc.GetValueOrDefault(),
							AccessTokenIssueDateUtc = authorizationState.AccessTokenIssueDateUtc.GetValueOrDefault(),
							Email = userInformation.Email,
							RefreshToken = authorizationState.RefreshToken
						};

						UserInformation user = this.MembershipService.CreateUpdateAuthorizationUserInfo(authInfo, userInformation);
						if(user != null)
						{
							this.CreateFormsAuthenticationTicket(user);
						}
						else
						{
							return UnableToValidateRedirectToAction();
						}
					}
					else
					{
						return UnableToValidateRedirectToAction();
					}
				}
			}
			else
			{
				return UnableToValidateRedirectToAction();
			}

			// If you get here; the user is validated; redirecto to speicified url;
			#region Handle the redirection; retrieve the redirect URL information for current request;
			string formsAuthenticationRedirectUrl = string.Empty;
			if (Session["ReturnUrl"] != null)
			{
				formsAuthenticationRedirectUrl = Session["ReturnUrl"].ToString();
			}
			string url = !string.IsNullOrEmpty(redirectUrl) ? redirectUrl : formsAuthenticationRedirectUrl;
			if (string.IsNullOrEmpty(url))
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return Redirect(url);
			}
			#endregion;
		}

		private ActionResult UnableToValidateRedirectToAction()
		{
			return RedirectToAction("OAuth2Callback", "Security", new{message = "Unable to validate your Google credentials."});
		}

		/// <summary>
		/// Creates the forms authentication ticket.
		/// </summary>
		/// <param name="user">The user.</param>
		private void CreateFormsAuthenticationTicket(UserInformation user)
		{
			System.Web.HttpCookie cookie = this.MembershipService.CreateFormsAuthenticationCookie(user);
			if (cookie != null)
			{
				this.Response.Cookies.Add(cookie);
			}
		}
	}
}
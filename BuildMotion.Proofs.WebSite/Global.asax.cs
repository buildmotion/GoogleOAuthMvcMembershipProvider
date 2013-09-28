#region

using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BuildMotion.Membership;
using BuildMotion.Membership.Business;
using BuildMotion.Membership.DataAccess.Configurations;
using BuildMotion.Membership.Entity.Google;
using BuildMotion.Proofs.WebSite.App_Start;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

#endregion

namespace BuildMotion.Proofs.WebSite
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication
	{
		private IMembershipService membershipService = null;
		
		/// <summary>
		///     Gets the build motion service.
		/// </summary>
		/// <value>
		///     The build motion service.
		/// </value>
		public IMembershipService MembershipService
		{
			get
			{
				if(this.membershipService == null)
				{
					this.InitializeMembershipService();
				}
				return membershipService;
			}
		}

		/// <summary>
		/// Application_s the start.
		/// </summary>
		protected void Application_Start()
		{
			
			this.InitializeBuildMotionDb();
			this.InitializeMembershipService();

			GlobalConfiguration.Configuration.Filters.Add(new AuthorizeAttribute());
			
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}

		/// <summary>
		/// Handles the AuthenticateRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			/* DO NOT perform FormAuthentication in this method.
			 *
			 * The Principal is put into a RolePrincipal object - not compatible with IPrincipal. This
			 * is too early to set the HttpContext.Current.User to the principal object. Perform this later
			 * in the processing lifecycle: Application_OnPostAuthenticateRequest(..).
			 * 
			 * (see http://msdn.microsoft.com/en-us/library/system.web.security.roleprincipal.aspx)
			 * 
			 * This solution is using the GenericPrincipal class as the base class for the custom 
			 * principal type: OAuthPrincipal.
			 */
		}

		/// <summary>
		/// Handles the OnPostAuthenticateRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		public void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
		{
			// [true|false]: Suppress the redirect when Authorize fails when using [Roles]; 
			HttpContext.Current.Response.SuppressFormsAuthenticationRedirect = Convert.ToBoolean(ConfigurationManager.AppSettings["SuppressFormsAuthenticationRedirect"].ToString());

			// retrieve the authentication cookie by name;
			HttpCookie authorizationCookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];

			if(authorizationCookie != null) // user has been previously authenticated.
			{
				// retrieve FormsAuthenticationTicket by decrypting value in authorization cookie.
				FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authorizationCookie.Value);

				if(authenticationTicket != null)
				{
					// Create the IIdentity and IPrincipal items for the request context.
					OAuthIdentity identity = new OAuthIdentity(authenticationTicket.Name, true, "OAuthAuthentication");
					OAuthPrincipal principal = new OAuthPrincipal(identity, authenticationTicket.UserData.Split(new char[] { '|' }));

					// set the current user to the principal - retrieved from FormAuthentication; 
					HttpContext.Current.User = principal;

					// Make sure the Principal's are in sync
					System.Threading.Thread.CurrentPrincipal = HttpContext.Current.User;
				}
			}
		}

		/// <summary>
		/// Initializes the build motion db.
		/// </summary>
		private void InitializeBuildMotionDb()
		{
			#region Database Initialization ***TURN OFF FOR PRODUCTION***
			/* You will need to turn this OFF for production and other deployments.
			 * It is only relevant for initial/rapid development scenarios. Or if 
			 * you are a hard-core EF fanatic...and really like this bit of chaos. 
			 * 
			 * Use the appSetting item [InitializeBuildMotionDb] to configure;
			 */
			if(Convert.ToBoolean(ConfigurationManager.AppSettings["InitializeBuildMotionDb"].ToString()))
			{
				// initialize the datbase with seed data;
				Database.SetInitializer(new BuildMotionDbInitializer());
			}
			#endregion;
		}

		/// <summary>
		///     Initializes the build motion service.
		/// </summary>
		private void InitializeMembershipService()
		{
			// attempt to initialize;
			if (this.membershipService == null)
			{
				//initialize;
				this.membershipService = Bootstrapper.Install();
			}
		}
	}
}
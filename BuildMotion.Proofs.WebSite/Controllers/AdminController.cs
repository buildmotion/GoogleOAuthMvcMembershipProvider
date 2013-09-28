#region

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BuildMotion.Membership.Entity.Google;


#endregion

namespace BuildMotion.Proofs.WebSite.Controllers
{
	[System.Web.Mvc.Authorize]
	public class AdminController : Controller
	{
		[System.Web.Mvc.Authorize(Roles = "admin,user")]
		public ActionResult Index()
		{
			OAuthPrincipal principal = (OAuthPrincipal)HttpContext.User;
			bool isAdmin = false;
			if (principal != null)
			{
				string username = principal.Identity.Name;
				bool isAuthenticated = principal.Identity.IsAuthenticated;
				isAdmin = principal.IsInRole("admin");
			}

			if (!isAdmin)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
			}
			return View(principal);
		}

		[System.Web.Mvc.Authorize(Roles = "superadmin")]
		public ActionResult SuperAdmin()
		{
			return View();
		}
	}
}
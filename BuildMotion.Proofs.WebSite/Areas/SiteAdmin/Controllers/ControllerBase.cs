#region

using System.Web.Mvc;
using BuildMotion.Membership;

#endregion

namespace BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Controllers
{
	public class ControllerBase : Controller
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
				if (this.membershipService == null)
				{
					// retrieve from application;
					this.membershipService = ((MvcApplication)System.Web.HttpContext.Current.ApplicationInstance).MembershipService;
				}
				return membershipService;
			}
		}
	}
}
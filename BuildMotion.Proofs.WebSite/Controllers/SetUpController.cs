#region

using System.Web.Mvc;

#endregion

namespace BuildMotion.Proofs.WebSite.Controllers
{
	public class SetUpController : Controller
	{
		//
		// GET: /SetUp/
		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}
	}
}
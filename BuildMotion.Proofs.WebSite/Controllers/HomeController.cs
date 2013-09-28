using System.Web.Mvc;

namespace BuildMotion.Proofs.WebSite.Controllers
{
	public class HomeController : Controller
	{
		[AllowAnonymous]
		public ActionResult Index()
		{
			ViewBag.Message = "Hit the Road Running";
			return View();
		}

		[AllowAnonymous]
		public ActionResult About()
		{
			ViewBag.Message = "About Build Motion";
			return View();
		}

		[AllowAnonymous]
		public ActionResult Contact()
		{
			ViewBag.Message = "Contact Information";
			return View();
		}
	}
}

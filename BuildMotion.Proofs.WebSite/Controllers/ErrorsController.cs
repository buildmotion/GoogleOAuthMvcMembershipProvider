using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMotion.Proofs.WebSite.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Error401()
		{
			return View();
		}
    }
}

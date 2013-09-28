#region

using System.Web.Mvc;
using BuildMotion.Membership.Entity.Google;
using BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Models;

#endregion

namespace BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Controllers
{
	public class RolesController : ControllerBase
	{
		//
		// GET: /SiteAdmin/Roles/
		[Authorize(Roles = "admin")]
		public ActionResult Index()
		{
			UsersRolesModel model = new UsersRolesModel();
			model.Roles = this.MembershipService.RetrieveRoles();
			model.Users = this.MembershipService.RetrieveUsers();
			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int id)
		{
			//retrieve specified role by id;
			Role role = this.MembershipService.RetrieveRole(id);
			if (role != null)
			{
				return View(role);
			}
			return RedirectToAction("Index", "Roles");
		}

		/// <summary>
		/// Edits the specified role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult Edit(Role role)
		{
			if (ModelState.IsValid)
			{
				bool isSaved = this.MembershipService.UpdateRole(role);
				if (isSaved)
				{
					return RedirectToAction("Index");
				}
			}
			return View(role);
		}

		[HttpGet]
		public ActionResult Create()
		{
			Role model = new Role();
			model.IsActive = true;
			return View(model);
		}

		/// <summary>
		/// Creates the specified role.
		/// </summary>
		/// <param name="role">The role.</param>
		[HttpPost]
		public ActionResult Create(Role role)
		{
			if (ModelState.IsValid)
			{
				bool isCreated = this.MembershipService.CreateRole(role);
				if (isCreated)
				{
					return RedirectToAction("Index");
				}
			}
			return View(role);
		}
	}
}
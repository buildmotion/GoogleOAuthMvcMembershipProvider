#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BuildMotion.Membership.Entity.Google;
using BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Models;

#endregion

namespace BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Controllers
{
	public class UsersController : ControllerBase
	{
		/// <summary>
		///     Edits the user.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult EditUser(string emailAddress)
		{
			ManageUserRolesModel model = RetrieveUserRolesModel(emailAddress);
			if (model.UserRoles.Count > 0)
			{
				return View(model);
			}
			return RedirectToAction("Index", "Roles");
		}

		/// <summary>
		///     Retrieves the user roles model.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		private ManageUserRolesModel RetrieveUserRolesModel(string emailAddress)
		{
			UserInformation user = this.MembershipService.RetrieveUserInformation(emailAddress);
			EmailInRoles userRoles = this.MembershipService.RetrieveUserRoles(user.Email);
			Roles allRoles = this.MembershipService.RetrieveRoles();
			allRoles.ForEach(delegate(Role r) { r.IsActive = userRoles.Find(i => i.RoleId == r.RoleId) != null; });

			List<RoleCheckBoxModel> availableRoles = allRoles.Select(r => new RoleCheckBoxModel{
				RoleId = r.RoleId,
				Name = r.Name,
				IsChecked = r.IsActive
			}).OrderBy(o => o.Name).ToList();

			ManageUserRolesModel model = new ManageUserRolesModel{
				User = user,
				UserRoles = userRoles,
				AvailableRoles = availableRoles,
				SelectedAvailableRoles = new List<int>()
			};
			return model;
		}

		/// <summary>
		///     Edits the user.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult EditUser(ManageUserRolesModel model)
		{
			if (ModelState.IsValid)
			{
				// retrieve un-modified user roles;
				EmailInRoles originalRoles = this.MembershipService.RetrieveUserRoles(model.User.Email);
				List<Role> originalUserRoles = originalRoles.Select(r => new Role{
					RoleId = r.Role.RoleId,
					Name = r.Role.Name,
					Description = r.Role.Description,
					IsActive = r.Role.IsActive
				}).ToList();

				// compare un-modified list to list returned from [HttpPost]
				List<int> roleIdsSelected = model.SelectedAvailableRoles;
				List<Role> selectedRoles = (from item in this.MembershipService.RetrieveRoles()
				                            join s in roleIdsSelected
					                            on item.RoleId equals s
				                            select item).ToList();

				// Roles to un-associate/remove;
				List<Role> removeRoles = originalUserRoles.Except(selectedRoles, new RoleComparer()).ToList();

				// Roles to associate/add to the user;
				List<Role> addRoles = selectedRoles.Except(originalUserRoles, new RoleComparer()).ToList();

				bool isUpdated = this.MembershipService.UpdateUser(model.User);
				isUpdated = isUpdated && this.MembershipService.AddRolesToUser(addRoles, model.User);
				isUpdated = isUpdated && this.MembershipService.RemoveRolesFromUser(removeRoles, model.User);
				if (isUpdated)
				{
					return RedirectToAction("Index", "Roles");
				}
				return View(this.RetrieveUserRolesModel(model.User.Email));
			}
			return View(model);
		}
	}
}
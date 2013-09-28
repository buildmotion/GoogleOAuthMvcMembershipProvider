#region

using System.Collections.Generic;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Models
{
	public class ManageUserRolesModel
	{
		/// <summary>
		///     Gets or sets the user.
		/// </summary>
		/// <value>
		///     The user.
		/// </value>
		public UserInformation User{ get; set; }

		/// <summary>
		///     Gets or sets the user roles.
		/// </summary>
		/// <value>
		///     The user roles.
		/// </value>
		public EmailInRoles UserRoles{ get; set; }

		/// <summary>
		///     Gets or sets the available roles.
		/// </summary>
		/// <value>
		///     The available roles.
		/// </value>
		public List<RoleCheckBoxModel> AvailableRoles{ get; set; }

		/// <summary>
		///     Gets or sets the selected available roles.
		/// </summary>
		/// <value>
		///     The selected available roles.
		/// </value>
		public List<int> SelectedAvailableRoles{ get; set; }
	}
}
#region

using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Proofs.WebSite.Areas.SiteAdmin.Models
{
	public class UsersRolesModel
	{
		/// <summary>
		///     Gets or sets the roles.
		/// </summary>
		/// <value>
		///     The roles.
		/// </value>
		public Roles Roles{ get; set; }

		/// <summary>
		///     Gets or sets the users.
		/// </summary>
		/// <value>
		///     The users.
		/// </value>
		public Users Users{ get; set; }
	}
}
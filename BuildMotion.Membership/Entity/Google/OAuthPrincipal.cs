#region

using System.Security.Principal;

#endregion

namespace BuildMotion.Membership.Entity.Google
{
	/// <summary>
	/// Use to contain information about "what" the user can do/access. 
	/// </summary>
	public class OAuthPrincipal : GenericPrincipal // IPrincipal // RolePrincipal 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OAuthPrincipal" /> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs.</param>
		public OAuthPrincipal(IIdentity identity, string[] roles) : base(identity, roles)
		{
		}

		///// <summary>
		///// Determines whether the current principal belongs to the specified role.
		///// </summary>
		///// <param name="role">The name of the role for which to check membership.</param>
		///// <returns>
		///// true if the current principal is a member of the specified role; otherwise, false.
		///// </returns>
		//public new bool IsInRole(string role)
		//{
		//	// determine if the specified [role] is contained in the associated roles for the user.
		//	var roleInRoles = (from r in this.IsInRole()
		//					   where r.ToLower() == role.ToLower()
		//					   select r).FirstOrDefault();
		//	if (roleInRoles != null)
		//	{
		//		return true;
		//	}
		//	return false;
		//}
	}
}
using System.Collections.Generic;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

namespace BuildMotion.Membership.DataAccess
{
	public abstract class DataAccessBase
	{
		/// <summary>
		///     Gets or sets the name of the connection string.
		/// </summary>
		/// <value>
		///     The name of the connection string.
		/// </value>
		public abstract string ConnectionStringName{ get; set; }

		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public abstract UserInformation RetrieveUserInformation(string email);

		/// <summary>
		/// Creates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public abstract UserInformation CreateUserInformation(UserInformation userInfo);

		/// <summary>
		/// Creates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		public abstract bool CreateAuthorizationInformation(AuthorizationInformation authorization);

		/// <summary>
		/// Updates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public abstract UserInformation UpdateUserInformation(UserInformation userInfo);

		/// <summary>
		/// Updates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <returns></returns>
		public abstract bool UpdateAuthorizationInformation(AuthorizationInformation authorization);

		public abstract List<EmailInRole> RetrieveUserRoles(string email);

		/// <summary>
		/// Creates the email in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public abstract bool CreateEmailInRole(EmailInRole emailInRole);

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public abstract Roles RetrieveRoles();

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public abstract Role RetrieveRole(int roleId);

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public abstract bool UpdateRole(Role role);

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public abstract bool RemoveUserInRole(EmailInRole emailInRole);

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public abstract bool CreateRole(Role role);

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		public abstract Users RetrieveUsers();
	}
}
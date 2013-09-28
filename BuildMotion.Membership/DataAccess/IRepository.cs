using System.Collections.Generic;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

namespace BuildMotion.Membership.DataAccess
{
	public interface IRepository
	{
		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		UserInformation RetrieveUserInformation(string email);

		/// <summary>
		/// Creates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		UserInformation CreateUserInformation(UserInformation userInfo);

		/// <summary>
		/// Creates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		bool CreateAuthorizationInformation(AuthorizationInformation authorization);

		/// <summary>
		/// Updates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		UserInformation UpdateUserInformation(UserInformation userInfo);

		/// <summary>
		/// Updates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		bool UpdateAuthorizationInformation(AuthorizationInformation authorization);

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		List<EmailInRole> RetrieveUserRoles(string email);

		/// <summary>
		/// Creates the email in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		bool CreateEmailInRole(EmailInRole emailInRole);

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		Roles RetrieveRoles();

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		Role RetrieveRole(int roleId);

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		bool UpdateRole(Role role);

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		bool RemoveUserInRole(EmailInRole emailInRole);

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		bool CreateRole(Role role);

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		Users RetrieveUsers();
	}
}
using System.Collections.Generic;
using System.Web;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.OAuth2;

namespace BuildMotion.Membership
{
	public interface IMembershipService
	{
		/// <summary>
		/// Creates the update authorization user info.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		UserInformation CreateUpdateAuthorizationUserInfo(AuthorizationInformation authorization, UserInformation userInfo);

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		string RetrieveUserRolesString(string email);

		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		UserInformation RetrieveUserInformation(string emailAddress);

		/// <summary>
		/// Creates the forms authentication cookie.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		HttpCookie CreateFormsAuthenticationCookie(UserInformation user);

		/// <summary>
		/// Validates the token.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		bool ValidateToken(string accessToken);

		/// <summary>
		/// Retrieves the google user information.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		UserInformation RetrieveGoogleUserInformation(string accessToken);

		/// <summary>
		/// Creates the google client.
		/// </summary>
		/// <param name="authorizationServerDescription"></param>
		/// <returns></returns>
		WebServerClient CreateGoogleClient(AuthorizationServerDescription authorizationServerDescription);

		/// <summary>
		/// Users the is valid.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		bool UserDomainIsValid(string domain);

		/// <summary>
		/// Gets the google app domain.
		/// </summary>
		/// <value>
		/// The google app domain.
		/// </value>
		string GoogleAppDomain{ get; }

		/// <summary>
		/// Gets the google client id.
		/// </summary>
		/// <value>
		/// The google client id.
		/// </value>
		string GoogleClientId{ get; }

		/// <summary>
		/// Gets the google client secret.
		/// </summary>
		/// <value>
		/// The google client secret.
		/// </value>
		string GoogleClientSecret{ get; }

		/// <summary>
		/// Retrieves the auth server description.
		/// </summary>
		/// <returns></returns>
		AuthorizationServerDescription RetrieveAuthServerDescription();

		/// <summary>
		/// Refreshes the access token.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		string RefreshAccessToken(UserInformation user);

		/// <summary>
		/// Retrieves the token info.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		TokenInfo RetrieveTokenInfo(string accessToken);

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
		/// Retrieves the current access token.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		string RetrieveCurrentAccessToken(string emailAddress);

		EmailInRoles RetrieveEmailInRoles(string emailAddress);

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		bool RemoveUserInRole(EmailInRole emailInRole);

		/// <summary>
		/// Adds the user to role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		bool AddUserToRole(EmailInRole emailInRole);

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

		/// <summary>
		/// Updates the user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		bool UpdateUser(UserInformation user);

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		EmailInRoles RetrieveUserRoles(string email);

		/// <summary>
		/// Adds the roles to user.
		/// </summary>
		/// <param name="addRoles">The add roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		bool AddRolesToUser(List<Role> addRoles, UserInformation user);

		/// <summary>
		/// Removes the roles from user.
		/// </summary>
		/// <param name="removeRoles">The remove roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		bool RemoveRolesFromUser(List<Role> removeRoles, UserInformation user);
	}
}
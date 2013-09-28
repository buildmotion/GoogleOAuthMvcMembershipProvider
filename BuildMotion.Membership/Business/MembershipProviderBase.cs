#region

using System.Collections.Generic;
using System.Web;
using BuildMotion.Membership.DataAccess;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.OAuth2;

#endregion

namespace BuildMotion.Membership.Business
{
	public abstract class MembershipProviderBase
	{
		/// <summary>
		///     Gets the repository.
		/// </summary>
		/// <value>
		///     The repository.
		/// </value>
		public abstract IRepository Repository{ get; }

		/// <summary>
		///     Gets or sets a value indicating whether this instance is persistent cookie.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is persistent cookie; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsPersistentCookie{ get; set; }

		/// <summary>
		///     Gets or sets the cookie expiration in hours.
		/// </summary>
		/// <value>
		///     The cookie expiration in hours.
		/// </value>
		public abstract int CookieExpirationInHours{ get; set; }

		/// <summary>
		///     Gets or sets the google client id.
		/// </summary>
		/// <value>
		///     The google client id.
		/// </value>
		public abstract string GoogleClientId{ get; set; }

		/// <summary>
		///     Gets or sets the google client secret.
		/// </summary>
		/// <value>
		///     The google client secret.
		/// </value>
		public abstract string GoogleClientSecret{ get; set; }

		/// <summary>
		/// Gets or sets the google app domain.
		/// </summary>
		/// <value>
		/// The google app domain.
		/// </value>
		public abstract string GoogleAppDomain{ get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [restrict users to google app domain].
		/// </summary>
		/// <value>
		/// <c>true</c> if [restrict users to google app domain]; otherwise, <c>false</c>.
		/// </value>
		public abstract bool RestrictUsersToGoogleAppDomain{ get; set; }

		/// <summary>
		///     Creates the update authorization user info.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public abstract UserInformation CreateUpdateAuthorizationUserInfo(AuthorizationInformation authorization,
		                                                                  UserInformation userInfo);

		/// <summary>
		///     Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public abstract string RetrieveUserRolesString(string email);

		/// <summary>
		///     Adds the email to role.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="roleId">The role id.</param>
		public abstract bool AddEmailToRole(string email, int roleId);

		/// <summary>
		///     Retrieves the user information.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public abstract UserInformation RetrieveUserInformation(string emailAddress);

		/// <summary>
		///     Creates the forms authentication cookie.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public abstract HttpCookie CreateFormsAuthenticationCookie(UserInformation user);

		/// <summary>
		///     Validates the token.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public abstract bool ValidateToken(string accessToken);

		/// <summary>
		///     Gets the token info.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public abstract TokenInfo GetTokenInfo(string accessToken);

		/// <summary>
		///     Retrieves the google user information.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public abstract UserInformation RetrieveGoogleUserInformation(string accessToken);

		/// <summary>
		///     Creates the google client.
		/// </summary>
		/// <returns></returns>
		public abstract WebServerClient CreateGoogleClient(AuthorizationServerDescription authorizationServer);

		/// <summary>
		///     Gets the auth server description.
		/// </summary>
		/// <returns></returns>
		public abstract AuthorizationServerDescription GetAuthServerDescription();

		/// <summary>
		/// Users the domain is valid.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		public abstract bool UserDomainIsValid(string domain);

		/// <summary>
		/// Refreshes the access token.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public abstract string RefreshAccessToken(UserInformation user);

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
		/// Retrieves the current access token.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public abstract string RetrieveCurrentAccessToken(string emailAddress);

		/// <summary>
		/// Retrieves the email in roles.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public abstract EmailInRoles RetrieveEmailInRoles(string emailAddress);

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public abstract bool RemoveUserInRole(EmailInRole emailInRole);

		/// <summary>
		/// Adds the user to role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public abstract bool AddUserToRole(EmailInRole emailInRole);

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

		/// <summary>
		/// Updates the user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public abstract bool UpdateUser(UserInformation user);

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public abstract EmailInRoles RetrieveUserRoles(string email);

		/// <summary>
		/// Adds the roles to user.
		/// </summary>
		/// <param name="addRoles">The add roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public abstract bool AddRolesToUser(List<Role> addRoles, UserInformation user);

		/// <summary>
		/// Removes the roles from user.
		/// </summary>
		/// <param name="removeRoles">The remove roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public abstract bool RemoveRolesFromUser(List<Role> removeRoles, UserInformation user);
	}
}
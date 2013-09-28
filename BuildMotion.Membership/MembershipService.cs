#region

using System;
using System.Collections.Generic;
using System.Web;
using BuildMotion.Membership.Business;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.OAuth2;

#endregion

namespace BuildMotion.Membership
{
	public class MembershipService : IMembershipService
	{
		private readonly MembershipProviderBase provider;

		/// <summary>
		///     Initializes a new instance of the <see cref="MembershipService" /> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <exception cref="System.ArgumentNullException">provider</exception>
		public MembershipService(MembershipProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this.provider = provider;
		}

		/// <summary>
		///     Creates the update authorization user info.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public UserInformation CreateUpdateAuthorizationUserInfo(AuthorizationInformation authorization,
		                                                         UserInformation userInfo)
		{
			return this.provider.CreateUpdateAuthorizationUserInfo(authorization, userInfo);
		}

		/// <summary>
		///     Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public string RetrieveUserRolesString(string email)
		{
			return this.provider.RetrieveUserRolesString(email);
		}

		/// <summary>
		///     Retrieves the user information.
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		public UserInformation RetrieveUserInformation(string emailAddress)
		{
			return this.provider.RetrieveUserInformation(emailAddress);
		}

		/// <summary>
		///     Creates the forms authentication cookie.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public HttpCookie CreateFormsAuthenticationCookie(UserInformation user)
		{
			return this.provider.CreateFormsAuthenticationCookie(user);
		}

		/// <summary>
		///     Validates the token.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public bool ValidateToken(string accessToken)
		{
			return provider.ValidateToken(accessToken);
		}

		/// <summary>
		///     Retrieves the google user information.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public UserInformation RetrieveGoogleUserInformation(string accessToken)
		{
			return this.provider.RetrieveGoogleUserInformation(accessToken);
		}

		/// <summary>
		///     Creates the google client.
		/// </summary>
		/// <param name="authorizationServerDescription"></param>
		/// <returns></returns>
		public WebServerClient CreateGoogleClient(AuthorizationServerDescription authorizationServerDescription)
		{
			return this.provider.CreateGoogleClient(authorizationServerDescription);
		}

		/// <summary>
		///     Determines if the user's domain is valid.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		public bool UserDomainIsValid(string domain)
		{
			return this.provider.UserDomainIsValid(domain);
		}

		/// <summary>
		///     Gets the google app domain.
		/// </summary>
		/// <value>
		///     The google app domain.
		/// </value>
		public string GoogleAppDomain
		{
			get
			{
				return this.provider.GoogleAppDomain;
			}
		}

		/// <summary>
		///     Gets the google client id.
		/// </summary>
		/// <value>
		///     The google client id.
		/// </value>
		public string GoogleClientId
		{
			get
			{
				return this.provider.GoogleClientId;
			}
		}

		/// <summary>
		/// Gets the google client secret.
		/// </summary>
		/// <value>
		/// The google client secret.
		/// </value>
		public string GoogleClientSecret{
			get
			{
				return this.provider.GoogleClientSecret;
			}
		}

		/// <summary>
		///     Retrieves the auth server description.
		/// </summary>
		/// <returns></returns>
		public AuthorizationServerDescription RetrieveAuthServerDescription()
		{
			return this.provider.GetAuthServerDescription();
		}

		/// <summary>
		///     Refreshes the access token.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public string RefreshAccessToken(UserInformation user)
		{
			return this.provider.RefreshAccessToken(user);
		}

		/// <summary>
		/// Retrieves the token info.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public TokenInfo RetrieveTokenInfo(string accessToken)
		{
			return this.provider.GetTokenInfo(accessToken);
		}

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public Roles RetrieveRoles()
		{
			return this.provider.RetrieveRoles();
		}

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public Role RetrieveRole(int roleId)
		{
			return this.provider.RetrieveRole(roleId);
		}

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool UpdateRole(Role role)
		{
			return this.provider.UpdateRole(role);
		}

		/// <summary>
		/// Retrieves the current access token.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public string RetrieveCurrentAccessToken(string emailAddress)
		{
			return this.provider.RetrieveCurrentAccessToken(emailAddress);
		}

		/// <summary>
		/// Retrieves the email in roles.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public EmailInRoles RetrieveEmailInRoles(string emailAddress)
		{
			return this.provider.RetrieveEmailInRoles(emailAddress);
		}

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool RemoveUserInRole(EmailInRole emailInRole)
		{
			return this.provider.RemoveUserInRole(emailInRole);
		}

		/// <summary>
		/// Adds the user to role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool AddUserToRole(EmailInRole emailInRole)
		{
			return this.provider.AddUserToRole(emailInRole);
		}

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool CreateRole(Role role)
		{
			return this.provider.CreateRole(role);
		}

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		public Users RetrieveUsers()
		{
			return this.provider.RetrieveUsers();
		}

		/// <summary>
		/// Updates the user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public bool UpdateUser(UserInformation user)
		{
			return this.provider.UpdateUser(user);
		}

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public EmailInRoles RetrieveUserRoles(string email)
		{
			return this.provider.RetrieveUserRoles(email);
		}

		/// <summary>
		/// Adds the roles to user.
		/// </summary>
		/// <param name="addRoles">The add roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public bool AddRolesToUser(List<Role> addRoles, UserInformation user)
		{
			return this.provider.AddRolesToUser(addRoles, user);
		}

		/// <summary>
		/// Removes the roles from user.
		/// </summary>
		/// <param name="removeRoles">The remove roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public bool RemoveRolesFromUser(List<Role> removeRoles, UserInformation user)
		{
			return this.provider.RemoveRolesFromUser(removeRoles, user);
		}
	}
}
#region

using System;
using System.Collections.Generic;
using System.Web;
using BuildMotion.Membership.Business.Security.Actions;
using BuildMotion.Membership.DataAccess;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.OAuth2;
using Vergosity.Actions;

#endregion

namespace BuildMotion.Membership.Business
{
	public class MembershipProvider : MembershipProviderBase
	{
		private readonly IRepository respository;

		/// <summary>
		///     Initializes a new instance of the <see cref="MembershipProvider" /> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		/// <exception cref="System.ArgumentNullException">repository</exception>
		public MembershipProvider(IRepository repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			this.respository = repository;
		}

		/// <summary>
		///     Gets the repository.
		/// </summary>
		/// <value>
		///     The repository.
		/// </value>
		public override IRepository Repository
		{
			get
			{
				return this.respository;
			}
		}

		/// <summary>
		///     Gets or sets a value indicating whether this instance is persistent cookie.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is persistent cookie; otherwise, <c>false</c>.
		/// </value>
		public override bool IsPersistentCookie{ get; set; }

		/// <summary>
		///     Gets or sets the cookie expiration in hours.
		/// </summary>
		/// <value>
		///     The cookie expiration in hours.
		/// </value>
		public override int CookieExpirationInHours{ get; set; }

		/// <summary>
		///     Gets or sets the google client id.
		/// </summary>
		/// <value>
		///     The google client id.
		/// </value>
		public override string GoogleClientId{ get; set; }

		/// <summary>
		///     Gets or sets the google client secret.
		/// </summary>
		/// <value>
		///     The google client secret.
		/// </value>
		public override string GoogleClientSecret{ get; set; }

		/// <summary>
		///     Gets or sets the google app domain.
		/// </summary>
		/// <value>
		///     The google app domain.
		/// </value>
		public override string GoogleAppDomain{ get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether [restrict users to google app domain].
		/// </summary>
		/// <value>
		///     <c>true</c> if [restrict users to google app domain]; otherwise, <c>false</c>.
		/// </value>
		public override bool RestrictUsersToGoogleAppDomain{ get; set; }

		/// <summary>
		///     Creates the update authorization user info.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public override UserInformation CreateUpdateAuthorizationUserInfo(AuthorizationInformation authorization,
		                                                                  UserInformation userInfo)
		{
			UserInformation userInformation = null;
			CreateUpdateAuthorizationUserInfoAction action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInfo,
			                                                                                             this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				userInformation = action.UserInformation;
			}
			return userInformation;
		}

		/// <summary>
		///     Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public override string RetrieveUserRolesString(string email)
		{
			string roles = string.Empty;
			RetrieveUserRolesActionString actionString = new RetrieveUserRolesActionString(email, this);
			actionString.Execute();
			if (actionString.Result == ActionResult.Success)
			{
				roles = actionString.UserRoles;
			}
			return roles;
		}

		/// <summary>
		///     Adds the email to role.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public override bool AddEmailToRole(string email, int roleId)
		{
			bool isAdded = false;
			AddEmailToRoleAction action = new AddEmailToRoleAction(email, roleId, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isAdded = action.IsAdded;
			}
			return isAdded;
		}

		/// <summary>
		///     Retrieves the user information.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public override UserInformation RetrieveUserInformation(string emailAddress)
		{
			UserInformation info = null;
			RetrieveUserInformationAction action = new RetrieveUserInformationAction(emailAddress, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				info = action.UserInformation;
			}
			return info;
		}

		/// <summary>
		///     Creates the forms authentication cookie.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public override HttpCookie CreateFormsAuthenticationCookie(UserInformation user)
		{
			HttpCookie cookie = null;
			CreateFormsAuthenticationCookieAction action = new CreateFormsAuthenticationCookieAction(user, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				cookie = action.Cookie;
			}
			return cookie;
		}

		/// <summary>
		///     Validates the token.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public override bool ValidateToken(string accessToken)
		{
			bool isValid = false;
			ValidateTokenAction action = new ValidateTokenAction(accessToken, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isValid = action.IsValid;
			}
			return isValid;
		}

		/// <summary>
		///     Gets the token info.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public override TokenInfo GetTokenInfo(string accessToken)
		{
			TokenInfo tokenInfo = null;
			RetrieveTokenInfoAction action = new RetrieveTokenInfoAction(accessToken, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				tokenInfo = action.TokenInfo;
			}
			return tokenInfo;
		}

		/// <summary>
		///     Retrieves the google user information.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		public override UserInformation RetrieveGoogleUserInformation(string accessToken)
		{
			UserInformation user = null;
			RetrieveGoogleUserInformationAction action = new RetrieveGoogleUserInformationAction(accessToken, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				user = action.UserInformation;
			}
			return user;
		}

		/// <summary>
		///     Creates the google client.
		/// </summary>
		/// <returns></returns>
		public override WebServerClient CreateGoogleClient(AuthorizationServerDescription authorizationServerDescription)
		{
			WebServerClient client = null;
			CreateGoogleClientAction action = new CreateGoogleClientAction(authorizationServerDescription, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				client = action.Client;
			}
			return client;
		}

		/// <summary>
		///     Gets the auth server description.
		/// </summary>
		/// <returns></returns>
		public override AuthorizationServerDescription GetAuthServerDescription()
		{
			AuthorizationServerDescription description = null;
			GetAuthServerDescriptionAction action = new GetAuthServerDescriptionAction(this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				description = action.AuthorizationServerDescription;
			}
			return description;
		}

		/// <summary>
		///     Users the domain is valid.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <returns></returns>
		public override bool UserDomainIsValid(string domain)
		{
			bool isValid = false;
			UserDomainIsValidAction action = new UserDomainIsValidAction(domain, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isValid = action.DomainIsValid;
			}
			return isValid;
		}

		/// <summary>
		///     Refreshes the access token.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public override string RefreshAccessToken(UserInformation user)
		{
			string accessToken = string.Empty;
			RefreshAccessTokenAction action = new RefreshAccessTokenAction(user, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				accessToken = action.AccessToken;
			}
			return accessToken;
		}

		/// <summary>
		///     Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public override Roles RetrieveRoles()
		{
			Roles roles = new Roles();
			RetrieveRolesAction action = new RetrieveRolesAction(this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				roles = action.ApplicationRoles;
			}
			return roles;
		}

		/// <summary>
		///     Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public override Role RetrieveRole(int roleId)
		{
			Role role = null;
			RetrieveRoleAction action = new RetrieveRoleAction(roleId, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				role = action.Role;
			}
			return role;
		}

		/// <summary>
		///     Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public override bool UpdateRole(Role role)
		{
			bool isUpdated = false;
			UpdateRoleAction action = new UpdateRoleAction(role, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isUpdated = action.IsUpdated;
			}
			return isUpdated;
		}

		/// <summary>
		///     Retrieves the current access token.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public override string RetrieveCurrentAccessToken(string emailAddress)
		{
			string accessToken = string.Empty;
			var action = new RetrieveCurrentAccessTokenAction(emailAddress, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				accessToken = action.AccessToken;
			}
			return accessToken;
		}

		/// <summary>
		///     Retrieves the email in roles.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns></returns>
		public override EmailInRoles RetrieveEmailInRoles(string emailAddress)
		{
			EmailInRoles roles = new EmailInRoles();
			RetrieveEmailInRolesAction action = new RetrieveEmailInRolesAction(emailAddress, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				roles = action.UserRoles;
			}
			return roles;
		}

		/// <summary>
		///     Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public override bool RemoveUserInRole(EmailInRole emailInRole)
		{
			bool isRemoved = false;
			RemoveUserInRoleAction action = new RemoveUserInRoleAction(emailInRole, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isRemoved = action.IsRemoved;
			}
			return isRemoved;
		}

		/// <summary>
		///     Adds the user to role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public override bool AddUserToRole(EmailInRole emailInRole)
		{
			bool isAdded = false;
			AddUserToRoleAction action = new AddUserToRoleAction(emailInRole, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isAdded = action.IsAdded;
			}
			return isAdded;
		}

		/// <summary>
		///     Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public override bool CreateRole(Role role)
		{
			bool isCreated = false;
			CreateRoleAction action = new CreateRoleAction(role, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isCreated = action.IsCreated;
			}
			return isCreated;
		}

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		public override Users RetrieveUsers()
		{
			Users users = new Users();
			var action = new RetrieveUsersAction(this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				users = action.Users;
			}
			return users;
		}

		/// <summary>
		/// Updates the user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public override bool UpdateUser(UserInformation user)
		{
			bool isUpdated = false;
			var action = new UpdateUserAction(user, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isUpdated = action.IsUpdated;
			}
			return isUpdated;
		}

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public override EmailInRoles RetrieveUserRoles(string email)
		{
			EmailInRoles roles = new EmailInRoles();
			RetrieveUserRolesAction action = new RetrieveUserRolesAction(email, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				roles = action.UserRoles;
			}
			return roles;
		}

		/// <summary>
		/// Adds the roles to user.
		/// </summary>
		/// <param name="addRoles">The add roles.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public override bool AddRolesToUser(List<Role> addRoles, UserInformation user)
		{
			bool isAdded = false;
			AddRolesToUserAction action = new AddRolesToUserAction(addRoles, user, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isAdded = action.IsAdded;
			}
			return isAdded;
		}

		public override bool RemoveRolesFromUser(List<Role> removeRoles, UserInformation user)
		{
			bool isRemoved = false;
			var action = new RemoveRolesFromUserAction(removeRoles, user, this);
			action.Execute();
			if (action.Result == ActionResult.Success)
			{
				isRemoved = action.IsRemoved;
			}
			return isRemoved;
		}
	}
}
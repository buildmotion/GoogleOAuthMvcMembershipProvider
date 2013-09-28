//------------------------------------------------------------------------------
// <Vergosity.License>
//    All of the source code, logic, patterns, notes...really anything contained in the 
//		source code, compiled assemblies, or other mechanisms (i.e., drawings, diagrams, 
//		notes, or documentation) are the sole and explicit property of Build Motion, LLC.
//
//    You are entitled to use the compiled representations of the software only if they 
//		are licensed by either Vergosity or Build Motion, LLC. See "License.txt" in compiled
//		resource for details on license limitations and usage agreement.
// </Vergosity.License>
//------------------------------------------------------------------------------
#region

using System;
using System.Collections.Generic;
using BuildMotion.Membership.DataAccess;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.Business
{
	public class Repository : IRepository
	{
		private readonly IDataAdaptor adaptor;

		/// <summary>
		///     Initializes a new instance of the <see cref="Repository" /> class.
		/// </summary>
		/// <param name="adaptor">The adaptor.</param>
		/// <exception cref="System.ArgumentNullException">adaptor</exception>
		public Repository(IDataAdaptor adaptor)
		{
			if (adaptor == null)
			{
				throw new ArgumentNullException("adaptor");
			}
			this.adaptor = adaptor;
		}

		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public UserInformation RetrieveUserInformation(string email)
		{
			return this.adaptor.RetrieveUserInformation(email);
		}

		/// <summary>
		/// Creates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public UserInformation CreateUserInformation(UserInformation userInfo)
		{
			return this.adaptor.CreateUserInformation(userInfo);
		}

		/// <summary>
		/// Creates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		public bool CreateAuthorizationInformation(AuthorizationInformation authorization)
		{
			return this.adaptor.CreateAuthorizationInformation(authorization);
		}

		/// <summary>
		/// Updates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public UserInformation UpdateUserInformation(UserInformation userInfo)
		{
			return this.adaptor.UpdateUserInformation(userInfo);
		}

		/// <summary>
		/// Updates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		public bool UpdateAuthorizationInformation(AuthorizationInformation authorization)
		{
			return this.adaptor.UpdateAuthorizationInformation(authorization);
		}

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public List<EmailInRole> RetrieveUserRoles(string email)
		{
			return this.adaptor.RetrieveUserRoles(email);
		}

		/// <summary>
		/// Creates the email in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool CreateEmailInRole(EmailInRole emailInRole)
		{
			return this.adaptor.CreateEmailInRole(emailInRole);
		}

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public Roles RetrieveRoles()
		{
			return this.adaptor.RetrieveRoles();
		}

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public Role RetrieveRole(int roleId)
		{
			return this.adaptor.RetrieveRole(roleId);
		}

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool UpdateRole(Role role)
		{
			return this.adaptor.UpdateRole(role);
		}

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool RemoveUserInRole(EmailInRole emailInRole)
		{
			return this.adaptor.RemoveUserInRole(emailInRole);
		}

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool CreateRole(Role role)
		{
			return this.adaptor.CreateRole(role);
		}

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		public Users RetrieveUsers()
		{
			return this.adaptor.RetrieveUsers();
		}
	}
}
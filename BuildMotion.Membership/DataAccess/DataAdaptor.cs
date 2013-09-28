using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

namespace BuildMotion.Membership.DataAccess
{
	public class DataAdaptor : IDataAdaptor
	{
		private readonly DataAccessContextBase dataAccessContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAdaptor"/> class.
		/// </summary>
		/// <param name="dataAccessContext">The data access context.</param>
		/// <exception cref="System.ArgumentNullException">dataAccessContext</exception>
		public DataAdaptor(DataAccessContextBase dataAccessContext)
		{
			if (dataAccessContext == null)
			{
				throw new ArgumentNullException("dataAccessContext");
			}
			this.dataAccessContext = dataAccessContext;
		}

		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public UserInformation RetrieveUserInformation(string email)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RetrieveUserInformation(email);
		}

		/// <summary>
		/// Creates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public UserInformation CreateUserInformation(UserInformation userInfo)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.CreateUserInformation(userInfo);
		}

		/// <summary>
		/// Creates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		public bool CreateAuthorizationInformation(AuthorizationInformation authorization)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.CreateAuthorizationInformation(authorization);
		}

		/// <summary>
		/// Updates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public UserInformation UpdateUserInformation(UserInformation userInfo)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.UpdateUserInformation(userInfo);
		}

		/// <summary>
		/// Updates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <returns></returns>
		public bool UpdateAuthorizationInformation(AuthorizationInformation authorization)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.UpdateAuthorizationInformation(authorization);
		}

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public List<EmailInRole> RetrieveUserRoles(string email)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RetrieveUserRoles(email);
		}

		/// <summary>
		/// Creates the email in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool CreateEmailInRole(EmailInRole emailInRole)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.CreateEmailInRole(emailInRole);
		}

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public Roles RetrieveRoles()
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RetrieveRoles();
		}

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public Role RetrieveRole(int roleId)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RetrieveRole(roleId);
		}

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool UpdateRole(Role role)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.UpdateRole(role);
		}

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public bool RemoveUserInRole(EmailInRole emailInRole)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RemoveUserInRole(emailInRole);
		}

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public bool CreateRole(Role role)
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.CreateRole(role);
		}

		/// <summary>
		/// Retrieves the users.
		/// </summary>
		/// <returns></returns>
		public Users RetrieveUsers()
		{
			return this.dataAccessContext.EntityFrameworkDataAccess.RetrieveUsers();
		}
	}
}

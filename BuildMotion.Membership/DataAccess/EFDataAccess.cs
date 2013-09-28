#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.DataAccess
{
	public class EFDataAccess : DataAccessBase
	{
		private string connectionStringName;
		private BuildMotionDb dbContext;

		/// <summary>
		///     Initializes a new instance of the <see cref="EFDataAccess" /> class.
		/// </summary>
		public EFDataAccess()
		{
		}

		/// <summary>
		///     Gets or sets the name of the connection string.
		/// </summary>
		/// <value>
		///     The name of the connection string.
		/// </value>
		public override string ConnectionStringName
		{
			get
			{
				return this.connectionStringName;
			}
			set
			{
				this.connectionStringName = value;
			}
		}

		/// <summary>
		/// Retrieves the user information.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public override UserInformation RetrieveUserInformation(string email)
		{
			UserInformation info = null;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				info = this.dbContext.UserInfos.Include(a => a.Authorization).FirstOrDefault(e => e.Email == email);
			}
			return info;
		}

		/// <summary>
		/// Creates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public override UserInformation CreateUserInformation(UserInformation userInfo)
		{
			UserInformation userInformation = null;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				//add and save;
				this.dbContext.UserInfos.Add(userInfo);
				int result = this.dbContext.SaveChanges();
				//retrieve;
				userInformation = this.dbContext.UserInfos.Find(userInfo.Email);
			}
			return userInformation;
		}

		/// <summary>
		/// Creates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <returns></returns>
		public override bool CreateAuthorizationInformation(AuthorizationInformation authorization)
		{
			bool isCreated = false;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.AuthorizationStates.Add(authorization);
				int result = this.dbContext.SaveChanges();
				isCreated = result > 0;
			}
			return isCreated;
		}

		/// <summary>
		/// Updates the user information.
		/// </summary>
		/// <param name="userInfo">The user info.</param>
		/// <returns></returns>
		public override UserInformation UpdateUserInformation(UserInformation userInfo)
		{
			UserInformation userInformation = null;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.UserInfos.Attach(userInfo);
				this.dbContext.Entry(userInfo).State = EntityState.Modified;
				int result = this.dbContext.SaveChanges();

				userInformation = this.RetrieveUserInformation(userInfo.Email);
			}
			return userInformation;
		}

		/// <summary>
		/// Updates the authorization information.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <returns></returns>
		public override bool UpdateAuthorizationInformation(AuthorizationInformation authorization)
		{
			bool isUpdated = false;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.AuthorizationStates.Attach(authorization);
				this.dbContext.Entry(authorization).State = EntityState.Modified;
				int result = this.dbContext.SaveChanges();
				isUpdated = result > 0;
			}
			return isUpdated;
		}

		/// <summary>
		/// Retrieves the user roles.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public override List<EmailInRole> RetrieveUserRoles(string email)
		{
			List<EmailInRole> roles = new List<EmailInRole>();
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				roles.AddRange(this.dbContext.EmailInRoles.Include(r => r.Role).Where(e => e.Email == email).ToList());
			}
			return roles;
		}

		/// <summary>
		/// Creates the email in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public override bool CreateEmailInRole(EmailInRole emailInRole)
		{
			bool isCreated = false;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.EmailInRoles.Add(emailInRole);
				int result = this.dbContext.SaveChanges();
				isCreated = result > 0;
			}
			return isCreated;
		}

		/// <summary>
		/// Retrieves the roles.
		/// </summary>
		/// <returns></returns>
		public override Roles RetrieveRoles()
		{
			Roles roles = new Roles();
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				roles.AddRange(this.dbContext.Roles);
			}
			return roles;
		}

		/// <summary>
		/// Retrieves the role.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <returns></returns>
		public override Role RetrieveRole(int roleId)
		{
			Role role = null;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				role = this.dbContext.Roles.Find(roleId);
			}
			return role;
		}

		/// <summary>
		/// Updates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public override bool UpdateRole(Role role)
		{
			bool isUpdated = false;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.Roles.Attach(role);
				this.dbContext.Entry(role).State = EntityState.Modified;
				int result = this.dbContext.SaveChanges();
				isUpdated = result > 0;
			}
			return isUpdated;
		}

		/// <summary>
		/// Removes the user in role.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <returns></returns>
		public override bool RemoveUserInRole(EmailInRole emailInRole)
		{
			bool isRemoved = false;
			using (dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.EmailInRoles.Attach(emailInRole);
				this.dbContext.Entry(emailInRole).State = EntityState.Deleted;
				int result = this.dbContext.SaveChanges();
				isRemoved = result > 0;
			}
			return isRemoved;
		}

		/// <summary>
		/// Creates the role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <returns></returns>
		public override bool CreateRole(Role role)
		{
			bool isCreated = false;
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				this.dbContext.Roles.Add(role);
				int result = this.dbContext.SaveChanges();
				isCreated = result > 0;
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
			using (this.dbContext = new BuildMotionDb(this.connectionStringName))
			{
				users.AddRange(this.dbContext.UserInfos);
			}
			return users;
		}
	}
}
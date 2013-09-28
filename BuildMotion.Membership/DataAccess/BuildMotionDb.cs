﻿#region

using System.Data.Entity;
using BuildMotion.Membership.DataAccess.Configurations;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.DataAccess
{
	public class BuildMotionDb : DbContext
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="BuildMotionDb" /> class.
		/// </summary>
		/// <param name="connectionStringName">Name of the connection string.</param>
		public BuildMotionDb(string connectionStringName) : base(connectionStringName)
		{
		}

		#region Google

		public DbSet<UserInformation> UserInfos{ get; set; }
		public DbSet<AuthorizationInformation> AuthorizationStates{ get; set; }
		public DbSet<EmailInRole> EmailInRoles{ get; set; }
		public DbSet<Role> Roles{ get; set; }

		#endregion

		/// <summary>
		///     This method is called when the model for a derived context has been initialized, but
		///     before the model has been locked down and used to initialize the context.  The default
		///     implementation of this method does nothing, but it can be overridden in a derived
		///     such that the model can be further configured before it is locked down.
		/// </summary>
		/// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
		/// <remarks>
		///     Typically, this method is called only once when the first instance of a derived context
		///     is created.  The model for that context is then cached and is for all further instances of
		///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
		///     property on the given ModelBuidler, but note that this can seriously degrade performance.
		///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
		///     classes directly.
		/// </remarks>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			#region Google Authentication
			modelBuilder.Configurations.Add(new UserInfoConfiguration());
			modelBuilder.Configurations.Add(new AuthorizationStateConfiguration());
			modelBuilder.Configurations.Add(new EmailInRoleConfiguration());

			#endregion;

			//REMOVED: not required if adding configurations; 
			//base.OnModelCreating(modelBuilder);
		}
	}
}
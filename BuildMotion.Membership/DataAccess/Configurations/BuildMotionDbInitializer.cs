#region

using System;
using System.Data.Entity;
using BuildMotion.Membership.Entity;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.DataAccess.Configurations
{
	public class BuildMotionDbInitializer : DropCreateDatabaseAlways<BuildMotionDb>
	{
		protected override void Seed(BuildMotionDb context)
		{
			#region Default Google Membership/Roles

			context.Roles.Add(new Role
			{
				RoleId = 1
				,Name = "Admin"
				,Description = "Only allow administrators access to resources with this role."
				,IsActive = true
			});

			context.Roles.Add(new Role
			{
				RoleId = 2
				,Name = "User"
				,Description = "Allow general access to resources with the role."
				,IsActive = true
			});

			// add all of the item(s) in the context to the data store;
			base.Seed(context);
			#endregion;

			#region Create Sample User Roles

			// Admin;
			context.EmailInRoles.Add(new EmailInRole
			{
				Email = "matt.vaughn@buildmotion.com"
				,RoleId = 1 //admin
			});

			#endregion;

			// add all of the item(s) in the context to the data store;
			base.Seed(context);
		}
	}
}
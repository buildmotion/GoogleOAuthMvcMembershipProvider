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
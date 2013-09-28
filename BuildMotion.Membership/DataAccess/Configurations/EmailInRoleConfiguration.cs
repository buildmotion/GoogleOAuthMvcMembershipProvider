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

using System.Data.Entity.ModelConfiguration;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.DataAccess.Configurations
{
	internal class EmailInRoleConfiguration : EntityTypeConfiguration<EmailInRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmailInRoleConfiguration"/> class.
		/// </summary>
		public EmailInRoleConfiguration()
		{
			// entity: configuration for composite key;
			this.HasKey(k => new{
				k.RoleId,
				k.Email
			});

			// property
			this.Property(p => p.RoleId).IsRequired();
			this.Property(p => p.Email).IsRequired();

			// relationships;
			//this.HasRequired(r => r.Role).WithOptional();
		}
	}
}
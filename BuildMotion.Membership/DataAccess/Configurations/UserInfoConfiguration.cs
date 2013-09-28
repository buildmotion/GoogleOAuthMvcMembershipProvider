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
	public class UserInfoConfiguration : EntityTypeConfiguration<UserInformation>
	{
		public UserInfoConfiguration()
		{
			//Entity
			this.ToTable("UserInformation");
			this.HasKey(k => k.Email);

			//Property
			this.Property(e => e.Email).IsRequired().HasMaxLength(80);
			this.Property(g => g.GoogleId).IsRequired().HasMaxLength(80);
			this.Property(f => f.FirstName).HasMaxLength(40);
			this.Property(l => l.LastName).HasMaxLength(40);
			this.Property(fn => fn.FullName).HasMaxLength(81);
			this.Property(d => d.Domain).HasMaxLength(80);

			//Relationships
			//this.HasRequired(a => a.Authorization).WithMany().HasForeignKey(a => a.AuthorizationStateId);
			this.HasRequired(a => a.Authorization).WithRequiredPrincipal();
		}
	}
}
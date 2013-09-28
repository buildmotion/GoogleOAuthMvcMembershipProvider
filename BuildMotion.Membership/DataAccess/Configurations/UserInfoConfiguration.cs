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
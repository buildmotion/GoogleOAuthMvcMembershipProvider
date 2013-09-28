#region

using System.Data.Entity.ModelConfiguration;
using BuildMotion.Membership.Entity.Google;
using System.Linq;
#endregion

namespace BuildMotion.Membership.DataAccess.Configurations
{
	public class AuthorizationStateConfiguration : EntityTypeConfiguration<AuthorizationInformation>
	{
		public AuthorizationStateConfiguration()
		{
			//Entity
			this.HasKey(i => i.Email);

			//Property
			this.Property(a => a.AccessToken).IsRequired().HasMaxLength(120);
			this.Property(r => r.RefreshToken).IsRequired().HasMaxLength(120);
			this.Property(e => e.Email).IsRequired().HasMaxLength(120);

			//Relationships;
		}
	}
}
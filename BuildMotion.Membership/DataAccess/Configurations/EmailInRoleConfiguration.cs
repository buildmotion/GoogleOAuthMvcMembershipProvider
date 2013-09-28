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
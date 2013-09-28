#region

using System.Data.Entity.ModelConfiguration;
using BuildMotion.Membership.Entity.Google;

#endregion

namespace BuildMotion.Membership.DataAccess.Configurations
{
	internal class RoleConfiguration : EntityTypeConfiguration<Role>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="RoleConfiguration" /> class.
		/// </summary>
		public RoleConfiguration()
		{
			this.HasKey(k => k.RoleId);

			this.Property(p => p.Name).IsRequired().HasMaxLength(25);
			this.Property(d => d.Description).HasMaxLength(200);
		}
	}
}
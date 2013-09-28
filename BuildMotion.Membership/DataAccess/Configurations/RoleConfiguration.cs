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
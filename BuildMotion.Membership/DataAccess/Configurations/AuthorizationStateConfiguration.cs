﻿//------------------------------------------------------------------------------
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
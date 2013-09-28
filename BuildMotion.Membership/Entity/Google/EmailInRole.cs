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

using System.Text;

namespace BuildMotion.Membership.Entity.Google
{
	/// <summary>
	///     Use to associate a specified email address (user) to a role.
	/// </summary>
	public class EmailInRole
	{
		/// <summary>
		///     Gets or sets the role id.
		/// </summary>
		/// <value>
		///     The role id.
		/// </value>
		public int RoleId{ get; set; }

		/// <summary>
		///     Gets or sets the email.
		/// </summary>
		/// <value>
		///     The email.
		/// </value>
		public string Email{ get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		public Role Role{ get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Email: {0}{1}", this.Email, "\n\t");
			sb.AppendFormat("{0}", this.Role.ToString());
			return sb.ToString();
		}
	}
}
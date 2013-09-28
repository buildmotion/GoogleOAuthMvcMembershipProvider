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
using System.Collections.Generic;
using System.Text;

namespace BuildMotion.Membership.Entity.Google
{
	public class Role
	{
		/// <summary>
		/// Gets or sets the role id.
		/// </summary>
		/// <value>
		/// The role id.
		/// </value>
		public int RoleId{ get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name{ get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description{ get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive{ get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("RoleId: {0}{1}", this.RoleId, "\n\t");
			sb.AppendFormat("Name: {0}{1}", this.Name, "\n\t");
			sb.AppendFormat("Description: {0}{1}", this.Description, "\n\t");
			sb.AppendFormat("IsActive: {0}{1}", this.IsActive, "\n\t");
			return sb.ToString();
		}

	}
}
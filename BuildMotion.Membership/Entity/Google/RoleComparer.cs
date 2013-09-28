using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMotion.Membership.Entity.Google
{
	public class RoleComparer : EqualityComparer<Role>
	{

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public override bool Equals(Role x, Role y)
		{
			if(x.RoleId == y.RoleId
				& x.Name == y.Name
				& x.Description == y.Description
				& x.IsActive == y.IsActive)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode(Role obj)
		{
			int hashCode = 0;
			if(obj != null)
			{
				hashCode = obj.RoleId;
			}
			return hashCode.GetHashCode();
		}
	}
}

namespace BuildMotion.Membership.DataAccess
{
	public abstract class DataAccessContextBase
	{
		/// <summary>
		/// Gets the db access.
		/// </summary>
		/// <value>
		/// The db access.
		/// </value>
		public abstract DataAccessBase EntityFrameworkDataAccess{ get; }
	}
}
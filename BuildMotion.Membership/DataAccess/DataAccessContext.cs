namespace BuildMotion.Membership.DataAccess
{
	public class DataAccessContext : DataAccessContextBase
	{
		private readonly DataAccessBase entityFrameworkDataAccess;

		/// <summary>
		///     Initializes a new instance of the <see cref="DataAccessContext" /> class.
		/// </summary>
		/// <param name="efDataAccess">The ef data access.</param>
		public DataAccessContext(DataAccessBase efDataAccess)
		{
			this.entityFrameworkDataAccess = efDataAccess;
		}

		/// <summary>
		///     Gets the db access.
		/// </summary>
		/// <value>
		///     The db access.
		/// </value>
		public override DataAccessBase EntityFrameworkDataAccess
		{
			get
			{
				return this.entityFrameworkDataAccess;
			}
		}
	}
}
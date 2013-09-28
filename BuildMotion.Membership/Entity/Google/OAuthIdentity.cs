#region

using System.Security.Principal;

#endregion

namespace BuildMotion.Membership.Entity.Google
{
	/// <summary>
	/// Use to contain identification iformation for the specified user. 
	/// </summary>
	public class OAuthIdentity : IIdentity
	{
		private readonly string name;

		/// <summary>
		///     Initializes a new instance of the <see cref="OAuthIdentity" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="isAuthenticated">
		///     if set to <c>true</c> [is authenticated].
		/// </param>
		/// <param name="authenticationType">Type of the authentication.</param>
		public OAuthIdentity(string name, bool isAuthenticated, string authenticationType)
		{
			this.Name = name;
			this.AuthenticationType = authenticationType;
			this.IsAuthenticated = isAuthenticated;
		}

		public string Name{ get; private set; }
		public string AuthenticationType{ get; private set; }
		public bool IsAuthenticated{ get; private set; }
	}
}
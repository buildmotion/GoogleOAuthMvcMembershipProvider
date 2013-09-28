
#region

using System;
using DotNetOpenAuth.OAuth2;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class GetAuthServerDescriptionAction : ActionBase
    {
		private AuthorizationServerDescription authServerDescription;

	    /// <summary>
		/// Initializes a new instance of the <see cref="GetAuthServerDescriptionAction"/> class.
		/// </summary>
		/// <param name="membershipProvider">The build motion provider.</param>
	    public GetAuthServerDescriptionAction(MembershipProvider membershipProvider) : base(membershipProvider)
	    {
	    }

		/// <summary>
		/// Gets the authorization server description.
		/// </summary>
		/// <value>
		/// The authorization server description.
		/// </value>
	    public AuthorizationServerDescription AuthorizationServerDescription
	    {
		    get
		    {
				return authServerDescription;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			authServerDescription = new AuthorizationServerDescription();
			// Add the access_type parameter sot that the RefreshToken is returned along with the AccessTokein
			authServerDescription.AuthorizationEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/auth?access_type=offline&approval_prompt=force");
			authServerDescription.TokenEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/token");
			authServerDescription.ProtocolVersion = ProtocolVersion.V20;
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = authServerDescription != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}

#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveCurrentAccessTokenAction : ActionBase
    {
		[StringIsNotEmptySpace("emailAddressIsValid", "The email address is not valid.")]
	    private readonly string emailAddress;
	    private string accessToken;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveCurrentAccessTokenAction"/> class.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public RetrieveCurrentAccessTokenAction(string emailAddress, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.emailAddress = emailAddress;
	    }

		/// <summary>
		/// Gets the access token.
		/// </summary>
		/// <value>
		/// The access token.
		/// </value>
	    public string AccessToken
	    {
		    get
		    {
			    return accessToken;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			// need to retrieve a valid Token: 
			UserInformation user = this.Provider.RetrieveUserInformation(this.emailAddress);
			if(DateTime.UtcNow > user.Authorization.AccessTokenExpirationUtc)
			{
				// need to retrieve updated access token;
				accessToken = this.Provider.RefreshAccessToken(user);
			}
			else
			{
				accessToken = user.Authorization.AccessToken;
			}
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = !string.IsNullOrEmpty(this.accessToken) ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
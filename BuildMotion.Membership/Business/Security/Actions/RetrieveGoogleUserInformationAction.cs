
#region

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveGoogleUserInformationAction : ActionBase
    {
		[StringIsNotEmptySpace("AccessTokenIsValid", "The access token is not valid. Cannot retrieve Google user information.")]
	    private readonly string accessToken;
	    private UserInformation userInformation;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveGoogleUserInformationAction"/> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <param name="provider">The provider.</param>
		/// <exception cref="System.NotImplementedException"></exception>
	    public RetrieveGoogleUserInformationAction(string accessToken, MembershipProviderBase provider) : base(provider)
		{
			this.accessToken = accessToken;
		}

	    public UserInformation UserInformation
	    {
		    get
		    {
			    return userInformation;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			var userInfoUrl = "https://www.googleapis.com/oauth2/v1/userinfo";
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
			var response = hc.GetAsync(userInfoUrl).Result;
			dynamic userInfo = response.Content.ReadAsAsync<dynamic>().Result;

		    if (userInfo != null)
		    {
				this.userInformation = new UserInformation
				{
					Id = Guid.NewGuid(),
					Email = userInfo.email,
					FullName= userInfo.name,
					FirstName = userInfo.given_name,
					LastName = userInfo.family_name,
					Domain = userInfo.hd,
					IsVerifiedEmail = userInfo.verified_email,
					Link = userInfo.link,
					GoogleId = userInfo.id
				};
		    }
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.userInformation != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
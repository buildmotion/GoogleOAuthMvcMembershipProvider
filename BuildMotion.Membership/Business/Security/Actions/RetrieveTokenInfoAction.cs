
#region

using System;
using System.Net.Http;
using System.Web.Script.Serialization;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveTokenInfoAction : ActionBase
    {
		[StringIsNotEmptySpace("AccessTokenIsValid", "The access token is not valid.")]
	    private readonly string accessToken;
	    private TokenInfo tokenInfo;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveTokenInfoAction"/> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <param name="membershipProvider">The build motion provider.</param>
	    public RetrieveTokenInfoAction(string accessToken, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.accessToken = accessToken;
	    }

		/// <summary>
		/// Gets the token info.
		/// </summary>
		/// <value>
		/// The token info.
		/// </value>
	    public TokenInfo TokenInfo
	    {
		    get
		    {
			    return tokenInfo;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			var verificationUri = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=" + accessToken;
			var hc = new HttpClient();
			var response = hc.GetAsync(verificationUri).Result;
			dynamic result = response.Content.ReadAsAsync<dynamic>().Result;

			// serialize the result into a TokenInfo object; 
		    this.tokenInfo = new JavaScriptSerializer().Deserialize<TokenInfo>(result.ToString());
        }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.tokenInfo != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
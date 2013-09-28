
#region

using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class ValidateTokenAction : ActionBase
    {
		[StringIsNotEmptySpace("AccessTokenIsValid", "The access token cannot be null or empty string.")]
	    private readonly string accessToken;
	    private bool isValid = true; //let's assume the token is valid;

	    /// <summary>
		/// Initializes a new instance of the <see cref="ValidateTokenAction"/> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <param name="provider">The provider.</param>
	    public ValidateTokenAction(string accessToken, MembershipProviderBase provider) : base(provider)
	    {
		    this.accessToken = accessToken;
	    }

		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
	    public bool IsValid
	    {
		    get
		    {
			    return isValid;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
        {
			#region Validate the user's AuthToken

			dynamic tokenInfo = this.Provider.GetTokenInfo(this.accessToken);
		    
			var audience = tokenInfo.audience.ToString();
			if(string.IsNullOrEmpty(audience) || audience != this.Provider.GoogleClientId)
			{
				this.isValid = false;
			}

			if(tokenInfo.expires_in == null)
			{
				this.isValid = false;
			}
			var expiresIn = tokenInfo.expires_in.ToString();
			int intExpiresIn;
			var isInt = int.TryParse(expiresIn, out intExpiresIn);

			/* the ValidateToken method checks the expires_in property. If its value is 
			 * equal to or smaller than zero, the token is no longer valid, 
			 * which also leads to an exception
			 * 
			 * The [expires_in] value must be a valid integer greater than 0. Usually it is a
			 * value of [3600] with the unit being a second. 
			 */
			if(!isInt || intExpiresIn <= 0)
			{
				this.isValid = false;
			}
			#endregion;
		}

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
		{
			this.isValid = this.ValidationContext.IsValid;
			this.Result = this.isValid ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
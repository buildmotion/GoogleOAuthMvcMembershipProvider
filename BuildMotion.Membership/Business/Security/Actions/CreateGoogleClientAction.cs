
#region

using System;
using System.Configuration;
using DotNetOpenAuth.OAuth2;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class CreateGoogleClientAction : ActionBase
    {
		[IsNotNull("AuthorizationServerDescriptionIsNotNull", "The AuthorizationServerDescription cannot be null.")]
	    private readonly AuthorizationServerDescription authorizationDescription;
	    private WebServerClient client;
	    [StringIsNotEmptySpace("ClientIdIsValid", "The client id cannot be empty or null string.")]
		private readonly string clientId;
		[StringIsNotEmptySpace("ClientSecretIsValid", "The client secret cannot be empty or null string.")]
		private readonly string clientSecret;

	    /// <summary>
	    /// Initializes a new instance of the <see cref="CreateGoogleClientAction"/> class.
	    /// </summary>
	    /// <param name="authorizationDescription"></param>
	    /// <param name="membershipProvider">The build motion provider.</param>
	    public CreateGoogleClientAction(AuthorizationServerDescription authorizationDescription, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.authorizationDescription = authorizationDescription;
			//PATTERN: setup in constructor to allow for action validation on fields/properties;
		    this.clientId = this.Provider.GoogleClientId;
		    this.clientSecret = this.Provider.GoogleClientSecret;
	    }

	    /// <summary>
		/// Gets the client.
		/// </summary>
		/// <value>
		/// The client.
		/// </value>
	    public WebServerClient Client
	    {
		    get
		    {
			    return client;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			this.client = new WebServerClient(this.authorizationDescription, this.clientId); // client Id
			client.ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(this.clientSecret); // client secret;
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.client != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
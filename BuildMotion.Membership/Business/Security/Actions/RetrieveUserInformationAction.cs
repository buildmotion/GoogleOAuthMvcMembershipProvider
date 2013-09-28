
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveUserInformationAction : ActionBase
    {
	    [StringIsNotEmptySpace("EmailAddressIsValid", "The email address is not valid. Cannot be empty or null string.")]
		private readonly string emailAddress;
	    private UserInformation userInformation;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveUserInformationAction"/> class.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <param name="provider">The provider.</param>
	    public RetrieveUserInformationAction(string emailAddress, MembershipProviderBase provider) : base(provider)
	    {
		    this.emailAddress = emailAddress;
	    }

		/// <summary>
		/// Gets the user information.
		/// </summary>
		/// <value>
		/// The user information.
		/// </value>
	    public UserInformation UserInformation
	    {
		    get
		    {
			    return userInformation;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
		    this.userInformation = this.Repository.RetrieveUserInformation(this.emailAddress);
	    }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.userInformation != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
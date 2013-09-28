
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveEmailInRolesAction : ActionBase
    {
	    private readonly string emailAddress;
	    private EmailInRoles userRoles = new EmailInRoles();

		/// <summary>
		/// Initializes a new instance of the <see cref="RetrieveEmailInRolesAction"/> class.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <param name="provider">The provider.</param>
	    public RetrieveEmailInRolesAction(string emailAddress, MembershipProviderBase provider) : base(provider)
	    {
		    this.emailAddress = emailAddress;
	    }

	    /// <summary>
		/// Gets the user roles.
		/// </summary>
		/// <value>
		/// The user roles.
		/// </value>
	    public EmailInRoles UserRoles
	    {
		    get
		    {
			    return userRoles;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
			this.userRoles.AddRange(this.Repository.RetrieveUserRoles(this.emailAddress));
	    }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = ActionResult.Success;
	        return Result;
        }
	}
}
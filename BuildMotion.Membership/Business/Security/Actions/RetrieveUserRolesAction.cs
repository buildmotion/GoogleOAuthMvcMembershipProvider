
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveUserRolesAction : ActionBase
    {
	    private readonly string email;
		private EmailInRoles userRoles = new EmailInRoles();

		/// <summary>
		/// Initializes a new instance of the <see cref="RetrieveUserRolesAction"/> class.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="provider">The provider.</param>
	    public RetrieveUserRolesAction(string email, MembershipProviderBase provider) : base(provider)
	    {
		    this.email = email;
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
			this.userRoles.AddRange(this.Repository.RetrieveUserRoles(this.email));
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
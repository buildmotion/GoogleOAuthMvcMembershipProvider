
#region

using System;
using System.Collections.Generic;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveRolesAction : ActionBase
    {
	    private Roles applicationRoles;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveRolesAction"/> class.
		/// </summary>
		/// <param name="membershipProvider">The membership provider.</param>
	    public RetrieveRolesAction(MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    
	    }

		/// <summary>
		/// Gets the application roles.
		/// </summary>
		/// <value>
		/// The application roles.
		/// </value>
	    public Roles ApplicationRoles
	    {
		    get
		    {
			    return this.applicationRoles;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
		    this.applicationRoles = this.Repository.RetrieveRoles();
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.applicationRoles != null && this.applicationRoles.Count > 0 ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
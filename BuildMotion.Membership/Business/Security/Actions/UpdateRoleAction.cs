
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class UpdateRoleAction : ActionBase
    {
	    private readonly Role role;
	    private bool isUpdated;

	    /// <summary>
		/// Initializes a new instance of the <see cref="UpdateRoleAction"/> class.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public UpdateRoleAction(Role role, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.role = role;
	    }

		/// <summary>
		/// Gets a value indicating whether this instance is updated.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is updated; otherwise, <c>false</c>.
		/// </value>
	    public bool IsUpdated
	    {
		    get
		    {
			    return isUpdated;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
		    this.isUpdated = this.Repository.UpdateRole(this.role);
	    }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.isUpdated ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
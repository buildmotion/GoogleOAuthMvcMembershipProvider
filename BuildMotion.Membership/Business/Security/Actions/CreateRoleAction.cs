
#region

using System;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class CreateRoleAction : ActionBase
    {
		[RoleIsValid("RoleIsValid", "The specified role is not valid.")]
	    private readonly Role role;
	    private bool isCreated;

	    /// <summary>
		/// Initializes a new instance of the <see cref="CreateRoleAction"/> class.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public CreateRoleAction(Role role, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.role = role;
	    }

		/// <summary>
		/// Gets a value indicating whether this instance is created.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is created; otherwise, <c>false</c>.
		/// </value>
	    public bool IsCreated
	    {
		    get
		    {
			    return isCreated;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
		    this.isCreated = this.Repository.CreateRole(role);
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.isCreated ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
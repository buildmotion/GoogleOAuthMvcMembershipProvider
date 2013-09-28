
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveRoleAction : ActionBase
    {
		[Range("RoleIdIsValid", "The roleId value is not valid. Must be 1 or greater.", 1, int.MaxValue)]
	    private readonly int roleId;
	    private Role role;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveRoleAction" /> class.
		/// </summary>
		/// <param name="roleId">The role id.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public RetrieveRoleAction(int roleId, MembershipProviderBase membershipProvider) : base(membershipProvider)
		{
			this.roleId = roleId;
		}

		/// <summary>
		/// Gets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
	    public Role Role
	    {
		    get
		    {
			    return role;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
		    this.role = this.Repository.RetrieveRole(this.roleId);
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.role != null ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
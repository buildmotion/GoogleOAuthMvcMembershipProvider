
#region

using System;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class AddUserToRoleAction : ActionBase
    {
		[EmailInRoleIsValid("EmailInRoleIsValid", "The email and role information is not valid.")]
	    private readonly EmailInRole emailInRole;
	    private bool isAdded;

	    /// <summary>
		/// Initializes a new instance of the <see cref="AddUserToRoleAction"/> class.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public AddUserToRoleAction(EmailInRole emailInRole, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.emailInRole = emailInRole;
	    }

		/// <summary>
		/// Gets a value indicating whether this instance is added.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is added; otherwise, <c>false</c>.
		/// </value>
	    public bool IsAdded
	    {
		    get
		    {
			    return isAdded;
		    }
	    }

		/// <summary>
		/// Does this instance.
		/// </summary>
        public override void PerformAction()
	    {
			EmailInRole addRole = new EmailInRole{
				Email = this.emailInRole.Email,
				RoleId = this.emailInRole.RoleId
			};
		    this.isAdded = this.Repository.CreateEmailInRole(addRole);
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.isAdded ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
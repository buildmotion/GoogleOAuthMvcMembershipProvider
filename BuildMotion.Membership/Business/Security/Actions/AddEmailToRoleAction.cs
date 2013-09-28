#region

using System;
using System.Collections.Generic;
using System.Linq;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class AddEmailToRoleAction : ActionBase
    {
	    private readonly string email;
	    private readonly int roleId;
	    private bool isAdded;

	    /// <summary>
		/// Initializes a new instance of the <see cref="AddEmailToRoleAction"/> class.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="roleId">The role id.</param>
		/// <param name="membershipProvider">The build motion provider.</param>
	    public AddEmailToRoleAction(string email, int roleId, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.email = email;
		    this.roleId = roleId;
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
			// retrieve all roles for the current user;
			List<EmailInRole> currentRoles = this.Repository.RetrieveUserRoles(this.email);
			
			// check for duplicate roles; only add distinct roles;
			var duplicate = (from cr in currentRoles
		                     where cr.RoleId == this.roleId
		                     select cr).FirstOrDefault();

		    if (duplicate == null)
		    {
				// add new role if not already exists;
				EmailInRole emailInRole = new EmailInRole{
					Email = this.email,
					RoleId = this.roleId
				};
				this.isAdded = this.Repository.CreateEmailInRole(emailInRole);
		    }
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = isAdded ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
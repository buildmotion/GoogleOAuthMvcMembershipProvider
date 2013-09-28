
#region

using System;
using System.Collections.Generic;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RemoveRolesFromUserAction : ActionBase
    {
		[IsNotNull("RemoveRolesIsNotNull", "The role(s) to remove is null.")]
	    private readonly List<Role> removeRoles;
		[UserInformationIsValid("UserInformationIsValidToRemoveRoles", "The user information is not valid. Cannot remove roles from user.")]
		private readonly UserInformation user;
	    private bool isRemoved = true;

	    public RemoveRolesFromUserAction(List<Role> removeRoles, UserInformation user, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.removeRoles = removeRoles;
		    this.user = user;
	    }

		/// <summary>
		/// Gets a value indicating whether this instance is removed.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is removed; otherwise, <c>false</c>.
		/// </value>
	    public bool IsRemoved
	    {
		    get
		    {
			    return isRemoved;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
			EmailInRoles roles = new EmailInRoles();
			this.removeRoles.ForEach(r => roles.Add(new EmailInRole
			{
				RoleId = r.RoleId,
				Email = this.user.Email
			}));

			foreach(EmailInRole emailInRole in roles)
			{
				isRemoved = this.isRemoved && this.Repository.RemoveUserInRole(emailInRole);
			}
        }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.isRemoved ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
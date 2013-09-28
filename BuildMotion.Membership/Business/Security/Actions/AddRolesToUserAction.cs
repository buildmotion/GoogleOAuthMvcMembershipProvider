
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
    internal class AddRolesToUserAction : ActionBase
    {
	    [IsNotNull("RoleListIsNotNull", "The role list is null. Cannot add roles to user.")]
		private readonly List<Role> addRoles;
		[UserInformationIsValid("UserInformationIsValidToAddRoles", "The user information is not valid. Cannot add roles to user.")]
		private readonly UserInformation user;
	    private bool isAdded = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="AddRolesToUserAction"/> class.
		/// </summary>
		/// <param name="addRoles">The add roles.</param>
		/// <param name="user">The user.</param>
		/// <param name="provider">The provider.</param>
	    public AddRolesToUserAction(List<Role> addRoles, UserInformation user, MembershipProviderBase provider) : base(provider)
	    {
		    this.addRoles = addRoles;
		    this.user = user;
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
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
        {
		    EmailInRoles roles = new EmailInRoles();
			this.addRoles.ForEach(r => roles.Add(new EmailInRole{
				RoleId = r.RoleId,
				Email = this.user.Email
			}));

		    foreach (EmailInRole emailInRole in roles)
		    {
			    isAdded = this.isAdded && this.Repository.CreateEmailInRole(emailInRole);
		    }
        }

        /// <summary>
        ///   Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
        /// </summary>
        /// <returns> </returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = isAdded ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}

#region

using System;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class UpdateUserAction : ActionBase
    {
		[UserInformationIsValid("UserInformationIsValidForUpdate", "The user information is not valid. Cannot update.")]
	    private readonly UserInformation user;
	    private bool isUpdated;

	    /// <summary>
		/// Initializes a new instance of the <see cref="UpdateUserAction"/> class.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public UpdateUserAction(UserInformation user, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.user = user;
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
		    UserInformation userInformation = this.Repository.UpdateUserInformation(this.user);
			if (userInformation != null)
			{
				this.isUpdated = true;
			}
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
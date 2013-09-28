
#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveUsersAction : ActionBase
    {
	    private Users users;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveUsersAction" /> class.
		/// </summary>
		/// <param name="membershipProvider">The membership provider.</param>
		/// <exception cref="System.NotImplementedException"></exception>
	    public RetrieveUsersAction(MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    
	    }

		/// <summary>
		/// Gets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>
	    public Users Users
	    {
		    get
		    {
			    return users;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
	    {
		    this.users = this.Repository.RetrieveUsers();
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
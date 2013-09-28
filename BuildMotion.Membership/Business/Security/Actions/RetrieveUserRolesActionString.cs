#region

using System;
using System.Collections.Generic;
using System.Text;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class RetrieveUserRolesActionString : ActionBase
    {
		[StringIsNotEmptySpace("EmailIsValid", "The email address value is not valid.")]
	    private readonly string email;
	    private string userRoles = string.Empty;

	    /// <summary>
		/// Initializes a new instance of the <see cref="RetrieveUserRolesActionString"/> class.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="membershipProvider">The build motion provider.</param>
	    public RetrieveUserRolesActionString(string email, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.email = email;
	    }

		/// <summary>
		/// Gets the user roles.
		/// </summary>
		/// <value>
		/// The user roles.
		/// </value>
	    public string UserRoles
	    {
		    get
		    {
			    return userRoles;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
	    {
		    UserInformation user = this.Provider.RetrieveUserInformation(this.email);
		    if (user.IsActive) //The user must be active in the membership database to retrieve roles; 
		    {
				List<EmailInRole> roleList = this.Repository.RetrieveUserRoles(this.email);
				if(roleList != null && roleList.Count > 0)
				{
					//create the "|" pipe delimited string;
					StringBuilder sb = new StringBuilder();
					bool useDelimiter = false;
					foreach(EmailInRole role in roleList)
					{
						if(role.Role.IsActive) //DO NOT ADD INACTIVE ROLES TO USER.
						{
							sb.AppendFormat("{1}{0}", role.Role.Name.ToLower(), useDelimiter ? "|" : string.Empty);
							useDelimiter = true;
						}
					}
					this.userRoles = sb.ToString();
				}
			}
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = !string.IsNullOrEmpty(this.userRoles) ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
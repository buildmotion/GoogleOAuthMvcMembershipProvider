
#region

using System;
using System.Web;
using System.Web.Security;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class CreateFormsAuthenticationCookieAction : ActionBase
    {
	    [UserInformationIsValid("UserDomainIsValid", "The user infomration is not valid. Cannot create authentication cookie.")]
		private readonly UserInformation user;
	    private HttpCookie cookie;
		[StringIsNotEmptySpace("RolesListIsValid", "The roles value cannot be null or empty string.")]
	    private string rolesUserDate;

	    /// <summary>
		/// Initializes a new instance of the <see cref="CreateFormsAuthenticationCookieAction" /> class.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="provider">The provider.</param>
		/// <exception cref="System.NotImplementedException"></exception>
	    public CreateFormsAuthenticationCookieAction(UserInformation user, MembershipProviderBase provider) : base(provider)
		{
			this.user = user;
			this.rolesUserDate = this.Provider.RetrieveUserRolesString(this.user.Email);
		}

		/// <summary>
		/// Gets the cookie. The return object of the action.
		/// </summary>
		/// <value>
		/// The cookie.
		/// </value>
	    public HttpCookie Cookie
	    {
		    get
		    {
			    return cookie;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
	    {
		    bool isPersistentCookie = this.Provider.IsPersistentCookie;
		    
			DateTime issueDate = DateTime.Now;
		    DateTime expiration = issueDate.AddHours(this.Provider.CookieExpirationInHours);
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Email, issueDate, expiration, isPersistentCookie, rolesUserDate, FormsAuthentication.FormsCookiePath);
			string encTicket = FormsAuthentication.Encrypt(ticket);
		    this.cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.cookie != null ?  ActionResult.Success: ActionResult.Fail;
	        return Result;
        }
	}
}
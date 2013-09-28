
#region

using System;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{ 
    internal class UserDomainIsValidAction : ActionBase
    {
		[StringIsNotEmptySpace("DomainStringIsValid", "The domain string value is not valid. Cannot validate domain.")]
	    private readonly string domain;
	    private bool domainIsValid;

	    /// <summary>
		/// Initializes a new instance of the <see cref="UserDomainIsValidAction"/> class.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="membershipProvider">The membership provider.</param>
	    public UserDomainIsValidAction(string domain, MembershipProviderBase membershipProvider) : base(membershipProvider)
	    {
		    this.domain = domain;
	    }

		/// <summary>
		/// Gets a value indicating whether [domain is valid].
		/// </summary>
		/// <value>
		///   <c>true</c> if [domain is valid]; otherwise, <c>false</c>.
		/// </value>
	    public bool DomainIsValid
	    {
		    get
		    {
			    return domainIsValid;
		    }
	    }

	    /// <summary>
        ///   Does this instance.
        /// </summary>
        public override void PerformAction()
	    {
		    this.domainIsValid = this.domain.ToLower().Equals(this.Provider.GoogleAppDomain.ToLower());
	    }

		/// <summary>
		/// Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
        protected override ActionResult ValidateActionResult()
        {
			this.Result = this.domainIsValid ? ActionResult.Success : ActionResult.Fail;
	        return Result;
        }
	}
}
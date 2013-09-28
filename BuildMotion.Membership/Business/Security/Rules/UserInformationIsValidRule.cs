using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Rules;

namespace BuildMotion.Membership.Business.Security.Rules
{ 
    internal class UserInformationIsValidRule : RuleComposite
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserInformationIsValidRule"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="message">The message.</param>
		/// <param name="target">The target.</param>
	    public UserInformationIsValidRule(string name, string message, UserInformation target) : base(name, message)
		{
			this.Rules.Add(new IsNotNullRule("UserInformationIsNotNull", "UserInformation is null.", target));
		    if (target != null)
		    {
				this.Rules.Add(new StringIsNotEmptySpace("EmailStringIsValid", "The email string value is not valid.", target.Email));
				//TODO: GET A BETTER reGEX;
				//this.Rules.Add(new RegularExpression("EmailAddressIsValid", "The email address is not valid.", target.Email, @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b"));
				this.Rules.Add(new StringIsNotEmptySpace("FullNameStringIsValid", "The full name value is not valid.", target.FullName));
				this.Rules.Add(new StringIsNotEmptySpace("GoogleIdStringIsValid", "The googleId value is not valid. Cannot be empty or null string.", target.GoogleId));
		    }
		}
	}
}
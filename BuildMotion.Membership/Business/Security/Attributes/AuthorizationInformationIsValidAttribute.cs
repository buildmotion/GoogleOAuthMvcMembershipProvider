#region

using BuildMotion.Membership.Business.Security.Rules;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Attributes
{ 
   public class AuthorizationInformationIsValidAttribute : ValidationAttribute
    {
        public AuthorizationInformationIsValidAttribute(string name, string failMessage) : base(name, failMessage)
        {
        }

        #region Overrides of ValidationAttribute

        /// <summary>
        ///   Creates the rule.
        /// </summary>
        /// <param name="target"> </param>
        /// <returns> </returns>
        public override RulePolicy CreateRule(object target)
        {
			Rule = new AuthorizationInformationIsValidRule(RuleName, FailMessage, (AuthorizationInformation)target);
            return Rule;
        }

        #endregion
    }
}
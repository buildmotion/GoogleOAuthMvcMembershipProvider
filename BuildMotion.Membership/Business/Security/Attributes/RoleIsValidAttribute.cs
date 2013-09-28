#region

using BuildMotion.Membership.Business.Security.Rules;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Attributes
{ 
   public class RoleIsValidAttribute : ValidationAttribute
    {
        public RoleIsValidAttribute(string name, string failMessage) : base(name, failMessage)
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
            Rule = new RoleIsValidRule(RuleName, FailMessage, (Role)target);
            return Rule;
        }

        #endregion
    }
}
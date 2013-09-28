//------------------------------------------------------------------------------
// <Vergosity.License>
//    All of the source code, logic, patterns, notes...really anything contained in the 
//		source code, compiled assemblies, or other mechanisms (i.e., drawings, diagrams, 
//		notes, or documentation) are the sole and explicit property of Build Motion, LLC.
//
//    You are entitled to use the compiled representations of the software only if they 
//		are licensed by either Vergosity or Build Motion, LLC. See "License.txt" in compiled
//		resource for details on license limitations and usage agreement.
// </Vergosity.License>
//------------------------------------------------------------------------------
#region

using BuildMotion.Membership.Business.Security.Rules;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Attributes
{ 
   public class UserInformationIsValidAttribute : ValidationAttribute
    {
        public UserInformationIsValidAttribute(string name, string failMessage) : base(name, failMessage)
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
			Rule = new UserInformationIsValidRule(RuleName, FailMessage, (UserInformation)target);
            return Rule;
        }

        #endregion
    }
}
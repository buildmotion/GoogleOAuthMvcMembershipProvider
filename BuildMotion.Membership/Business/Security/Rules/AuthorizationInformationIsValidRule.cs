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

using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Rules;

#endregion

namespace BuildMotion.Membership.Business.Security.Rules
{
	internal class AuthorizationInformationIsValidRule : RuleComposite
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="AuthorizationInformationIsValidRule" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="message">The message.</param>
		/// <param name="target">The target.</param>
		public AuthorizationInformationIsValidRule(string name, string message, AuthorizationInformation target)
			: base(name, message)
		{
			this.Rules.Add(new IsNotNullRule("AuthorizationInformationIsNotNull", "AuthorizationInformation cannot be null.", target));
			if (target != null)
			{
				this.Rules.Add(new StringIsNotEmptySpace("AccessTokenIsValid", "The AccessToken is not valid.", target.AccessToken));
				this.Rules.Add(new StringIsNotEmptySpace("RefreshTokenIsValid", "The RefreshToken token is not valid.", target.RefreshToken));
				this.Rules.Add(new StringIsNotEmptySpace("RefreshTokenIsValid", "The Email value is not valid.", target.Email));
			}
		}
	}
}
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
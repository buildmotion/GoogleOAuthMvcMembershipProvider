#region

using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Rules;

#endregion

namespace BuildMotion.Membership.Business.Security.Rules
{
	internal class EmailInRoleIsValidRule : RuleComposite
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EmailInRoleIsValidRule" /> class.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <param name="message"> The message. </param>
		/// <param name="target"></param>
		public EmailInRoleIsValidRule(string name, string message, EmailInRole target) : base(name, message)
		{
			this.RenderType = RenderType.EvaluateAllRules;

			this.Rules.Add(new IsNotNullRule("EmailInRoleIsNotNull", "The EmailInRole object cannot be null.", target));
			if (target != null)
			{
				this.Rules.Add(new Range<int>("RoleIdIsValid", "The roleId is not valid.", target.RoleId, 1, int.MaxValue));
				this.Rules.Add(new StringIsNotEmptySpace("EmailAddressIsValidString",
				                                         "The email address cannot be empty or null string.", target.Email));
			}
		}
	}
}
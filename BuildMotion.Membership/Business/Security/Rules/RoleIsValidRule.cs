#region

using System;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Validation;
using Vergosity.Validation.Rules;

#endregion

namespace BuildMotion.Membership.Business.Security.Rules
{
	internal class RoleIsValidRule : RuleComposite
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="RoleIsValidRule" /> class.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <param name="message"> The message. </param>
		/// <param name="target"></param>
		public RoleIsValidRule(string name, string message, Role target) : base(name, message)
		{
			this.Rules.Add(new IsNotNullRule("RoleIsNotNull", "The role cannot be null.", target));
			if (target != null)
			{
				this.Rules.Add(new StringIsNotNullEmptyRange("NameStringIsNotNullEmptyRange", "The name is not valid. Must be between 3 and 25 characters.", target.Name, 3, 25));
				this.Rules.Add(new StringIsNotNullEmptyRange("DescriptionStringIsNotNullEmptyRange", "The descriptions is not valid. Must be between 10 and 200 characters.", target.Description, 10, 200));
			}
		}
	}
}
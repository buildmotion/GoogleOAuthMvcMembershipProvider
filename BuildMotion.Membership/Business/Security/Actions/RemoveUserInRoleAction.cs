#region

using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{
	internal class RemoveUserInRoleAction : ActionBase
	{
		[Attributes.EmailInRoleIsValid("EmailInRoleIsValid", "The email and role information is not valid. Cannot remove user from role.")] 
		private readonly EmailInRole emailInRole;
		private bool isRemoved;

		/// <summary>
		///     Initializes a new instance of the <see cref="RemoveUserInRoleAction" /> class.
		/// </summary>
		/// <param name="emailInRole">The email in role.</param>
		/// <param name="membershipProvider">The membership provider.</param>
		public RemoveUserInRoleAction(EmailInRole emailInRole, MembershipProviderBase membershipProvider)
			: base(membershipProvider)
		{
			this.emailInRole = emailInRole;
		}

		/// <summary>
		///     Gets a value indicating whether this instance is removed.
		/// </summary>
		/// <value>
		///     <c>true</c> if this instance is removed; otherwise, <c>false</c>.
		/// </value>
		public bool IsRemoved
		{
			get
			{
				return isRemoved;
			}
		}

		/// <summary>
		///     Does this instance.
		/// </summary>
		public override void PerformAction()
		{
			this.isRemoved = this.Repository.RemoveUserInRole(this.emailInRole);
		}

		/// <summary>
		///     Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns></returns>
		protected override ActionResult ValidateActionResult()
		{
			this.Result = this.isRemoved ? ActionResult.Success : ActionResult.Fail;
			return Result;
		}
	}
}
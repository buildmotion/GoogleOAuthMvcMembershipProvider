#region

using BuildMotion.Membership.DataAccess;
using Vergosity.Actions;
using Vergosity.Validation;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{
	internal class ActionBase : Action
	{
		private readonly MembershipProviderBase provider;
		private readonly IRepository repository;
		private readonly ValidationContext validationContext = new ValidationContext();

		/// <summary>
		///     Initializes a new instance of the <see cref="ActionBase" /> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public ActionBase(MembershipProviderBase provider)
		{
			this.provider = provider;
			this.repository = provider.Repository;
		}

		/// <summary>
		///     Class implementors must override and implement this <see cref="ValidationContext" /> property.
		///     <see
		///         cref="ValidationContext" />
		///     is an abstract class, therefore, a sub-class that implements the abstract class will be needed.
		/// </summary>
		public override IValidationContext ValidationContext
		{
			get
			{
				return validationContext;
			}
		}
		
		/// <summary>
		///     Gets the provider.
		/// </summary>
		/// <value>
		///     The provider.
		/// </value>
		public MembershipProviderBase Provider
		{
			get
			{
				return provider;
			}
		}
		
		/// <summary>
		///     Gets the repository.
		/// </summary>
		/// <value>
		///     The repository.
		/// </value>
		public IRepository Repository
		{
			get
			{
				return repository;
			}
		}


		/// <summary>
		///     Use this method to validate the action. Validation may include any business rules, required data, and specific data formats.
		/// </summary>
		/// <returns> </returns>
		protected override IValidationContext ValidateAction()
		{
			return validationContext.RenderRules(validationContext.BuildRules(this));
		}
	}
}
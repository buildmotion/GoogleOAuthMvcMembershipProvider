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

using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{
	internal class CreateUpdateAuthorizationUserInfoAction : ActionBase
	{
		[AuthorizationInformationIsValid("AuthorizationInformationIsValid", "The authorization infomration is not valid. Access and Refresh tokens are required.")] 
		private readonly AuthorizationInformation authorization;
		[UserInformationIsValid("UserInformationIsValid", "The user information is not valid. Cannot save.")] 
		private readonly UserInformation userInfo;
		private UserInformation userInformation;

		/// <summary>
		///     Initializes a new instance of the <see cref="CreateUpdateAuthorizationUserInfoAction" /> class.
		/// </summary>
		/// <param name="authorization">The authorization.</param>
		/// <param name="userInfo">The user info.</param>
		/// <param name="provider">The provider.</param>
		public CreateUpdateAuthorizationUserInfoAction(AuthorizationInformation authorization, UserInformation userInfo,
		                                               MembershipProviderBase provider) : base(provider)
		{
			this.authorization = authorization;
			this.userInfo = userInfo;
		}

		/// <summary>
		///     Gets the user information.
		/// </summary>
		/// <value>
		///     The user information.
		/// </value>
		public UserInformation UserInformation
		{
			get
			{
				return userInformation;
			}
		}

		/// <summary>
		/// Does this instance.
		/// </summary>
		public override void PerformAction()
		{
			//Determine if [User Information] already exists;
			this.userInformation = this.Repository.RetrieveUserInformation(this.userInfo.Email);
			if (userInformation == null)
			{
				bool isDomainValid = true; // the default is to allow all domains if user authenticates with Google;
				if (this.Provider.RestrictUsersToGoogleAppDomain)
				{
					isDomainValid = this.Provider.UserDomainIsValid(this.userInfo.Domain);
				}

				if (isDomainValid)
				{
					// Create [User/Authorization] information;
					this.userInformation = this.Repository.CreateUserInformation(this.userInfo);
					// Create [Authorization] information;
					this.Repository.CreateAuthorizationInformation(this.authorization);
					// Add new user to default role
					int roleId = 2;//general user;
					bool isAdded = this.Provider.AddEmailToRole(this.userInformation.Email, roleId);
				}
			}
			else
			{
				// Update [User/Authorization] information;
				this.userInformation = this.Repository.UpdateUserInformation(this.userInfo);
				this.Repository.UpdateAuthorizationInformation(this.authorization);
			}
		}

		/// <summary>
		///     Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns> </returns>
		protected override ActionResult ValidateActionResult()
		{
			this.Result = this.userInformation != null ? ActionResult.Success : ActionResult.Fail;
			return Result;
		}
	}
}
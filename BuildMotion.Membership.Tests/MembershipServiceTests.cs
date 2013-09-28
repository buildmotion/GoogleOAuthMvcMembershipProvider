#region

using System;
using System.Web;
using BuildMotion.Membership.Business;
using BuildMotion.Membership.Entity.Google;
using DotNetOpenAuth.OAuth2;
using NUnit.Framework;

#endregion

namespace BuildMotion.Membership.Tests
{
	public class MembershipServiceTests
	{
		private IMembershipService membershipService;
		private readonly string emailAddress = "matt.vaughn@buildmotion.com";

		[SetUp]
		public void SetUp()
		{
			this.membershipService = Bootstrapper.Install();
			Assert.IsNotNull(this.membershipService);
		}

		/// <summary>
		/// Determines whether this instance [can create google client].
		/// </summary>
		[Test]
		public void CanCreateGoogleClient()
		{
			AuthorizationServerDescription serverDescription = this.membershipService.RetrieveAuthServerDescription();
			WebServerClient client = this.membershipService.CreateGoogleClient(serverDescription);
			Assert.IsNotNull(client);
		}

		/// <summary>
		/// Determines whether this instance [can retrieve user information].
		/// </summary>
		public void CanRetrieveUserInformation()
		{
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			Assert.IsNotNull(user);

			Assert.IsNotNullOrEmpty(user.Email);
			Assert.IsNotNullOrEmpty(user.Domain);
			Assert.IsNotNullOrEmpty(user.FirstName);
			Assert.IsNotNullOrEmpty(user.LastName);
			Assert.IsNotNullOrEmpty(user.FullName);
			Assert.IsNotNullOrEmpty(user.GoogleId);
			Assert.IsTrue(user.IsVerifiedEmail);
			Assert.IsNotNullOrEmpty(user.Id.ToString());
			Assert.AreNotEqual(Guid.Empty, user.Id);
		}
		
		[Test]
		public void CanCreateFormsAuthenticationCookie()
		{
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			Assert.IsNotNull(user);
			HttpCookie cookie = this.membershipService.CreateFormsAuthenticationCookie(user);
			Assert.IsNotNull(cookie);
		}

		/// <summary>
		/// Determines whether this instance [can retrieve current access token].
		/// </summary>
		[Test]
		public void CanRetrieveCurrentAccessToken()
		{
			string accessToken = this.membershipService.RetrieveCurrentAccessToken(this.emailAddress);
			Assert.IsNotNullOrEmpty(accessToken);
		}

		#region Role Service Tests;
		
		/// <summary>
		///     Determines whether this instance [can retrieve membership service].
		/// </summary>
		[Test]
		public void CanRetrieveMembershipService()
		{
			this.membershipService = Bootstrapper.Install();
			Assert.IsNotNull(this.membershipService);
		}

		/// <summary>
		/// Determines whether this instance [can retrieve application roles].
		/// </summary>
		[Test]
		public void CanRetrieveApplicationRoles()
		{
			Roles roles = this.membershipService.RetrieveRoles();
			Assert.IsNotNull(roles);
			Assert.Greater(roles.Count, 0);

			foreach(Role role in roles)
			{
				Console.WriteLine(role.ToString());
			}
		}

		/// <summary>
		/// Determines whether this instance [can retrieve application role by id].
		/// </summary>
		[Test]
		public void CanRetrieveApplicationRoleById()
		{
			Roles roles = this.membershipService.RetrieveRoles();
			Assert.IsNotNull(roles);
			Assert.Greater(roles.Count, 0);

			foreach(Role role in roles)
			{
				Role r = this.membershipService.RetrieveRole(role.RoleId);
				Assert.IsNotNull(r);
				Console.WriteLine(r.ToString());
			}
		}

		/// <summary>
		/// Determines whether this instance [can update application roles].
		/// </summary>
		[Test]
		public void CanUpdateApplicationRoles()
		{
			Roles roles = this.membershipService.RetrieveRoles();
			Assert.IsNotNull(roles);
			Assert.Greater(roles.Count, 0);

			foreach(Role role in roles)
			{
				Role r = this.membershipService.RetrieveRole(role.RoleId);
				Assert.IsNotNull(r);
				Console.WriteLine(r.ToString());

				r.IsActive = false;
				bool isUpdated = this.membershipService.UpdateRole(r);
				Assert.IsTrue(isUpdated);

				r.IsActive = true;
				isUpdated = this.membershipService.UpdateRole(r);
				Assert.IsTrue(isUpdated);
			}
		}

		/// <summary>
		/// Determines whether this instance [can retrieve user roles].
		/// </summary>
		[Test]
		public void CanRetrieveUserRoles()
		{
			string userRoles = this.membershipService.RetrieveUserRolesString(this.emailAddress);
			Assert.IsNotNullOrEmpty(userRoles);
			Console.WriteLine(userRoles);
		}

		/// <summary>
		/// Determines whether this instance [can retrieve user role list by user].
		/// </summary>
		[Test]
		public void CanRetrieveUserRoleListByUser()
		{
			EmailInRoles emailInRoles = this.membershipService.RetrieveEmailInRoles(this.emailAddress);
			Assert.IsNotNull(emailInRoles);
			Assert.Greater(emailInRoles.Count, 0);

			foreach(EmailInRole emailInRole in emailInRoles)
			{
				Console.WriteLine(emailInRole.ToString());
			}
		}

		/// <summary>
		/// Determines whether this instance [can remove and add user role for user].
		/// </summary>
		[Test]
		public void CanRemoveAndAddUserRoleForUser()
		{
			EmailInRoles emailInRoles = this.membershipService.RetrieveEmailInRoles(this.emailAddress);
			Assert.IsNotNull(emailInRoles);
			Assert.Greater(emailInRoles.Count, 0);

			foreach(EmailInRole emailInRole in emailInRoles)
			{
				EmailInRole addRole = new EmailInRole{
					Email = emailInRole.Email,
					RoleId = emailInRole.RoleId 
				};
				Console.WriteLine(emailInRole.ToString());

				bool isRemoved = this.membershipService.RemoveUserInRole(emailInRole);
				Assert.IsTrue(isRemoved);

				bool isAdded = this.membershipService.AddUserToRole(addRole);
				Assert.IsTrue(isAdded);
			}
		}

		#endregion;
	}
}
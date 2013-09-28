#region

using System;
using System.Linq;
using BuildMotion.Membership.Business;
using BuildMotion.Membership.Business.Security.Actions;
using BuildMotion.Membership.DataAccess;
using BuildMotion.Membership.Entity.Google;
using Moq;
using NUnit.Framework;
using Vergosity.Validation;

#endregion

namespace BuildMotion.Membership.Tests
{
	[TestFixture]
	public class MembershipActionTests
	{
		[SetUp]
		public void SetUp()
		{
			this.providerMock = new Mock<MembershipProviderBase>();
			this.repositoryMock = new Mock<IRepository>();
			providerMock.Setup(r => r.Repository).Returns(repositoryMock.Object);
		}

		/// <summary>
		///     Tests the fixture set up.
		/// </summary>
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			this.membershipService = Bootstrapper.Install();
			Assert.IsNotNull(this.membershipService);
		}

		private IMembershipService membershipService;
		private Mock<MembershipProviderBase> providerMock;
		private Mock<IRepository> repositoryMock;
		private readonly string emailAddress = "matt.vaughn@buildmotion.com";

		#region UserDomainIsValidAction Tests;
		/// <summary>
		///     Determines whether this instance [can validate target domain is google app domain].
		/// </summary>
		[Test]
		public void CanValidateTargetDomainIsGoogleAppDomain()
		{
			string domain = "buildmotion.com";
			this.providerMock.Setup(d => d.GoogleAppDomain).Returns("buildmotion.com");
			UserDomainIsValidAction action = new UserDomainIsValidAction(domain, this.providerMock.Object);
			action.Execute();
			Assert.AreEqual(domain, this.providerMock.Object.GoogleAppDomain);
			Assert.IsTrue(action.DomainIsValid);
		}

		/// <summary>
		/// Attempts the validate target domain is google app domain with invalid domain.
		/// </summary>
		[Test]
		public void AttemptValidateTargetDomainIsGoogleAppDomainWithInvalidDomain()
		{
			string domain = "buildmotion2.com";
			this.providerMock.Setup(d => d.GoogleAppDomain).Returns("buildmotion.com");
			UserDomainIsValidAction action = new UserDomainIsValidAction(domain, this.providerMock.Object);
			action.Execute();
			Assert.IsFalse(action.DomainIsValid);
		}

		#endregion;

		/// <summary>
		/// Asserts the validation context.
		/// </summary>
		/// <param name="validationContext">The validation context.</param>
		private void WriteValidationContextRuleResults(IValidationContext validationContext)
		{
			if(!validationContext.IsValid)
			{
				var results = (from r in validationContext.Results
							   where r.IsValid == false
							   select r).ToList();

				Console.WriteLine("Failed Rules: ");
				foreach(var r in results)
				{
					Console.WriteLine("IsValid: {2}; Name: {0}; Message: {1}", r.RulePolicy.Name, r.RulePolicy.Message, r.IsValid);
					var ruleComposite = r.RulePolicy as RuleComposite;
					if(ruleComposite != null)
					{
						foreach(Result result in ruleComposite.ResultDetails)
						{
							Console.WriteLine("\tIsValid: {2}; Name: {0}; Message: {1}", result.RulePolicy.Name, result.Message, result.IsValid);
						}
					}
				}
			}
		}

		/// <summary>
		/// Retrieves the current access token.
		/// </summary>
		/// <returns></returns>
		private string RetrieveCurrentAccessToken()
		{
			string accessToken = string.Empty;

			// need to retrieve a valid Token: 
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			if(DateTime.UtcNow > user.Authorization.AccessTokenExpirationUtc)
			{
				// need to retrieve updated access token;
				accessToken = this.membershipService.RefreshAccessToken(user);
			}
			else
			{
				accessToken = user.Authorization.AccessToken;
			}
			return accessToken;
		}

		#region RefreshAccessTokenAction 

		/// <summary>
		/// Determines whether this instance [can refresh access token].
		/// </summary>
		[Test]
		public void CanRefreshAccessToken()
		{
			//setup with real user;
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			this.providerMock.Setup(i => i.GoogleClientId).Returns(this.membershipService.GoogleClientId);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(this.membershipService.GoogleClientSecret);

			// test;
			var action = new RefreshAccessTokenAction(user, this.providerMock.Object);
			action.Execute();

			// assert/verify;
			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNotNullOrEmpty(action.AccessToken);
		}

		/// <summary>
		/// Attempts the refresh access token with invalid client id.
		/// </summary>
		[Test]
		public void AttemptRefreshAccessTokenWithInvalidClientId()
		{
			//setup with real user;
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			this.providerMock.Setup(i => i.GoogleClientId).Returns(string.Empty);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(this.membershipService.GoogleClientSecret);

			// test;
			var action = new RefreshAccessTokenAction(user, this.providerMock.Object);
			action.Execute();

			// assert/verify;
			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNullOrEmpty(action.AccessToken);
		}

		/// <summary>
		/// Attempts the refresh access token with invalid client secret.
		/// </summary>
		[Test]
		public void AttemptRefreshAccessTokenWithInvalidClientSecret()
		{
			//setup with real user;
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			this.providerMock.Setup(i => i.GoogleClientId).Returns(this.membershipService.GoogleClientId);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(string.Empty);

			// test;
			var action = new RefreshAccessTokenAction(user, this.providerMock.Object);
			action.Execute();

			// assert/verify;
			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNullOrEmpty(action.AccessToken);
		}


		/// <summary>
		/// Attempts the refresh access token with invalid user.
		/// </summary>
		[Test]
		public void AttemptRefreshAccessTokenWithInvalidUser()
		{
			//setup with real user;
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			user.Email = string.Empty; //invalid email address;
			this.providerMock.Setup(i => i.GoogleClientId).Returns(this.membershipService.GoogleClientId);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(this.membershipService.GoogleClientSecret);

			// test;
			var action = new RefreshAccessTokenAction(user, this.providerMock.Object);
			action.Execute();

			// assert/verify;
			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNullOrEmpty(action.AccessToken);
		}


		/// <summary>
		/// Attempts the refresh access token with invalid authorization.
		/// </summary>
		[Test]
		public void AttemptRefreshAccessTokenWithInvalidAuthorization()
		{
			//setup with real user;
			UserInformation user = this.membershipService.RetrieveUserInformation(this.emailAddress);
			user.Authorization = null;
			this.providerMock.Setup(i => i.GoogleClientId).Returns(this.membershipService.GoogleClientId);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(this.membershipService.GoogleClientSecret);

			// test;
			var action = new RefreshAccessTokenAction(user, this.providerMock.Object);
			action.Execute();

			// assert/verify;
			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNullOrEmpty(action.AccessToken);
		}

		#endregion

		#region CreateUpdateAuthorizationUserInfoAction Tests
		/// <summary>
		/// Determines whether this instance [can create update authorization user info].
		/// </summary>
		[Test]
		public void CanCreateUpdateAuthorizationUserInfo()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = userInformation.Email; //remove this to fail the test;
			userInformation.Id = Guid.NewGuid();

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>()));
			this.repositoryMock.Setup(r => r.CreateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(c => c.CreateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsTrue(action.ValidationContext.IsValid);
			Assert.IsNotNull(user);
			Assert.AreNotEqual(Guid.Empty, user.Id);
			Assert.IsNotNullOrEmpty(user.Email);
		}

		/// <summary>
		/// Attempts the create update authorization user info with invalid email address.
		/// </summary>
		[Test]
		public void AttemptCreateUpdateAuthorizationUserInfoWithInvalidEmailAddress()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = string.Empty; //remove this to fail the test;
			userInformation.Email = string.Empty;

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>()));
			this.repositoryMock.Setup(r => r.CreateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(c => c.CreateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.ValidationContext.IsValid);
			Assert.IsNull(user);
		}

		/// <summary>
		/// Attempts the create update authorization user info with invalid google id.
		/// </summary>
		[Test]
		public void AttemptCreateUpdateAuthorizationUserInfoWithInvalidGoogleId()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = userInformation.Email; //remove this to fail the test;
			userInformation.Id = Guid.NewGuid();
			userInformation.GoogleId = string.Empty;//should cause action to fail;

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>()));
			this.repositoryMock.Setup(r => r.CreateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(c => c.CreateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.ValidationContext.IsValid);
			Assert.IsNull(user);
		}

		/// <summary>
		/// Determines whether this instance [can update user authorization information].
		/// </summary>
		[Test]
		public void CanUpdateUserAuthorizationInformation()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = userInformation.Email; //remove this to fail the test;
			userInformation.Id = Guid.NewGuid();

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>())).Returns(userInformation);
			this.repositoryMock.Setup(u => u.UpdateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(a => a.UpdateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsTrue(action.ValidationContext.IsValid);
			Assert.IsNotNull(user);
			Assert.AreNotEqual(Guid.Empty, user.Id);
			Assert.IsNotNullOrEmpty(user.Email);
		}

		/// <summary>
		/// Attempts the update user authorization information with invalid email address.
		/// </summary>
		[Test]
		public void AttemptUpdateUserAuthorizationInformationWithInvalidEmailAddress()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = string.Empty; //remove this to fail the test;
			userInformation.Id = Guid.NewGuid();

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>())).Returns(userInformation);
			this.repositoryMock.Setup(u => u.UpdateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(a => a.UpdateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.ValidationContext.IsValid);
			Assert.IsNull(user);
		}

		/// <summary>
		/// Attempts the update user authorization information with invalid refresh token.
		/// </summary>
		[Test]
		public void AttemptUpdateUserAuthorizationInformationWithInvalidRefreshToken()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = userInformation.Email;
			authorization.RefreshToken = string.Empty;
			userInformation.Id = Guid.NewGuid();

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>())).Returns(userInformation);
			this.repositoryMock.Setup(u => u.UpdateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(a => a.UpdateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.ValidationContext.IsValid);
			Assert.IsNull(user);
		}

		/// <summary>
		/// Attempts the update user authorization information with invalid access token.
		/// </summary>
		[Test]
		public void AttemptUpdateUserAuthorizationInformationWithInvalidAccessToken()
		{
			// test setup;
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			AuthorizationInformation authorization = new AuthorizationInformation("abc", DateTime.Now.ToUniversalTime().AddHours(1), DateTime.Now.ToUniversalTime(), "xyz");
			authorization.Email = userInformation.Email;
			authorization.AccessToken = string.Empty;
			userInformation.Id = Guid.NewGuid();

			this.repositoryMock.Setup(r => r.RetrieveUserInformation(It.IsAny<string>())).Returns(userInformation);
			this.repositoryMock.Setup(u => u.UpdateUserInformation(It.IsAny<UserInformation>())).Returns(userInformation);
			this.repositoryMock.Setup(a => a.UpdateAuthorizationInformation(It.IsAny<AuthorizationInformation>())).Returns(true);

			// test;
			var action = new CreateUpdateAuthorizationUserInfoAction(authorization, userInformation, this.providerMock.Object);
			action.Execute();
			UserInformation user = action.UserInformation;

			// Assertions;
			WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.ValidationContext.IsValid);
			Assert.IsNull(user);
		}
		#endregion;

		#region CreateGoogleClientAction Tests
		/// <summary>
		/// Determines whether this instance [can create google client].
		/// </summary>
		[Test]
		public void CanCreateGoogleClient()
		{
			var serverDescription = this.membershipService.RetrieveAuthServerDescription();
			this.providerMock.Setup(c => c.GoogleClientId).Returns("abcdefghijklmnop");
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns("qrstuvwxyz");
			var action = new CreateGoogleClientAction(serverDescription, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNotNull(action.Client);
		}

		/// <summary>
		/// Attempts the create google client with invalid client id.
		/// </summary>
		[Test]
		public void AttemptCreateGoogleClientWithInvalidClientId()
		{
			var serverDescription = this.membershipService.RetrieveAuthServerDescription();
			this.providerMock.Setup(c => c.GoogleClientId).Returns(string.Empty);
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns("qrstuvwxyz");
			var action = new CreateGoogleClientAction(serverDescription, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNull(action.Client);
			Assert.IsFalse(action.ValidationContext.IsValid);
		}

		/// <summary>
		/// Attempts the create google client with invalid client secret.
		/// </summary>
		[Test]
		public void AttemptCreateGoogleClientWithInvalidClientSecret()
		{
			var serverDescription = this.membershipService.RetrieveAuthServerDescription();
			this.providerMock.Setup(c => c.GoogleClientId).Returns("abcdef");
			this.providerMock.Setup(s => s.GoogleClientSecret).Returns(string.Empty);
			var action = new CreateGoogleClientAction(serverDescription, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNull(action.Client);
			Assert.IsFalse(action.ValidationContext.IsValid);
		}

		#endregion;

		#region RetrieveTokenInfoAction

		/// <summary>
		/// Determines whether this instance [can get token info with access token].
		/// </summary>
		[Test]
		public void CanGetTokenInfoWithAccessToken()
		{
			var accessToken = RetrieveCurrentAccessToken();
			Assert.IsNotNullOrEmpty(accessToken);

			dynamic tokenInfo = null;
			RetrieveTokenInfoAction action = new RetrieveTokenInfoAction(accessToken, this.providerMock.Object);
			action.Execute();
			tokenInfo = action.TokenInfo;

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNotNull(tokenInfo);
			//Assert.IsNotNullOrEmpty(tokenInfo.issued_to);
			//Assert.IsNotNullOrEmpty(tokenInfo.audience);
			//Assert.IsNotNullOrEmpty(tokenInfo.user_id);
			//Assert.IsNotNullOrEmpty(tokenInfo.scope);
			//Assert.IsNotNullOrEmpty(tokenInfo.expires_in);
			//Assert.IsNotNullOrEmpty(tokenInfo.email);
			//Assert.IsTrue(tokenInfo.verified_email);
			//Assert.IsNotNullOrEmpty(tokenInfo.access_type);

			Console.WriteLine(tokenInfo.ToString());
		}



		/// <summary>
		/// Attempts the get token info with invalid access token.
		/// </summary>
		[Test]
		public void AttemptGetTokenInfoWithInvalidAccessToken()
		{
			dynamic tokenInfo = null;
			RetrieveTokenInfoAction action = new RetrieveTokenInfoAction("", this.providerMock.Object);
			action.Execute();
			tokenInfo = action.TokenInfo;

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNull(tokenInfo);
		}

		#endregion;

		#region ValidateTokenAction

		/// <summary>
		/// Determines whether this instance [can validate token].
		/// </summary>
		[Test]
		public void CanValidateToken()
		{
			string accessToken = this.RetrieveCurrentAccessToken();
			TokenInfo tokenInfo = this.membershipService.RetrieveTokenInfo(accessToken);
			this.providerMock.Setup(c => c.GoogleClientId).Returns(this.membershipService.GoogleClientId);
			this.providerMock.Setup(t => t.GetTokenInfo(accessToken)).Returns(tokenInfo);

			var action = new ValidateTokenAction(accessToken, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsTrue(action.IsValid);
		}


		[Test]
		public void AttemptValidateToken()
		{
			string accessToken = "";//invalid value;
			var action = new ValidateTokenAction(accessToken, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsFalse(action.IsValid);
		}

		#endregion;

		#region RetrieveGoogleUserInformationAction

		/// <summary>
		/// Determines whether this instance [can retrieve google user information].
		/// </summary>
		[Test]
		public void CanRetrieveGoogleUserInformation()
		{
			string accessToken = this.RetrieveCurrentAccessToken();
			var action = new RetrieveGoogleUserInformationAction(accessToken, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNotNull(action.UserInformation);
			Assert.IsNotNullOrEmpty(action.UserInformation.Id.ToString());
			Assert.IsNotNullOrEmpty(action.UserInformation.Email);
			Assert.IsNotNullOrEmpty(action.UserInformation.FullName);
			Assert.IsNotNullOrEmpty(action.UserInformation.FirstName);
			Assert.IsNotNullOrEmpty(action.UserInformation.LastName);
			Assert.IsNotNullOrEmpty(action.UserInformation.Domain);
			Assert.IsTrue(action.UserInformation.IsVerifiedEmail);
			Assert.IsNotNullOrEmpty(action.UserInformation.GoogleId);
		}


		/// <summary>
		/// Attempts the retrieve google user information with invalid access token.
		/// </summary>
		[Test]
		public void AttemptRetrieveGoogleUserInformationWithInvalidAccessToken()
		{
			string accessToken = null;
			var action = new RetrieveGoogleUserInformationAction(accessToken, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNull(action.UserInformation);
		}

		#endregion;

		#region CreateFormsAuthenticationCookieAction
		
		/// <summary>
		/// Determines whether this instance [can create forms authentication cookie].
		/// </summary>
		[Test]
		public void CanCreateFormsAuthenticationCookie()
		{
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			this.providerMock.Setup(r => r.RetrieveUserRolesString(It.IsAny<string>())).Returns("admin|user");
			var action = new CreateFormsAuthenticationCookieAction(userInformation, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNotNull(action.Cookie);
		}


		[Test]
		public void AttemptCreateFormsAuthenticationCookieWithInvalidUser()
		{
			UserInformation userInformation = new UserInformation("1234", "ana@buildmotion.com", true, "ana", "valencia", "ana valencia", "buildmotion.com", string.Empty);
			userInformation.Email = string.Empty; //invalid email address;
			this.providerMock.Setup(r => r.RetrieveUserRolesString(It.IsAny<string>())).Returns("admin|user");
			var action = new CreateFormsAuthenticationCookieAction(userInformation, this.providerMock.Object);
			action.Execute();

			this.WriteValidationContextRuleResults(action.ValidationContext);
			Assert.IsNull(action.Cookie);
		}
		#endregion;
	}
}
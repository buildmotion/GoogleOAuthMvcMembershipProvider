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

using System;

#endregion

namespace BuildMotion.Membership.Entity.Google
{
	public class AuthorizationInformation
	{
		private string accessToken;
		private DateTime accessTokenExpirationUtc;
		private DateTime accessTokenIssueDateUtc;
		private bool isDeleted;
		private string refreshToken;
		private string email;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationInformation"/> class.
		/// </summary>
		public AuthorizationInformation()
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="AuthorizationInformation" /> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <param name="tokenExpirationUtc">The token expiration UTC.</param>
		/// <param name="issueDateUtc">The issue date UTC.</param>
		/// <param name="refreshToken">The refresh token.</param>
		public AuthorizationInformation(string accessToken, DateTime tokenExpirationUtc, DateTime issueDateUtc,
		                          string refreshToken)
		{
			this.accessToken = accessToken;
			this.AccessTokenExpirationUtc = tokenExpirationUtc;
			this.accessTokenIssueDateUtc = issueDateUtc;
			this.refreshToken = refreshToken;
		}

		public string AccessToken
		{
			get
			{
				return accessToken;
			}
			set
			{
				accessToken = value;
			}
		}
		
		public DateTime AccessTokenExpirationUtc
		{
			get
			{
				return accessTokenExpirationUtc;
			}
			set
			{
				accessTokenExpirationUtc = value;
			}
		}
		
		public DateTime AccessTokenIssueDateUtc
		{
			get
			{
				return accessTokenIssueDateUtc;
			}
			set
			{
				accessTokenIssueDateUtc = value;
			}
		}
		
		public string RefreshToken
		{
			get
			{
				return refreshToken;
			}
			set
			{
				refreshToken = value;
			}
		}
		
		public bool IsDeleted
		{
			get
			{
				return isDeleted;
			}
			set
			{
				isDeleted = value;
			}
		}
		
		public string Email
		{
			get
			{
				return email;
			}
			set
			{
				email = value;
			}
		}
	}
}
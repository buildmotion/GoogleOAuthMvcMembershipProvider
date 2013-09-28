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
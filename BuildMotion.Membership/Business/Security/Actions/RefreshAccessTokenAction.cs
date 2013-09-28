#region

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using BuildMotion.Membership.Business.Security.Attributes;
using BuildMotion.Membership.Entity.Google;
using Vergosity.Actions;
using Vergosity.Validation.Attributes;

#endregion

namespace BuildMotion.Membership.Business.Security.Actions
{
	internal class RefreshAccessTokenAction : ActionBase
	{
		[UserInformationIsValid("UserInfoIsValid", "The user information is not valid. Cannot refresh the access token.")] 
		private readonly UserInformation user;
		private string accessToken;
		[StringIsNotEmptySpace("ClientIdIsValid", "The client id cannot be empty or null string.")]
		private string clientId;
		[StringIsNotEmptySpace("ClientSecretIsValid", "The client secret cannot be empty or null string.")]
		private string clientSecret;
		[IsNotNull("AuthorizationIsNotNull", "The user's authorization cannot be null.")]
		private AuthorizationInformation authorization;

		/// <summary>
		///     Initializes a new instance of the <see cref="RefreshAccessTokenAction" /> class.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="membershipProvider">The membership provider.</param>
		public RefreshAccessTokenAction(UserInformation user, MembershipProviderBase membershipProvider)
			: base(membershipProvider)
		{
			this.user = user;
			this.clientId = this.Provider.GoogleClientId;
			this.clientSecret = this.Provider.GoogleClientSecret;
			this.authorization = this.user.Authorization;
		}

		/// <summary>
		///     Gets the access token.
		/// </summary>
		/// <value>
		///     The access token.
		/// </value>
		public string AccessToken
		{
			get
			{
				return accessToken;
			}
		}

		/// <summary>
		///     Does this instance.
		/// </summary>
		public override void PerformAction()
		{
			//do something amazing here;
			string url = @"https://accounts.google.com/o/oauth2/token";
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			AuthorizationInformation auth = null;
			if (httpWebRequest != null && !string.IsNullOrEmpty(this.authorization.RefreshToken))
			{
				#region Create Data for the form post;

				string refreshToken = HttpUtility.UrlEncode(this.user.Authorization.RefreshToken);
				string formDataFormat = @"client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token";
				string formData = string.Format(formDataFormat, this.clientId, this.clientSecret, refreshToken);
				byte[] data = new ASCIIEncoding().GetBytes(formData);

				#endregion;

				#region set request properties; 

				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				httpWebRequest.ContentLength = data.Length;

				#endregion;

				#region Add the data to the request; 

				Stream myStream = httpWebRequest.GetRequestStream();
				myStream.Write(data, 0, data.Length);
				myStream.Close();

				#endregion;

				// Get the response and retrieve data;
				HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;

				if (response != null)
				{
					#region Retreive Response data;
					string responseData = string.Empty;
					Stream stream = null;

					using (stream = response.GetResponseStream())
					{
						if (stream != null && stream.CanRead)
						{
							StreamReader reader = null;
							using (reader = new StreamReader(stream))
							{
								responseData = reader.ReadToEnd();
							}
						}
					}
					
					AccessTokenInfo info = new JavaScriptSerializer().Deserialize<AccessTokenInfo>(responseData);
					if (info != null)
					{
						// update the user's authorization and user information;
						auth = this.authorization;
						auth.AccessToken = info.access_token;
						auth.AccessTokenIssueDateUtc = DateTime.Now.ToUniversalTime();
						// less (60) seconds to provide a little wiggle room; 
						auth.AccessTokenExpirationUtc = DateTime.Now.AddSeconds(info.expires_in - 60).ToUniversalTime();
						
						//Now we can finally set the return object; 
						this.accessToken = info.access_token; // all that for this...whoa!!!
					}
					#endregion;

					// update the autorization information for the specified user; 
					this.Provider.CreateUpdateAuthorizationUserInfo(auth, user);
				}
			}
		}

		/// <summary>
		///     Use to validate the resultDetails of the action. The implementation may include any event or KPI logging.
		/// </summary>
		/// <returns> </returns>
		protected override ActionResult ValidateActionResult()
		{
			this.Result = !string.IsNullOrWhiteSpace(this.accessToken) ? ActionResult.Success : ActionResult.Fail;
			return Result;
		}
	}
}
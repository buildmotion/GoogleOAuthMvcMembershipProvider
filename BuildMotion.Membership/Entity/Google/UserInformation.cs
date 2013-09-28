using System;

namespace BuildMotion.Membership.Entity.Google
{
	public class UserInformation
	{
		private Guid id;
		private string domain;
		private string email;
		private string firstName;
		private string fullName;
		private string googleId;
		private bool isVerifiedEmail;
		private string lastName;
		private string link;
		private AuthorizationInformation authorization;
		private bool isActive = true; //default for user;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserInformation"/> class.
		/// </summary>
		public UserInformation()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserInformation"/> class.
		/// </summary>
		/// <param name="googleid">The googleid.</param>
		/// <param name="email">The email.</param>
		/// <param name="isVerifiedEmail">if set to <c>true</c> [is verified email].</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="fullName">The full name.</param>
		/// <param name="domain">The domain.</param>
		/// <param name="link">The link.</param>
		public UserInformation(string googleid, string email, bool isVerifiedEmail, string firstName, string lastName,
		                string fullName, string domain, string link)
		{
			this.googleId = googleid;
			this.email = email;
			this.isVerifiedEmail = isVerifiedEmail;
			this.firstName = firstName;
			this.lastName = lastName;
			this.fullName = fullName;
			this.domain = domain;
			this.link = link;
		}

		public string GoogleId
		{
			get
			{
				return googleId;
			}
			set
			{
				googleId = value;
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
		public bool IsVerifiedEmail
		{
			get
			{
				return isVerifiedEmail;
			}
			set
			{
				isVerifiedEmail = value;
			}
		}
		public string FullName
		{
			get
			{
				return fullName;
			}
			set
			{
				fullName = value;
			}
		}
		public string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				firstName = value;
			}
		}
		public string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				lastName = value;
			}
		}
		public string Domain
		{
			get
			{
				return domain;
			}
			set
			{
				domain = value;
			}
		}
		public string Link
		{
			get
			{
				return link;
			}
			set
			{
				link = value;
			}
		}
		public Guid Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}
		public AuthorizationInformation Authorization
		{
			get
			{
				return authorization;
			}
			set
			{
				authorization = value;
			}
		}
		public bool IsActive
		{
			get
			{
				return isActive;
			}
			set
			{
				isActive = value;
			}
		}
	}
}
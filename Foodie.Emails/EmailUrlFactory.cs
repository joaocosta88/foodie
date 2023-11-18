namespace Foodie.Emails {
	public class EmailUrlFactory {
		private readonly EmailUrlConfiguration _emailUrlConfiguration;

		public EmailUrlFactory(EmailUrlConfiguration emailUrlConfiguration)
		{
			_emailUrlConfiguration = emailUrlConfiguration;
		}

		public string GetAccountConfirmationUrl(string token)
		=> $"{_emailUrlConfiguration.Domain}{_emailUrlConfiguration.AccountRegistrationEndpoint}{token}";

		public string GetPasswordResetUrl(string token)
		=> $"{_emailUrlConfiguration.Domain}{_emailUrlConfiguration.PasswordResetEndpoint}{token}";
	}
}

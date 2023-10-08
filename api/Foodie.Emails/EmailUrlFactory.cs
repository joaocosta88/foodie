namespace Foodie.Emails {
	public class EmailUrlFactory {
		private readonly EmailUrlConfiguration _emailUrlConfiguration;

		public EmailUrlFactory(EmailUrlConfiguration emailUrlConfiguration)
		{
			_emailUrlConfiguration = emailUrlConfiguration;
		}

		public string GetAccountConfirmationUrl(string token)
		{
			return $"{_emailUrlConfiguration.Domain}{_emailUrlConfiguration.AccountRegistrationEndpoint}{token}";
		}
	}
}

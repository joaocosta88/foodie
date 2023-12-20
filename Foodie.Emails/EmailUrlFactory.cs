using Microsoft.Extensions.Options;

namespace Foodie.Emails {
	public class EmailUrlFactory {
		private readonly EmailUrlConfiguration _emailUrlConfiguration;

		public EmailUrlFactory(IOptions<AuthMessageSenderOptions> optionsAccessor)
		{
			_emailUrlConfiguration = optionsAccessor.Value.EmailUrlConfiguration;
		}

		public string GetAccountConfirmationUrl(string token)
		=> $"{_emailUrlConfiguration.Domain}{_emailUrlConfiguration.AccountRegistrationEndpoint}{token}";

		public string GetPasswordResetUrl(string token)
		=> $"{_emailUrlConfiguration.Domain}{_emailUrlConfiguration.PasswordResetEndpoint}{token}";
	}
}

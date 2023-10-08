namespace Foodie.Emails {
	public class EmailTemplateFactory {

		private readonly EmailUrlFactory _emailUrlFactory;

		public EmailTemplateFactory(EmailUrlFactory emailUrlFactory)
		{
			_emailUrlFactory = emailUrlFactory;
		}

		public string CreateAccountConfirmationEmail(string confirmationToken)
		=> $"Please use the followig link to validate your account. <br>" +
				$"{_emailUrlFactory.GetAccountConfirmationUrl(confirmationToken)}";

		public string CreatePasswordResetEmail(string email, string resetToken)
		=> $"{email}, please use the followig token to reset your password. <br>" +
				$"{resetToken}";
	}
}

namespace Foodie.Emails {
	public class EmailTemplateFactory {

		private readonly EmailUrlFactory _emailUrlFactory;

		public EmailTemplateFactory(EmailUrlFactory emailUrlFactory)
		{
			_emailUrlFactory = emailUrlFactory;
		}

		public string CreateAccountConfirmationEmail(string confirmationToken)
		{
			return
				$"Please use the followig link to validate your account. <br>" +
				$"{_emailUrlFactory.GetAccountConfirmationUrl(confirmationToken)}";
		}

		public string CreateAccountResetEmail(string resetToken)
		{
			return "";
		}
	}
}

using Foodie.Emails.Exceptions;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Foodie.Emails {
	public class EmailSender {

		private readonly SendGridClient _sendGridClient;
		private readonly EmailAddress _fromEmailAddress;
		private readonly EmailTemplateFactory _emailTemplateFactory;
		public EmailSender(string sendGridApiKey, string from, string alias, EmailTemplateFactory emailTemplateFactory)
		{
			_sendGridClient = new SendGridClient(sendGridApiKey);
			_fromEmailAddress = new EmailAddress(from, alias);
			_emailTemplateFactory = emailTemplateFactory;
		}

		public async Task SendAccountConfirmationEmailAsync(string to, string token)
		{
			string subject = "Foodie - Confirm your account";
			string template = _emailTemplateFactory.CreateAccountConfirmationEmail(token);

			await SendEmailAsync(to, subject, template);
		}

		public async Task SendPasswordResetEmailAsync(string to, string token)
		{
			string subject = "Foodie - Reset your account";
			string template = _emailTemplateFactory.CreatePasswordResetEmail(to, token); ;
			await SendEmailAsync(to, subject, template);
		}

		private async Task SendEmailAsync(string to, string subject, string template)
		{
			var msg = new SendGridMessage
			{
				From = _fromEmailAddress,
				Subject = subject,
				PlainTextContent = template
			};

			msg.AddTo(to);
			var response = await _sendGridClient.SendEmailAsync(msg);
			if (!response.IsSuccessStatusCode)
			{
				var responseBody = await response.DeserializeResponseBodyAsync();
				throw new EmailCouldNotBeSentException(JsonConvert.SerializeObject(responseBody));
			}
		}
	}
}
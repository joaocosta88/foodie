using Foodie.Emails.Exceptions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Foodie.Emails {
	public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, Foodie.Emails.IEmailSender {

        private readonly ILogger _logger;
        private readonly SendGridClient _sendGridClient;
        private readonly EmailTemplateFactory _emailTemplateFactory;
        private readonly EmailAddress _fromEmailAddress;

        private readonly AuthMessageSenderOptions Options;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, EmailTemplateFactory emailTemplateFactory, ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            _emailTemplateFactory = emailTemplateFactory;

            _fromEmailAddress = new EmailAddress(Options.From, Options.Alias);
            _sendGridClient = new SendGridClient(Options.SendGridKey);
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

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage
            {
                From = _fromEmailAddress,
                Subject = subject,
                HtmlContent = htmlMessage
            };

            msg.AddTo(email);
            var response = await _sendGridClient.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.DeserializeResponseBodyAsync();
                throw new EmailCouldNotBeSentException(JsonConvert.SerializeObject(responseBody));
            }
        }
    }
}
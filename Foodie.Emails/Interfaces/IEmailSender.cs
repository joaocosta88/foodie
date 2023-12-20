namespace Foodie.Emails {
    public interface IEmailSender {
        Task SendAccountConfirmationEmailAsync(string to, string token);
        Task SendPasswordResetEmailAsync(string to, string token);
    }
}

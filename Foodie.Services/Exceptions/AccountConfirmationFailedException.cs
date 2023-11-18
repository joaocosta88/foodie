namespace Foodie.Services.Exceptions {
	public class AccountConfirmationFailedException : Exception {
		public AccountConfirmationFailedException() : base() { }
		public AccountConfirmationFailedException(string message) : base(message) { }
	}
}

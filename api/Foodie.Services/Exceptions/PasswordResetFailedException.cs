namespace Foodie.Services.Exceptions {
	public class PasswordResetFailedException : Exception {
		public PasswordResetFailedException() : base() { }
		public PasswordResetFailedException(string message) : base(message) { }
	}
}

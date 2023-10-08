namespace Foodie.Emails {
	public class EmailCouldNotBeSentException : Exception {
		public EmailCouldNotBeSentException() { }
		public EmailCouldNotBeSentException(string message) : base(message) { }
	}
}

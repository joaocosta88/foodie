﻿namespace Foodie.Services.Exceptions {
	public class RegistrationFailedException : Exception {
		public RegistrationFailedException() : base() { }
		public RegistrationFailedException(string message) : base(message) { }
	}
}

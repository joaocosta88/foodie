﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Services.Exceptions {
	public class AuthenticationFailedException : Exception {
		public AuthenticationFailedException() : base() { }
		public AuthenticationFailedException(string message) : base(message) { }
	}
}

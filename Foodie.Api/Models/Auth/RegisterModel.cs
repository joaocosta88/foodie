﻿using System.ComponentModel.DataAnnotations;

namespace Foodie.Api.Models.Auth {
	public class RegisterModel {

		[EmailAddress]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

	}
}
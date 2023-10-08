using System.ComponentModel.DataAnnotations;

namespace Foodie.Api.Models.Auth {
	public class PasswordResetModel {
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Token { get; set; }
		[Required]
		public string NewPassword { get; set; }
	}
}

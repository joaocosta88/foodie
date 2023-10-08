using Foodie.Api.Models.Auth;
using Foodie.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Api.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase {
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}


		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var token = await _authService.LoginAsync(model.Email, model.Password);

			return Ok(token);
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _authService.RegisterAsync(model.Email, model.Password);

			return Ok();
		}

		[HttpGet]
		[Route("confirm")]
		public async Task<IActionResult> ConfirmAccountAsync(string token)
		{
			await _authService.ConfirmAccountAsync(token);
			return Ok("account confirmed");
		}

		[HttpGet]
		[Route("reset")]
		public async Task GetPasswordResetAsync(string email)
		{
			await _authService.GetResetPasswordTokenAsync(email);
		}

		[HttpPost]
		[Route("reset")]
		public async Task<IActionResult> ResetPasswordAsync([FromBody] PasswordResetModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _authService.ResetPasswordReset(model.Email, model.Token, model.NewPassword);
			return Ok("done");
		}
	}
}

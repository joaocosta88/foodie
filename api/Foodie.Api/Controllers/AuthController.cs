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
		public async Task<IActionResult> Login([FromBody] LoginModel model)
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
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
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
		public async Task<IActionResult> ConfirmAccount(string token)
		{
			await _authService.ConfirmAccountAsync(token);
			return Ok();
		}
	}
}

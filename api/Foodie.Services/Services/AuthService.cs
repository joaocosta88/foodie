using Foodie.Emails;
using Foodie.Emails.Utils;
using Foodie.Entities.Entities;
using Foodie.Services.Dtos;
using Foodie.Services.Exceptions;
using Foodie.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Foodie.Services.Services {
	public class AuthService {
		private readonly UserManager<FoodieUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly AuthHelper _authHelper;
		private readonly EmailSender _emailSender;

		public AuthService(UserManager<FoodieUser> userManager, RoleManager<IdentityRole> roleManager, AuthHelper authHelper, EmailSender emailSender)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_authHelper = authHelper;
			_emailSender = emailSender;
		}

		public async Task<AuthTokenDto> LoginAsync(string email, string password)
		{

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
			{
				throw new AuthenticationFailedException("User does not exist or invalid password");
			}
			if (!user.EmailConfirmed)
			{
				throw new AuthenticationFailedException("User's email is not confirmed");
			}

			var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

			var userRoles = await _userManager.GetRolesAsync(user);
			foreach (var userRole in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			var token = _authHelper.GenerateJwtToken(authClaims);
			return new AuthTokenDto
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = token.ValidTo
			};
		}

		public async Task RegisterAsync(string email, string password)
		{
			var userExists = await _userManager.FindByEmailAsync(email);
			if (userExists != null)
			{
				throw new RegistrationFailedException();
			}

			var user = new FoodieUser
			{
				Email = email,
				UserName = email
			};

			var result = await _userManager.CreateAsync(user, password);
			if (!result.Succeeded)
			{
				throw new RegistrationFailedException(string.Join("\n",result.Errors.Select(m => m.Code + ":" + m.Description)));
			}

			await _userManager.AddToRoleAsync(user, FoodieUserRoles.User.ToString());

			var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var token = EncodingUtils.EncodeAccountEditToken(user.Email, emailConfirmationToken);
			
			await _emailSender.SendAccountConfirmationEmailAsync(email, token);
		}

		public async Task ConfirmAccountAsync(string encodedToken)
		{
			var (email, accountConfirmationToken) = EncodingUtils.DecodeAccountEditToken(encodedToken);

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				throw new AccountConfirmationFailedException("An issue occurred confirming the account");
			}

			var res = await _userManager.ConfirmEmailAsync(user, accountConfirmationToken);
		
			if (!res.Succeeded)
			{
				throw new AccountConfirmationFailedException(string.Join("\n", res.Errors.Select(m => m.Code + ":" + m.Description)));
			}
		}	
		
		public async Task GetResetPasswordTokenAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			//we don't want to give feedback about wether the email is registered
			if (user == null)
			{
				return;
			}

			var resetToken =
				await _userManager.GeneratePasswordResetTokenAsync(user);
			
			await _emailSender.SendPasswordResetEmailAsync(email, resetToken);
		}

		public async Task ResetPasswordReset(string email, string token, string newPassword)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				throw new PasswordResetFailedException();
			}

			var resetOperation = await _userManager.ResetPasswordAsync(user, token, newPassword);
			if (!resetOperation.Succeeded)
			{
				throw new PasswordResetFailedException(string.Join("\n", resetOperation.Errors.Select(m => m.Code + ":" + m.Description)));
			}

			//since we know that the user had access to inbo
			//we can mark the account as confirmed
			if (!user.EmailConfirmed)
			{
				var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				await _userManager.ConfirmEmailAsync(user, confirmationToken);
			}
		}
	}
}

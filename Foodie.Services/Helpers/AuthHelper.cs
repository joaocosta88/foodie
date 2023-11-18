using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Foodie.Services.Helpers {
	public class AuthHelper {
		private readonly SymmetricSecurityKey _authSigningKey;
		private readonly string _tokenIssuer;
		private readonly string _tokenAudience;
		private readonly int _tokenLifetimeInMinutes;

		public AuthHelper(SymmetricSecurityKey authSigningKey, string tokenIssuer, string tokenAudience, int tokenLifetimeInMinutes)
		{
			_authSigningKey = authSigningKey;
			_tokenIssuer = tokenIssuer;
			_tokenAudience = tokenAudience;
			_tokenLifetimeInMinutes = tokenLifetimeInMinutes;
		}

		public JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims)
		=> new JwtSecurityToken(
					issuer: _tokenIssuer,
					audience: _tokenAudience,
					expires: DateTime.Now.AddMinutes(_tokenLifetimeInMinutes),
					claims: claims,
					signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
					);
	}
}

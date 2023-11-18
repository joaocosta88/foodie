namespace Foodie.Services.Dtos {
	public class AuthTokenDto {
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}

using Foodie.Entities.Entities;
using Foodie.Services.Exceptions;
using Foodie.Services.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Foodie.Services.Tests.Services {

	[TestClass]
	public class AuthServiceTests {

		private AuthService authService;
		private Mock<UserManager<FoodieUser>> userManagerMock;

		[TestInitialize]
		public void Init()
		{
			userManagerMock = new Mock<UserManager<FoodieUser>>(Mock.Of<IUserStore<FoodieUser>>(), null, null, null, null, null, null, null, null);

			authService = new AuthService(userManagerMock.Object, null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(AuthenticationFailedException))]
		public async Task LoginAsync_UserDoesNotExist_ThrowsException()
		{
			userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
				.ReturnsAsync((FoodieUser)null);

			await authService.LoginAsync("", "");
		}

		[TestMethod]
		[ExpectedException(typeof(AuthenticationFailedException))]
		public async Task LoginAsync_WrongPassword_ThrowsException()
		{
			userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
				.ReturnsAsync(new FoodieUser());
			userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<FoodieUser>(),
				It.IsAny<string>())).ReturnsAsync(false);

			await authService.LoginAsync("", "");
		}

		[TestMethod]
		[ExpectedException(typeof(AuthenticationFailedException))]
		public async Task LoginAsync_EmailNotConfirmed_ThrowsException()
		{
			var user = new FoodieUser()
			{
				EmailConfirmed = false
			};
			userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
					.ReturnsAsync(user);
			userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<FoodieUser>(),
					It.IsAny<string>())).ReturnsAsync(true);

			await authService.LoginAsync("", "");
		}
	}
}

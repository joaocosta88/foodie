using Foodie.Common;
using Foodie.TripAdvisorApi;
using Foodie.TripAdvisorApi.Http;
using Foodie.Web.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Foodie.TripAdvisorApis.Tests {

	[TestClass]
	public class TripAdvisorApiInvokerTests {

		private TripAdvisorApiInvoker _invoker;

		[TestInitialize]
		public void Init()
		{
			var configuration = new ConfigurationBuilder().AddUserSecrets<HomeController>().Build();
			var apiKey = configuration["TripAdvisorApiKey"];

			Mock<FoodieHttpClientFactory> httpClientFactory = new Mock<FoodieHttpClientFactory>();
			httpClientFactory.Setup(m => m.CreateClient()).Returns(new HttpClient());

			TripAdvisorApiClient httpClient = new TripAdvisorApiClient(httpClientFactory.Object, apiKey);
			TripAdvisorUrlFactory googleApiUrlFactory = new TripAdvisorUrlFactory(apiKey);

			_invoker = new TripAdvisorApiInvoker(httpClient, googleApiUrlFactory);
		}

		[TestMethod]
		public async Task SearchPlace_WithKeyword_ReturnsPlace()
		{
			string input = "caseiro";
			var result = await _invoker.SearchPlacesAsync(input);

			Assert.IsNotNull(result);
		}
	}
}
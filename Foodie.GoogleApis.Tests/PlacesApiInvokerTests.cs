using Foodie.Common;
using Foodie.GoogleApis.Http;
using Foodie.Web.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Foodie.GoogleApis.Tests
{
    [TestClass]
	public class PlacesApiInvokerTests {

		private GooglePlacesApiInvoker? _invoker;

		[TestInitialize]
		public void Init()
		{
			var configuration = new ConfigurationBuilder().AddUserSecrets<HomeController>().Build();
			var apiKey = configuration["GoogleMapsApiKey"];

			Mock<FoodieHttpClientFactory> httpClientFactory = new Mock<FoodieHttpClientFactory>();
			httpClientFactory.Setup(m => m.CreateClient()).Returns(new HttpClient());

			GoogleApiClient googleHttpClient = new GoogleApiClient(httpClientFactory.Object, apiKey);
			GoogleApiUrlFactory googleApiUrlFactory = new GoogleApiUrlFactory(apiKey);

			_invoker = new GooglePlacesApiInvoker(googleHttpClient, googleApiUrlFactory);
		}

		[TestMethod]
		public async Task SearchPlace_WithKeyword_ReturnsPlace()
		{
			string input = "caseiro";
			var result = await _invoker!.SearchPlacesAsync(input);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Places);
			Assert.AreEqual(1, result.Places.Count());

		}
	}
}
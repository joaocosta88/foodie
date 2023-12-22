using Foodie.GoogleApis.Http;
using Foodie.Web.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Foodie.GoogleApis.Tests
{
    [TestClass]
	public class PlacesApiInvokerTests {

		private PlacesApiInvoker? _invoker;

		[TestInitialize]
		public void Init()
		{
			var configuration = new ConfigurationBuilder().AddUserSecrets<HomeController>().Build();
			var apiKey = configuration["GoogleMapsApiKey"];

			Mock<GoogleHttpClientFactory> googleHttpClientFactory = new Mock<GoogleHttpClientFactory>();
			googleHttpClientFactory.Setup(m => m.CreateClient()).Returns(new HttpClient());

			GoogleHttpClient googleHttpClient = new GoogleHttpClient(googleHttpClientFactory.Object, apiKey);
			GoogleApiUrlFactory googleApiUrlFactory = new GoogleApiUrlFactory(apiKey);

			_invoker = new PlacesApiInvoker(googleHttpClient, googleApiUrlFactory);
		}

		[TestMethod]
		public async Task SearchPlace_WithKeyword_ReturnsPlace()
		{
			string input = "caseiro";
			var result = await _invoker!.SearchPlaceAsync(input);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Places);
			Assert.AreEqual(1, result.Places.Count());

		}
	}
}
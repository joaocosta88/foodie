using System.Net.Http;

namespace Foodie.Common {
	public class FoodieHttpClientFactory {
		private readonly IHttpClientFactory _httpClientFactory;

		public FoodieHttpClientFactory() { }

		public FoodieHttpClientFactory(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public virtual HttpClient CreateClient()
			=> _httpClientFactory.CreateClient();
	}
}

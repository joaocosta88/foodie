using Foodie.Common;
using Newtonsoft.Json;

namespace Foodie.TripAdvisorApi.Http {
	public class TripAdvisorApiClient {
		private readonly FoodieHttpClientFactory _httpClientFactory;
		private readonly string _tripAdvisorApiClient;

		public TripAdvisorApiClient(FoodieHttpClientFactory httpClientFactory, string tripAdvisorApiClient)
		{
			_httpClientFactory = httpClientFactory;
			_tripAdvisorApiClient = tripAdvisorApiClient;
		}

		public async Task<T> MakeGetRequest<T>(string url)
		{
			HttpClient client = _httpClientFactory.CreateClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			var response = await client.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(result)!;
		}
	}
}

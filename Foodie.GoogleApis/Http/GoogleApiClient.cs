using Foodie.Common;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Foodie.GoogleApis.Http {

	public class GoogleApiClient
    {
        private readonly FoodieHttpClientFactory _httpClientFactory;
        private readonly string _googleApiKey;

        public GoogleApiClient(FoodieHttpClientFactory httpClientFactory, string googleApiKey)
        {
			_httpClientFactory = httpClientFactory;
            _googleApiKey = googleApiKey;
        }

        public async Task<T> MakeGetRequest<T>(string url, IEnumerable<KeyValuePair<string, string>>? additionalHeader = null)
        {
			HttpClient client = _httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Add("X-Goog-Api-Key", _googleApiKey);
			if (additionalHeader != null)
			{
				foreach (var header in additionalHeader)
				{
					client.DefaultRequestHeaders.Add(header.Key, header.Value);
				}
			}

            var response = await client.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(result)!;
		}

		public async Task<T> MakePostRequestAsync<T>(string url, object payload,
            IEnumerable<KeyValuePair<string, string>>? additionalHeader = null)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", _googleApiKey);

            if (additionalHeader != null)
            {
                foreach (var header in additionalHeader)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
				}
			}

            var response = await client.PostAsJsonAsync(url, payload);
			var result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(result)!;
		}
    }
}

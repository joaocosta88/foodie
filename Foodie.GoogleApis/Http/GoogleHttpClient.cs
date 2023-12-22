using System.Net.Http.Json;

namespace Foodie.GoogleApis.Http
{

    public class GoogleHttpClient
    {
        private readonly GoogleHttpClientFactory _googleHttpClientFactory;
        private readonly string _googleApiKey;

        public GoogleHttpClient(GoogleHttpClientFactory googleHttpClientFactory, string googleApiKey)
        {
            _googleHttpClientFactory = googleHttpClientFactory;
            _googleApiKey = googleApiKey;
        }

        public async Task<HttpResponseMessage> MakePostRequestAsync<T>(string url, T val,
            IEnumerable<KeyValuePair<string, string>>? additionalHeader = null) where T : class
        {
            HttpClient client = _googleHttpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", _googleApiKey);

            if (additionalHeader != null)
            {
                foreach (var header in additionalHeader)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return await client.PostAsJsonAsync(url, val);
        }
    }
}

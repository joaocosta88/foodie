namespace Foodie.GoogleApis.Http
{
    public class GoogleHttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleHttpClientFactory() { }

        public GoogleHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public virtual HttpClient CreateClient()
            => _httpClientFactory.CreateClient();
    }
}

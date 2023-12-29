namespace Foodie.TripAdvisorApi.Http {
	public class TripAdvisorUrlFactory {
		private readonly string _tripAdvisorApiKey;
		private readonly string _language;

		public TripAdvisorUrlFactory(string tripAdvisorApiKey, string language = "pt_PT")
		{
			_tripAdvisorApiKey = tripAdvisorApiKey;
			_language = language;
		}

		public string GetSearchLocationUrl(string searchQuery, string category = "restaurants")
			=> $"https://api.content.tripadvisor.com/api/v1/location/search?key={_tripAdvisorApiKey}&searchQuery={searchQuery}&category={category}&language={_language}";

		public string GetLocationDetailsUrl(string locationId, string currency = "EUR")
			=> $"https://api.content.tripadvisor.com/api/v1/location/{locationId}/details?key={_tripAdvisorApiKey}&language={_language}&currency={currency}";
	}
}

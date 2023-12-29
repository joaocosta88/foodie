using Foodie.Common;
using Foodie.GoogleApis.Dto;
using Foodie.GoogleApis.Http;
using Newtonsoft.Json;

namespace Foodie.GoogleApis {
	public class GooglePlacesApiInvoker {
		private readonly GoogleApiClient _googleHttpClient;
		private readonly GoogleApiUrlFactory _googleApiUrlFactory;

		public GooglePlacesApiInvoker(GoogleApiClient googleHttpClient, GoogleApiUrlFactory googleApiUrlFactory)
		{
			_googleHttpClient = googleHttpClient;
			_googleApiUrlFactory = googleApiUrlFactory;
		}

		public async Task<GooglePlaceSearchResult> SearchPlacesAsync(string input)
		{
			var url = _googleApiUrlFactory.GetPlacesV2ApiSearchUrl();

			return await _googleHttpClient.MakePostRequestAsync<GooglePlaceSearchResult>(url, new
			{
				textQuery = input
			}, new Dictionary<string, string> {
				{ "X-Goog-FieldMask", "places.displayName,places.name,places.formattedAddress,places.addressComponents,places.shortFormattedAddress,places.id,places.location" }});
		}

		public async Task<GooglePlace> GetPlaceDetails(string placeId)
		{
			var url = _googleApiUrlFactory.GetPlaceDetails(placeId);

			return await _googleHttpClient.MakeGetRequest<GooglePlace>(url, new Dictionary<string, string> {
				{ "X-Goog-FieldMask", "places.displayName,places.name,places.formattedAddress,places.shortFormattedAddress,places.id,places.location" }});

		}
	}
}

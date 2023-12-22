using Foodie.Common;
using Foodie.GoogleApis.Dto;
using Foodie.GoogleApis.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Foodie.GoogleApis
{
    public class PlacesApiInvoker {
		private readonly GoogleHttpClient _googleHttpClient;
		private readonly GoogleApiUrlFactory _googleApiUrlFactory;

		public PlacesApiInvoker(GoogleHttpClient googleHttpClient, GoogleApiUrlFactory googleApiUrlFactory)
		{
			_googleHttpClient = googleHttpClient;
			_googleApiUrlFactory = googleApiUrlFactory;
		}

		public async Task<GooglePlaceSearchResult> SearchPlaceAsync(string input)
		{
			var url = _googleApiUrlFactory.GetPlacesV2ApiSearchUrl();

			var response = await _googleHttpClient.MakePostRequestAsync(url, new
			{
				textQuery = input
			}, new Dictionary<string, string> {
				{ "X-Goog-FieldMask", "places.displayName,places.formattedAddress,places.id,places.location" }});

			response.EnsureSuccessStatusCode().WriteRequestToConsole();

			var result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<GooglePlaceSearchResult>(result);
			
		}
	}
}

using Foodie.TripAdvisorApi.Dto;
using Foodie.TripAdvisorApi.Http;

namespace Foodie.TripAdvisorApi {
	public class TripAdvisorApiInvoker {
		private readonly TripAdvisorApiClient _tripAdvisorApiClient;
		private readonly TripAdvisorUrlFactory _tripAdvisorUrlClient;

		public TripAdvisorApiInvoker(TripAdvisorApiClient tripAdvisorApiClient, TripAdvisorUrlFactory tripAdvisorUrlClient)
		{
			_tripAdvisorApiClient = tripAdvisorApiClient;
			_tripAdvisorUrlClient = tripAdvisorUrlClient;
		}

		public async Task<IEnumerable<TripAdvisorLocation>> SearchPlacesAsync(string input)
		{
			var url = _tripAdvisorUrlClient.GetSearchLocationUrl(input);

			var result = await _tripAdvisorApiClient.MakeGetRequest<TripAdvisorLocationSearchResult>(url);

			List<Task<TripAdvisorLocation>> taskList = new();
			foreach (var entry in result.Data)
			{
				var task = GetLocationDetails(entry.LocationId);
				taskList.Add(task);
			}

			await Task.WhenAll(taskList);
			return taskList.Select(m => m.Result);
		}

		public async Task<TripAdvisorLocation> GetLocationDetails(string locationId)
		{
			var url = _tripAdvisorUrlClient.GetLocationDetailsUrl(locationId);
			return await _tripAdvisorApiClient.MakeGetRequest<TripAdvisorLocation>(url);
		}
	}
}

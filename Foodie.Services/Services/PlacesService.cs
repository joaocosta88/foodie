using AutoMapper;
using Foodie.GoogleApis;
using Foodie.GoogleApis.Dto;
using Foodie.Services.Dtos;
using Foodie.TripAdvisorApi;
using Foodie.TripAdvisorApi.Dto;
using GeoCoordinatePortable;


namespace Foodie.Services.Services {
	public class PlacesService {

		private readonly GooglePlacesApiInvoker _googlePlacesApiInvoker;
		private readonly TripAdvisorApiInvoker _tripAdvisorApiInvoker;
		private readonly IMapper _mapper;

		public PlacesService(IMapper mapper, GooglePlacesApiInvoker googlePlacesApiInvoker, TripAdvisorApiInvoker tripAdvisorApiInvoker)
		{
			_googlePlacesApiInvoker = googlePlacesApiInvoker;
			_tripAdvisorApiInvoker = tripAdvisorApiInvoker;
			_mapper = mapper;
		}

		public  async Task<IEnumerable<Place>> SearchPlaceAsync(string searchQuery)
		{
			var googlePlacesTask = _googlePlacesApiInvoker.SearchPlacesAsync(searchQuery);
			var tripAdvisorTask = _tripAdvisorApiInvoker.SearchPlacesAsync(searchQuery);

			await Task.WhenAll(googlePlacesTask, tripAdvisorTask);

			return MergeResults(googlePlacesTask.Result.Places, tripAdvisorTask.Result);
		}

		private IEnumerable<Place> MergeResults(IEnumerable<GooglePlace> googlePlaces, IEnumerable<TripAdvisorLocation> tripAdvisorLocations)
		{
			var result = new List<Place>();

			foreach(var  place in googlePlaces)
			{
				var postalCode = place.AddressComponents.FirstOrDefault(m => m.Types.Contains("postal_code")).ShortText;
				var correctLocation = tripAdvisorLocations.FirstOrDefault(m => m.AddressObj.Postalcode == postalCode);
				if (correctLocation == null)
				{
					var googlePlaceCoordinates = new GeoCoordinate(Double.Parse(place.Location.Latitude), Double.Parse(place.Location.Longitude));
					var tripAdvisorPlaceCoordinates = tripAdvisorLocations.Select(m => new GeoCoordinate(Double.Parse(m.Latitude), Double.Parse(m.Longitude)));

					foreach(var ta in tripAdvisorPlaceCoordinates)
					{
						if (ta.GetDistanceTo(googlePlaceCoordinates) < 100)
						{
							correctLocation = tripAdvisorLocations.FirstOrDefault(m => m.Latitude == ta.Latitude.ToString() && m.Longitude == ta.Longitude.ToString());
						}
					}
				}

				var mappedResult = _mapper.Map<Place>((place, correctLocation));
				result.Add(mappedResult);
			}

			return result;
		}

		public async Task<Place> GetPlaceDetails(string googlePlaceId)
		{
			var googlePlace = await _googlePlacesApiInvoker.GetPlaceDetails(googlePlaceId);
			return _mapper.Map<Place>(googlePlace);

		}
	}
}

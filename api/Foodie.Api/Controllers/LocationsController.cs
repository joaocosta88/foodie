using Foodie.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Api.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class LocationsController : ControllerBase {

		private readonly ILogger<LocationsController> _logger;
		private readonly LocationService _locationService;

		public LocationsController(ILogger<LocationsController> logger, LocationService locationService)
		{
			_logger = logger;
			_locationService = locationService;
		}

		[HttpGet]
		public async Task<object> GetAsync()
		{
			await _locationService.AddLocationAsync(new Services.Dtos.LocationDto()
			{
				Coordinates = "asda",
				Rating="asdadasd"
			});

			return await _locationService.GetLocationsAsync();
		}
	}
}
using Foodie.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Web.Controllers {
	public class MapsController : Controller {

		private readonly ILogger<MapsController> _logger;
		private readonly PlacesService _placesService;
		private readonly IConfiguration _configuration;

		public MapsController(ILogger<MapsController> logger, IConfiguration configuration, PlacesService placesService)
		{
			_configuration = configuration;
			_logger = logger;
			_placesService = placesService;
		}

		public async Task<IActionResult> IndexAsync(string searchCriteria)
		{
			ViewData["GMapsKey"] = _configuration["GoogleMapsApiKey"];

			var places = await _placesService.SearchPlaceAsync(searchCriteria);
			return View(places.ToList());
		}

		public async Task<IActionResult> GetDetails(string googlePlaceId)
		{
			ViewData["GMapsKey"] = _configuration["GoogleMapsApiKey"];

			var place = await _placesService.GetPlaceDetails(googlePlaceId);
			return View(place);
		}
	}
}

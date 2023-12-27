using Foodie.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Web.ViewComponents {
	public class MapsViewComponent : ViewComponent {

		private readonly IConfiguration _configuration;
		private readonly PlacesService _placesService;
		public MapsViewComponent(IConfiguration configuration, PlacesService placesService)
		{
			_configuration = configuration;
			_placesService = placesService;
		}

		public async Task<IViewComponentResult> InvokeAsync(string searchString = "")
		{
			ViewData["GMapsKey"] = _configuration["GoogleMapsApiKey"];

			return View();
		}
	}
}

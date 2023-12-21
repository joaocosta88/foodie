using Microsoft.AspNetCore.Mvc;

namespace Foodie.Web.ViewComponents {
	public class MapsViewComponent : ViewComponent {

		private readonly IConfiguration _configuration;
		public MapsViewComponent(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewData["GMapsKey"] = _configuration["GoogleMapsApiKey"];

			return View();
		}
	}
}

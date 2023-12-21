using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Web.Controllers {
	public class MapsController : Controller {

		private readonly ILogger<MapsController> _logger;

		public MapsController(ILogger<MapsController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{

			return View();
		}
	}
}

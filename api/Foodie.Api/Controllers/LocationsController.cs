using AutoMapper;
using Foodie.Api.Models;
using Foodie.Services.Dtos;
using Foodie.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class LocationsController : ControllerBase {

		private readonly ILogger<LocationsController> _logger;
		private readonly IMapper _mapper;
		private readonly LocationService _locationService;

		public LocationsController(ILogger<LocationsController> logger, IMapper mapper, LocationService locationService)
		{
			_logger = logger;
			_mapper = mapper;
			_locationService = locationService;
		}

		[HttpGet]
		public async Task<IEnumerable<LocationModel>> GetLocationsAsync()
		{
			var locations = await _locationService.GetLocationsAsync();
			return _mapper.Map<IEnumerable<LocationModel>>(locations);
		}

		[HttpPost]
		[Authorize]
		public async Task<LocationModel> CreateLocationAsync(CreateLocationModel model)
		{
			var location = _mapper.Map<LocationDto>(model);
			location = await _locationService.AddLocationAsync(location);

			return _mapper.Map<LocationModel>(location);
		}
	}
}
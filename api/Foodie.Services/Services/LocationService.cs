using AutoMapper;
using Foodie.Entities.Entities;
using Foodie.Entities.Repositories;
using Foodie.Services.Dtos;

namespace Foodie.Services.Services {
	public class LocationService {
		private readonly LocationRepository _locationRepository;
		private readonly IMapper _mapper;

		public LocationService(IMapper mapper, LocationRepository locationRepository)
		{
			_mapper = mapper;
			_locationRepository = locationRepository;
		}

		public async Task<LocationDto> AddLocationAsync(LocationDto dto)
		{
			Location location = _mapper.Map<Location>(dto);
			location = await _locationRepository.AddAsync(location);

			return _mapper.Map<LocationDto>(location);
		}

		public async Task<IEnumerable<LocationDto>> GetLocationsAsync()
		{
			var res = await _locationRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<LocationDto>>(res);
		}
	}
}

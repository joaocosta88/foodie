using AutoMapper;
using Foodie.Entities.Entities;
using Foodie.Entities.Repositories;
using Foodie.Services.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Services.Services {
	public class LocationService {
		private readonly LocationRepository _locationRepository;
		private readonly IMapper _mapper;

		public LocationService(IMapper mapper, LocationRepository locationRepository)
		{
			_mapper = mapper;
			_locationRepository = locationRepository;
		}

		public async Task AddLocationAsync(LocationDto dto)
		{
			Location location = _mapper.Map<Location>(dto);
			await _locationRepository.AddAsync(location);
		}

		public async Task<IEnumerable<LocationDto>> GetLocationsAsync()
		{
			var res = await _locationRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<LocationDto>>(res);
		}
	}
}

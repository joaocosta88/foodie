using AutoMapper;
using Foodie.Entities.Entities;
using Foodie.Services.Dtos;

namespace Foodie.Services {
	public class FoodieServicesMappingProfile : Profile {

		public FoodieServicesMappingProfile()
		{
			CreateMap<LocationDto, Location>().ReverseMap();
		}
	}
}

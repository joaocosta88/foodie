using AutoMapper;
using Foodie.Api.Models;
using Foodie.Services.Dtos;

namespace Foodie.Api {
	public class FoodieApiMappingProfile : Profile {
		public FoodieApiMappingProfile()
		{
			CreateMap<CreateLocationModel, LocationDto>();
			CreateMap<LocationModel, LocationDto>().ReverseMap();
		}
	}
}

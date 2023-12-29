using AutoMapper;
using Foodie.Entities.Entities;
using Foodie.GoogleApis.Dto;
using Foodie.Services.Dtos;
using Foodie.TripAdvisorApi.Dto;

namespace Foodie.Services {
	public class FoodieServicesMappingProfile : Profile {

		public FoodieServicesMappingProfile()
		{
			CreateMap<LocationDto, Location>().ReverseMap();

			CreateMap<(GooglePlace googlePlace, TripAdvisorLocation tripAdvisorLocation), Place>()
				.ForMember(dest => dest.GooglePlaceId, opt => opt.MapFrom(m => m.googlePlace.Id))
				.ForMember(dest => dest.TripAdvisorLocationId, opt => opt.MapFrom(m => m.tripAdvisorLocation.LocationId))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(m => m.googlePlace.FormattedAddress))
				.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(m => m.googlePlace.DisplayName.Text))
				.ForMember(dest => dest.Lat, opt => opt.MapFrom(m => m.googlePlace.Location.Latitude))
				.ForMember(dest => dest.Long, opt => opt.MapFrom(m => m.googlePlace.Location.Longitude))
				.ForMember(dest => dest.TripAdvisorRating, opt => opt.MapFrom(m => m.tripAdvisorLocation.RatingImageUrl));
		}
	}
}

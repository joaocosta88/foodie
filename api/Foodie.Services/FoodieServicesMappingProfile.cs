using AutoMapper;
using Foodie.Entities.Entities;
using Foodie.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Services {
	public class FoodieServicesMappingProfile : Profile {

		public FoodieServicesMappingProfile()
		{
			CreateMap<LocationDto, Location>().ReverseMap();
		}
	}
}

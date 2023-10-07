using AutoMapper;
using Foodie.Entities.Entities;
using Google.Cloud.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Entities {
	public class FoodieEntitiesMappingProfile : Profile {
		public FoodieEntitiesMappingProfile()
		{
			CreateMap<DocumentSnapshot, Location>();
		}
	}
}

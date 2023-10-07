using Foodie.Entities;
using Foodie.Entities.Repositories;
using Foodie.Services.Services;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace Foodie.Api {
	public static class FoodieServices {
		public static void RegisterFoodieServices(this IServiceCollection services, IConfiguration configuration)
		{
			// define your specific services
			services.AddScoped<LocationService>();
			services.AddScoped<LocationRepository>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddDbContext<FoodieDbContext>(opt => opt.UseMySQL(configuration.GetConnectionString("FoodieDatabase")));

            services.AddScoped<FirestoreDb>(m =>
			{
				return new FirestoreDbBuilder
				{
					ProjectId = configuration["Firebase:ProjectId"],
					EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOrProduction
				}.Build();
			});
		}
	}
}

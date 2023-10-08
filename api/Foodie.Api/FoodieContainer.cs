using Foodie.Emails;
using Foodie.Entities;
using Foodie.Entities.Entities;
using Foodie.Entities.Repositories;
using Foodie.Services.Helpers;
using Foodie.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Foodie.Api {
	public static class FoodieContainer {
		public static void RegisterFoodieServices(this IServiceCollection services, IConfiguration configuration)
		{
			// define your specific services
			services.AddScoped<LocationService>();
			services.AddScoped<LocationRepository>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddDbContext<FoodieDbContext>(opt => opt.UseMySQL(configuration.GetConnectionString("FoodieDatabase")));

			services.AddScoped(m =>
			{
				var sendGridApiKey = configuration["Emails:SendGridApiKey"];
				var fromEmailAddress = configuration["Emails:From"];
				var aliasEmailAddress = configuration["Emails:Alias"];

				return new EmailSender(sendGridApiKey, fromEmailAddress, aliasEmailAddress);
			});
		}

		public static void RegisterAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<AuthService>();
			services.AddScoped(opt =>
			new AuthHelper(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"])),
				configuration["Auth:ValidIssuer"],
				configuration["Auth:ValidAudience"],
				Int32.Parse(configuration["Auth:TokenLifetimeInMinutes"])));
			services.AddIdentity<FoodieUser, IdentityRole>(
				options =>
				{
					options.SignIn.RequireConfirmedEmail = true;
				})
				.AddEntityFrameworkStores<FoodieDbContext>()
				.AddDefaultTokenProviders();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidAudience = configuration["Auth:ValidAudience"],
						ValidIssuer = configuration["Auth:ValidIssuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"]))
					};
				});
		}
	}
}

using Foodie.Common;
using Foodie.Emails;
using Foodie.Entities;
using Foodie.Entities.Entities;
using Foodie.Entities.Repositories;
using Foodie.GoogleApis;
using Foodie.GoogleApis.Http;
using Foodie.Services.Helpers;
using Foodie.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

namespace Foodie.Web {
	public static class FoodieContainer {
		public static void RegisterFoodieServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<LocationService>();
			services.AddScoped<PlacesApiInvoker>();
			services.AddScoped<GoogleApiClient>(m =>
			{
				var googleHttpClientFactory = m.GetRequiredService<FoodieHttpClientFactory>();
				var googleApiKey = configuration["GoogleMapsApiKey"];

				return new GoogleApiClient(googleHttpClientFactory, googleApiKey);
			});
			services.AddScoped<FoodieHttpClientFactory>();
			services.AddScoped(opt => new GoogleApiUrlFactory(configuration["GoogleMapsApiKey"]!));
			services.AddScoped<PlacesService>();
			services.AddScoped<LocationRepository>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.Configure<AuthMessageSenderOptions>(configuration);

			var connString = configuration.GetConnectionString("FoodieDatabase");
			services.AddDbContext<FoodieDbContext>(opt => opt.UseMySql(connString, ServerVersion.AutoDetect(connString)));
			services.AddHealthChecks().AddMySql(connString);
			services.AddDatabaseDeveloperPageExceptionFilter();

			//emails
			services.Configure<AuthMessageSenderOptions>(configuration.GetSection("Emails"));
			services.AddScoped<EmailUrlFactory>();
			services.AddScoped<EmailTemplateFactory>();
			services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
			services.AddScoped<Foodie.Emails.IEmailSender, EmailSender>();
		}

		public static void RegisterAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<AuthService>();
			services.AddScoped(opt =>
			new AuthHelper(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"])),
				configuration["Auth:ValidIssuer"],
				configuration["Auth:ValidAudience"],
				configuration.GetValue<int>("Auth:TokenLifetimeInMinutes")));

			services.AddDefaultIdentity<FoodieUser>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;
				options.SignIn.RequireConfirmedAccount = true;
			})
				.AddEntityFrameworkStores<FoodieDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings.
				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = false;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

				options.LoginPath = "/Identity/Account/Login";
				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			//services.AddAuthentication(options =>
			//{
			//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			//	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			//})
			//.AddJwtBearer(options =>
			//{
			//	options.SaveToken = true;
			//	options.RequireHttpsMetadata = false;
			//	options.TokenValidationParameters = new TokenValidationParameters()
			//	{
			//		ValidateIssuer = true,
			//		ValidateAudience = true,
			//		ValidAudience = configuration["Auth:ValidAudience"],
			//		ValidIssuer = configuration["Auth:ValidIssuer"],
			//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"]))
			//	};
			//});
		}
	}
}

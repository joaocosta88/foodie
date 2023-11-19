using Foodie.Entities;
using Foodie.Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace Foodie.Api {
	public class SeedData {
		public static async Task InitializeAsync(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var provider = scope.ServiceProvider;
			var context = provider.GetRequiredService<FoodieDbContext>();
			var userManager = provider.GetRequiredService<UserManager<FoodieUser>>();
			var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

			// automigration 
			await SeedRolesAsync(roleManager);
		}

		private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			if (!await roleManager.RoleExistsAsync(FoodieUserRoles.Admin.ToString()))
				await roleManager.CreateAsync(new IdentityRole(FoodieUserRoles.Admin.ToString()));
			if (!await roleManager.RoleExistsAsync(FoodieUserRoles.User.ToString()))
				await roleManager.CreateAsync(new IdentityRole(FoodieUserRoles.User.ToString()));
		}
	}
}

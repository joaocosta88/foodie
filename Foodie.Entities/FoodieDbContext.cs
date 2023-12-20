using Foodie.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Entities {
	public class FoodieDbContext(DbContextOptions<FoodieDbContext> options) : IdentityDbContext<FoodieUser>(options) {
		public DbSet<Location> Locations { get; set; }
    }
}

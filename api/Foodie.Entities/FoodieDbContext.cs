using Foodie.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Entities {
	public class FoodieDbContext : IdentityDbContext<FoodieUser> {
		public DbSet<Location> Locations { get; set; }

		public FoodieDbContext(DbContextOptions<FoodieDbContext> options) : base(options) { }
	}
}

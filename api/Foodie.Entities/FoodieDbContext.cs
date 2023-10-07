using Foodie.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Entities {
	public class FoodieDbContext : DbContext {
		public DbSet<Location> Locations { get; set; }

		public FoodieDbContext(DbContextOptions<FoodieDbContext> options) : base(options) { }
	}
}

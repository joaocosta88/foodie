using Foodie.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Entities.Repositories {
	public class LocationRepository {

		private readonly FoodieDbContext _foodieDbContext;
		public LocationRepository(FoodieDbContext foodieDbContext)
		{
			_foodieDbContext = foodieDbContext;
		}

		public async Task<Location> AddAsync(Location location)
		{
			await _foodieDbContext.Locations.AddAsync(location);
			await _foodieDbContext.SaveChangesAsync();
			return location;
		}

		public async Task<IEnumerable<Location>> GetAllAsync()
		=> await _foodieDbContext.Locations.ToListAsync();
	}
}

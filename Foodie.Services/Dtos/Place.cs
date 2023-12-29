namespace Foodie.Services.Dtos {
	public class Place {
		public string GooglePlaceId { get; set; }
		public string TripAdvisorLocationId { get; set; }
		public string DisplayName { get; set; }
		public string Address { get; set; }
		public string Lat { get; set; }
		public string Long { get; set; }
		public string TripAdvisorRating { get; set; }
	}
}

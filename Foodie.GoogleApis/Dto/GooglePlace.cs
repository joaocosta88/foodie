namespace Foodie.GoogleApis.Dto {
	public class GooglePlace {
		public string Id { get; set; }
		public string FormattedAddress { get; set; }
		public GooglePlaceLocation Location { get; set; }

		public class GooglePlaceLocation {
			public string Latitude { get; set; }
			public string Longitude { get; set; }
		}
	}
}

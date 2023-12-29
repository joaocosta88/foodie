namespace Foodie.GoogleApis.Dto {
	public class GooglePlace {
		public string Id { get; set; }
		public string Name { get; set; }
		public GooglePlaceLocalizedText DisplayName { get; set; }
		public string FormattedAddress { get; set; }
		public string ShortFormattedAddress { get; set; }
		public GooglePlaceLocation Location { get; set; }
		public IEnumerable<GooglePlaceAddress> AddressComponents { get; set; }

		public class GooglePlaceAddress
		{
			public string LongText { get; set; }
			public string ShortText { get; set; }
			public string LanguageCode { get; set; }
			public string[] Types { get; set; }
		}

		public class GooglePlaceLocation {
			public string Latitude { get; set; }
			public string Longitude { get; set; }
		}

		public class GooglePlaceLocalizedText {
			public string Text { get; set; }
			public string LanguageCode { get; set; }
		}
	}
}

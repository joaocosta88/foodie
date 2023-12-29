using Newtonsoft.Json;

namespace Foodie.TripAdvisorApi.Dto {
	public class TripAdvisorLocation {
		[JsonProperty("location_id")]
		public string LocationId { get; set; }
		public string Name { get; set; }
		[JsonProperty("web_url")]
		public string WebUrl { get; set; }
		[JsonProperty("address_obj")]
		public TripAdvisorAddress AddressObj { get; set; }
		public Ancestor[] Ancestors { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string Timezone { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		[JsonProperty("write_review")]
		public string WriteReviewUrl { get; set; }
		[JsonProperty("ranking_data")]
		public RankingData RankingInformation { get; set; }
		public string Rating { get; set; }
		[JsonProperty("rating_image_url")]
		public string RatingImageUrl { get; set; }
		[JsonProperty("num_reviews")]
		public int NumReviews { get; set; }
		[JsonProperty("review_rating_count")]
		public Dictionary<string, long> ReviewRatingCount { get; set; }
		[JsonProperty("subratings")]
		public Dictionary<string, Subrating> Subratings { get; set; }
		[JsonProperty("photo_count")]
		public int PhotoCount { get; set; }
		[JsonProperty("see_all_photos")]
		public string SeeAllPhotosUrl { get; set; }
		[JsonProperty("price_level")]
		public string PriceLevel { get; set; }
		[JsonProperty("hours")]
		public Hours WorkingHours { get; set; }
		public LocalizedLabel[] Cuisine { get; set; }
		public LocalizedLabel Category { get; set; }
		public LocalizedLabel[] Subcategory { get; set; }
		[JsonProperty("neighborhood_info")]
		public object[] NeighborhoodInfo { get; set; }
		public Subrating[] TripTypes { get; set; }
		public Award[] Awards { get; set; }

		public class TripAdvisorAddress {
			public string Street1 { get; set; }
			public string City { get; set; }
			public string Country { get; set; }
			public string Postalcode { get; set; }
			public string AddressString { get; set; }
		}

		public class Ancestor {
			public string Level { get; set; }
			public string Name { get; set; }
			[JsonProperty("location_id")]
			public string LocationId { get; set; }
		}

		public class Award {
			[JsonProperty("award_type")]
			public string AwardType { get; set; }
			public int Year { get; set; }
			public Images Images { get; set; }
			public object[] Categories { get; set; }
			[JsonProperty("display_name")]
			public string DisplayName { get; set; }
		}

		public class Images {
			public Uri Tiny { get; set; }
			public Uri Small { get; set; }
			public Uri Large { get; set; }
		}

		public class LocalizedLabel {
			public string Name { get; set; }

			[JsonProperty("localized_name")]
			public string LocalizedName { get; set; }
		}

		public partial class Hours {
			[JsonProperty("periods")]
			public Period[] Periods { get; set; }

			[JsonProperty("weekday_text")]
			public string[] WeekdayText { get; set; }
		}

		public partial class Period {
			public TripAdvisorDayTimePeriod Open { get; set; }
			public TripAdvisorDayTimePeriod Close { get; set; }
		}

		public partial class TripAdvisorDayTimePeriod {
			public int Day { get; set; }
			public string Time { get; set; }
		}

		public partial class RankingData {
			[JsonProperty("geo_location_id")]
			public string GeoLocationId { get; set; }
			[JsonProperty("ranking_string")]
			public string RankingString { get; set; }
			[JsonProperty("geo_location_name")]
			public string GeoLocationName { get; set; }
			[JsonProperty("ranking_out_of")]
			public string RankingOutOf { get; set; }
			public string Ranking { get; set; }
		}

		public partial class Subrating {
			public string Name { get; set; }
			[JsonProperty("localized_name")]
			public string LocalizedName { get; set; }
			[JsonProperty("rating_image_url")]
			public string RatingImageUrl { get; set; }
			public string Value { get; set; }
		}
	}
}

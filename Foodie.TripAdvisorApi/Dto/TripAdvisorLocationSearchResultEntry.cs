using Newtonsoft.Json;

namespace Foodie.TripAdvisorApi.Dto {
	public class TripAdvisorLocationSearchResultEntry {
		[JsonProperty("location_id")]
		public string LocationId { get; set; }
		public string Name { get; set; }
		[JsonProperty("address_obj")]
		public AddressObject AddressObj { get; set; }

		public class AddressObject
		{
			[JsonProperty("street1")]
			public string Street { get; set; }
			public string City { get; set; }
			public string Country { get; set; }
			[JsonProperty("postalcode")]
			public string PostalCode { get; set; }
			[JsonProperty("address_string")]
			public string AddressString { get; set; }
		}
	}
}

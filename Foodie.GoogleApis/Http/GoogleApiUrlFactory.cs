namespace Foodie.GoogleApis.Http
{
    public class GoogleApiUrlFactory
    {
        private readonly string _googleApiKey;
        public GoogleApiUrlFactory(string googleApiKey)
        {
            _googleApiKey = googleApiKey;
        }

        public string GetPlacesV1ApiSearchUrl(string input)
        => $"https://maps.googleapis.com/maps/api/place/autocomplete/json" +
            $"?input={input}" +
            $"&types=establishment" +
            $"&key={_googleApiKey}";

        public string GetPlacesV2ApiSearchUrl()
            => "https://places.googleapis.com/v1/places:searchText";
    }
}

namespace FlightFinderApi.Models
{

    public class IataResponse
    {
        public IataDatum[] data { get; set; }

    }

    public class IataDatum
    {
        public string code { get; set; }
        public string name { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string countryCode { get; set; }
    }

}


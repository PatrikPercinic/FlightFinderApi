namespace FlightFinderApi.Models
{
    public class FlightSearchRequest
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int PassengerCount { get; set; }
        public string Currency { get; set; }
    }
}

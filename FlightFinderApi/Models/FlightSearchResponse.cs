﻿namespace FlightFinderApi.Models
{

    public class FlightSearchResponse
    {
        public Meta meta { get; set; }
        public Datum[] data { get; set; }
        public Dictionaries dictionaries { get; set; }
    }

    public class Meta
    {
        public int count { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
    }

    public class Dictionaries
    {
        public Locations locations { get; set; }
        public Aircraft aircraft { get; set; }
        public Currencies currencies { get; set; }
        public Carriers carriers { get; set; }
    }

    public class Locations
    {
        public BKK BKK { get; set; }
        public VIE VIE { get; set; }
        public LHR LHR { get; set; }
        public SYD SYD { get; set; }
    }

    public class BKK
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }

    public class VIE
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }

    public class LHR
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }

    public class SYD
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }

    public class Aircraft
    {
        public string _772 { get; set; }
        public string _388 { get; set; }
        public string _359 { get; set; }
        public string _32N { get; set; }
    }

    public class Currencies
    {
        public string EUR { get; set; }
    }

    public class Carriers
    {
        public string TG { get; set; }
        public string OS { get; set; }
        public string QF { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public bool instantTicketingRequired { get; set; }
        public bool nonHomogeneous { get; set; }
        public bool oneWay { get; set; }
        public bool isUpsellOffer { get; set; }
        public string lastTicketingDate { get; set; }
        public string lastTicketingDateTime { get; set; }
        public int numberOfBookableSeats { get; set; }
        public Itinerary[] itineraries { get; set; }
        public Price price { get; set; }
        public Pricingoptions pricingOptions { get; set; }
        public string[] validatingAirlineCodes { get; set; }
        public Travelerpricing[] travelerPricings { get; set; }
    }

    public class Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string _base { get; set; }
        public Fee[] fees { get; set; }
        public string grandTotal { get; set; }
    }

    public class Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }

    public class Pricingoptions
    {
        public string[] fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }

    public class Itinerary
    {
        public string duration { get; set; }
        public Segment[] segments { get; set; }
    }

    public class Segment
    {
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
        public string carrierCode { get; set; }
        public string number { get; set; }
        public Aircraft1 aircraft { get; set; }
        public Operating operating { get; set; }
        public string duration { get; set; }
        public string id { get; set; }
        public int numberOfStops { get; set; }
        public bool blacklistedInEU { get; set; }
        public Stop[] stops { get; set; }
    }

    public class Departure
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }

    public class Arrival
    {
        public string iataCode { get; set; }
        public DateTime at { get; set; }
        public string terminal { get; set; }
    }

    public class Aircraft1
    {
        public string code { get; set; }
    }

    public class Operating
    {
        public string carrierCode { get; set; }
    }

    public class Stop
    {
        public string iataCode { get; set; }
        public string duration { get; set; }
        public DateTime arrivalAt { get; set; }
        public DateTime departureAt { get; set; }
    }

    public class Travelerpricing
    {
        public string travelerId { get; set; }
        public string fareOption { get; set; }
        public string travelerType { get; set; }
        public Price1 price { get; set; }
        public Faredetailsbysegment[] fareDetailsBySegment { get; set; }
    }

    public class Price1
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string _base { get; set; }
    }

    public class Faredetailsbysegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string brandedFareLabel { get; set; }
        public string _class { get; set; }
        public Includedcheckedbags includedCheckedBags { get; set; }
        public Amenity[] amenities { get; set; }
    }

    public class Includedcheckedbags
    {
        public int quantity { get; set; }
        public int weight { get; set; }
        public string weightUnit { get; set; }
    }

    public class Amenity
    {
        public string description { get; set; }
        public bool isChargeable { get; set; }
        public string amenityType { get; set; }
        public Amenityprovider amenityProvider { get; set; }
    }

    public class Amenityprovider
    {
        public string name { get; set; }
    }


}

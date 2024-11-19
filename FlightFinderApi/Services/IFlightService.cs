using FlightFinderApi.Models;

namespace FlightFinderApi.Services
{
    public interface IFlightService
    {
        Task<FlightSearchResponse> SearchFlightsAsync(FlightSearchRequest request);
    }
}

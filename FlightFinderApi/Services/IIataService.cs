using FlightFinderApi.Models;

namespace FlightFinderApi.Services
{
    public interface IIataService
    {
        Task<IataResponse> GetIataCodesAsync();
    }
}

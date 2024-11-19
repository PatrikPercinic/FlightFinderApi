using FlightFinderApi.Models;
using FlightFinderApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightFinderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IIataService _iataService;

        public FlightsController(IFlightService flightService, IIataService IataService)
        {
            _flightService = flightService;
            _iataService = IataService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<FlightSearchResponse>> SearchFlights([FromQuery] FlightSearchRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.DepartureAirport) || string.IsNullOrEmpty(request.ArrivalAirport))
            {
                return BadRequest("Missing required search parameters.");
            }

            var flights = await _flightService.SearchFlightsAsync(request);

            if (flights == null || flights.data.Count() == 0)
            {
                return NotFound("No flights found for the specified criteria.");
            }

            return Ok(flights);
        }

        [HttpGet("iata")]
        public async Task<ActionResult<IataResponse>> IataCodes()
        {

            var codes = await _iataService.GetIataCodesAsync();

            if (codes == null || codes.data.Count() == 0)
            {
                return NotFound("No flights found for the specified criteria.");
            }

            return Ok(codes);
        }
    }
}

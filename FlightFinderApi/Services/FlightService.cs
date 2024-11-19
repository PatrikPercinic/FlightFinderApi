using FlightFinderApi.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlightFinderApi.Services
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TokenService _tokenService;

        public FlightService(HttpClient httpClient, TokenService tokenService, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _cache = cache;
        }

        public async Task<FlightSearchResponse> SearchFlightsAsync(FlightSearchRequest request)
        {
            var cacheKey = GenerateCacheKey(request);

            if (_cache.TryGetValue(cacheKey, out FlightSearchResponse cachedFlightData))
            {
                return cachedFlightData;
            }

            var accessToken = await _tokenService.GetAccessTokenAsync();
            if (accessToken == null)
            {
                throw new HttpRequestException("Failed to retrieve access token.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var flightSearchUrl = "https://test.api.amadeus.com/v2/shopping/flight-offers"; //TODO: Move to configuration

            var queryParams = new Dictionary<string, string>
            {
                { "originLocationCode", request.DepartureAirport },
                { "destinationLocationCode", request.ArrivalAirport },
                { "departureDate", request.DepartureDate.ToString("yyyy-MM-dd") },
                { "adults", request.PassengerCount.ToString() },
                { "currencyCode", request.Currency }
            };

            if (request.ReturnDate.HasValue)
            {
                queryParams["returnDate"] = request.ReturnDate.Value.ToString("yyyy-MM-dd");
            }


            var urlWithParams = QueryHelpers.AddQueryString(flightSearchUrl, queryParams);

            var response = await _httpClient.GetAsync(urlWithParams);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Flight search failed with status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var flightData = JsonSerializer.Deserialize<FlightSearchResponse>(responseContent);

            if (flightData != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _cache.Set(cacheKey, flightData, cacheEntryOptions);
            }

            return flightData ?? new FlightSearchResponse();
        }
        private string GenerateCacheKey(FlightSearchRequest request)
        {
            var key = $"{request.DepartureAirport}-{request.ArrivalAirport}-{request.DepartureDate:yyyyMMdd}-{request.PassengerCount}-{request.Currency}";

            if (request.ReturnDate.HasValue)
            {
                key += $"-{request.ReturnDate:yyyyMMdd}";
            }

            return key;
        }
    }
}

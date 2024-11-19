using FlightFinderApi.Models;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlightFinderApi.Services
{
    public class IataService : IIataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public IataService(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<IataResponse> GetIataCodesAsync()
        {

            var cacheKey = "IataCodes";

            if (_cache.TryGetValue(cacheKey, out IataResponse cachedData))
            {
                return cachedData;
            }

            var clientSecret = _configuration["IATASettings:ClientSecret"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", clientSecret);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var IataEndpointUrl = "https://api.liteapi.travel/v3.0/data/iataCodes?timeout=4"; //TODO: Move to configuration

            var response = await _httpClient.GetAsync(IataEndpointUrl);

            _httpClient.DefaultRequestHeaders.Clear();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"IATA search failed with status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var flightData = JsonSerializer.Deserialize<IataResponse>(responseContent);

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            _cache.Set(cacheKey, flightData, cacheOptions);

            return flightData ?? new IataResponse();
        }

    }
}

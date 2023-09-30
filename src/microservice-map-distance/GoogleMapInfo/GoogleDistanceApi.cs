using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace GoogleMapInfo
{
    public sealed class GoogleDistanceApi
    {
        private readonly IConfiguration _configuration;

        public GoogleDistanceApi(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleDistanceData> GetMapDistance(
            string originCity,
            string destinationCity)
        {
            var apiKey = _configuration["googledistanceapikey"];
            var googleDistanceUrl = _configuration["googleDistanceApi:apiUrl"];
            googleDistanceUrl += $"origins={originCity}&destinations={destinationCity}&key={apiKey}";

            using HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Get, new Uri(googleDistanceUrl));

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            await using var data = await response.Content.ReadAsStreamAsync();
            var distanceInfo = await JsonSerializer.DeserializeAsync<GoogleDistanceData>(data);

            return distanceInfo;
        }
    }
}

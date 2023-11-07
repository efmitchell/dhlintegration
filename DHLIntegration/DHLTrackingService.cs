using DHLIntegration.Exceptions;
using DHLIntegration.Models;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace DHLIntegration;

public class DHLTrackingService : IDHLService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy _retryPolicy;

    public DHLTrackingService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _retryPolicy = Policy
            .Handle<RateLimitExceededException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            );
    }

    public async Task<TrackingEvent> GetLastTrackingEventAsync(string trackingNumber, string apiKey)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            var response = await _httpClient.GetAsync($"https://api.dhl.com/track/shipments?trackingNumber={trackingNumber}");

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitExceededException("Rate limit exceeded. Retry after some time.");
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var trackingInfo = JsonConvert.DeserializeObject<TrackingInformation>(content);
            return trackingInfo.ShipmentTrackingDetails[0].TrackingEvents[^1];
        });
    }

    public async Task<List<ServicePoint>> GetServicePointLocationsAsync(string countryCode, string city, int? radius, string apiKey)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            var response = await _httpClient.GetAsync($"https://api.dhl.com/servicepoints/locator?countryCode={countryCode}&city={city}&radius={radius}");

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitExceededException("Rate limit exceeded. Retry after some time.");
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var servicePoints = JsonConvert.DeserializeObject<ServicePointInformation>(content);
            
            // Perhaps consider pagination when returning list.
            return servicePoints.ServicePoints;
        });
    }
}
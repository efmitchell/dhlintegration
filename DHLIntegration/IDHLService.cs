using DHLIntegration.Models;

namespace DHLIntegration;

public interface IDHLService
{
    Task<TrackingEvent> GetLastTrackingEventAsync(string trackingNumber, string apiKey);
    Task<List<ServicePoint>> GetServicePointLocationsAsync(string countryCode, string city, int? radius, string apiKey);
}
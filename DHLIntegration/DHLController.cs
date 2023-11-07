using DHLIntegration.Models;

namespace DHLIntegration;

public class DHLController
{
    private readonly IDHLService _dhlService;

    public DHLController(IDHLService dhlService)
    {
        _dhlService = dhlService;
    }

    // Function to get tracking information for a package
    public async Task<TrackingEvent> GetTrackingInformation(string trackingNumber)
    {
        string apiKey = "your_api_key"; // Replace with actual API key
        return await _dhlService.GetLastTrackingEventAsync(trackingNumber, apiKey);
    }

    // Function to get service points near a location
    public async Task<List<ServicePoint>> GetServicePointsNearLocation(string countryCode, string city, int radius)
    {
        string apiKey = "your_api_key"; // Replace with actual API key
        return await _dhlService.GetServicePointLocationsAsync(countryCode, city, radius, apiKey);
    }
}
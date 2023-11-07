namespace DHLIntegration.Models;

public class TrackingInformation
{
    public List<ShipmentTrackingDetails> ShipmentTrackingDetails { get; set; }
}

public class ShipmentTrackingDetails
{
    public List<TrackingEvent> TrackingEvents { get; set; }
}

public class TrackingEvent
{
    public string Status { get; set; }
    public DateTime Timestamp { get; set; }
}
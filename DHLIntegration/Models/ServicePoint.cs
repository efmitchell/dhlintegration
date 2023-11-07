namespace DHLIntegration.Models;

public class ServicePointInformation
{
    public List<ServicePoint> ServicePoints { get; set; }
}

public class ServicePoint
{
    public string Address { get; set; }
    public string OpeningHours { get; set; }
}
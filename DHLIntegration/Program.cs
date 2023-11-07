using DHLIntegration;
using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddSingleton<IDHLService, DHLTrackingService>()
            .BuildServiceProvider();

        var dhlService = serviceProvider.GetService<IDHLService>();
        
        var dhlController = new DHLController(dhlService);

        // Example usage of the controller's methods
        // You need to replace "your_tracking_number", "your_country_code", "your_city" and "radius" with actual values
        var trackingInformation = await dhlController.GetTrackingInformation("your_tracking_number"); // 7777777770 or 8264715546
        var servicePoints = await dhlController.GetServicePointsNearLocation("your_country_code", "your_city", 10); 

        // Prints it to the console
        Console.WriteLine("Tracking Information:");
        Console.WriteLine(trackingInformation);
        Console.WriteLine("Service Points:");
        Console.WriteLine(servicePoints);
    }
}
# DHL API Integration

This project provides a C# implementation for integrating with DHL's tracking and service point locator APIs, with a focus on SOLID principles and good practices.

## Features

- Retrieve the latest tracking event for a shipment.
- Get service point locations within a specified radius.

## Getting Started

1. Clone the repository.
2. Open the solution in your preferred IDE (e.g., Visual Studio).
3. Restore NuGet packages.
4. Add your DHL API key to the configuration, and replace `"your_api_key"` in `DHLController.cs` with your actual API key.
5. Add the parameters you wish to test in the Program.cs.
6. Rebuild the project.
7. Run the application from the executable in the bin file.

## Usage

Create an instance of `DHLController` and use its methods to interact with the DHL API.

## Error Handling

- Handles rate limits with retry policy using exponential backoff.
- Custom exceptions for clearer error messages.
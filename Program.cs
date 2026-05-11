using Projekt2WeatherAggregator.Application.Interfaces;
using Projekt2WeatherAggregator.Application.Services;
using Projekt2WeatherAggregator.Domain;
using Projekt2WeatherAggregator.Infrastructure.Http;
using Projekt2WeatherAggregator.Infrastructure.Providers;
using Projekt2WeatherAggregator.Presentation;

ConsoleRenderer.PrintHeader();

Console.Write("Cities: ");
string? input = Console.ReadLine();

if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("You must enter at least one city.");
    return;
}

string[] cityNames = input
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

using HttpClient geocodingHttpClient = HttpClientFactory.CreateOpenMeteoGeocodingClient();
using HttpClient weatherHttpClient = HttpClientFactory.CreateOpenMeteoClient();

IGeocodingProvider geocodingProvider = new OpenMeteoGeocodingProvider(geocodingHttpClient);
IWeatherProvider weatherProvider = new OpenMeteoWeatherProvider(weatherHttpClient);
IWeatherAggregationService aggregationService = new WeatherAggregationService(geocodingProvider, weatherProvider);

using CancellationTokenSource timeoutSource = new(TimeSpan.FromSeconds(15));

try
{
    IReadOnlyList<WeatherReport> reports = await aggregationService.GetReportsAsync(cityNames, timeoutSource.Token);
    WeatherSummary summary = aggregationService.BuildSummary(reports);

    Console.WriteLine();
    ConsoleRenderer.PrintReports(reports);
    ConsoleRenderer.PrintSummary(summary);
    ConsoleRenderer.PrintWarmCities(reports, threshold: 20.0);
}
catch (OperationCanceledException)
{
    Console.WriteLine("The request timed out or was cancelled.");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP communication error: {ex.Message}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Data processing error: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Input validation error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}

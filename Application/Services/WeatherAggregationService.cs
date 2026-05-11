using Projekt2WeatherAggregator.Application.Interfaces;
using Projekt2WeatherAggregator.Domain;

namespace Projekt2WeatherAggregator.Application.Services;

public sealed class WeatherAggregationService : IWeatherAggregationService
{
    private readonly IGeocodingProvider _geocodingProvider;
    private readonly IWeatherProvider _weatherProvider;

    public WeatherAggregationService(IGeocodingProvider geocodingProvider, IWeatherProvider weatherProvider)
    {
        _geocodingProvider = geocodingProvider;
        _weatherProvider = weatherProvider;
    }

    public async Task<IReadOnlyList<WeatherReport>> GetReportsAsync(IEnumerable<string> cityNames, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(cityNames);

        List<string> normalizedCityNames = cityNames
            .Select(name => name?.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Cast<string>()
            .ToList();

        if (normalizedCityNames.Count == 0)
        {
            throw new ArgumentException("At least one city must be provided.", nameof(cityNames));
        }

        List<Task<WeatherReport>> tasks = normalizedCityNames
            .Select(city => GetWeatherForCityAsync(city, cancellationToken))
            .ToList();

        WeatherReport[] reports = await Task.WhenAll(tasks);

        return reports
            .OrderByDescending(report => report.TemperatureCelsius)
            .ThenBy(report => report.City)
            .ToList();
    }

    public WeatherSummary BuildSummary(IEnumerable<WeatherReport> reports)
    {
        ArgumentNullException.ThrowIfNull(reports);

        List<WeatherReport> reportList = reports.ToList();
        if (reportList.Count == 0)
        {
            throw new ArgumentException("Cannot build a summary from an empty collection.", nameof(reports));
        }

        WeatherReport warmest = reportList.MaxBy(report => report.TemperatureCelsius)!;
        WeatherReport coldest = reportList.MinBy(report => report.TemperatureCelsius)!;

        return new WeatherSummary(
            CityCount: reportList.Count,
            AverageTemperature: reportList.Average(report => report.TemperatureCelsius),
            MinimumTemperature: coldest.TemperatureCelsius,
            MaximumTemperature: warmest.TemperatureCelsius,
            WarmestCity: warmest.City,
            ColdestCity: coldest.City);
    }

    private async Task<WeatherReport> GetWeatherForCityAsync(string cityName, CancellationToken cancellationToken)
    {
        CityCoordinates coordinates = await _geocodingProvider.GetCoordinatesAsync(cityName, cancellationToken);
        return await _weatherProvider.GetCurrentWeatherAsync(coordinates, cancellationToken);
    }
}

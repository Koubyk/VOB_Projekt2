using Projekt2WeatherAggregator.Domain;

namespace Projekt2WeatherAggregator.Application.Interfaces;

public interface IWeatherAggregationService
{
    Task<IReadOnlyList<WeatherReport>> GetReportsAsync(IEnumerable<string> cityNames, CancellationToken cancellationToken);
    WeatherSummary BuildSummary(IEnumerable<WeatherReport> reports);
}

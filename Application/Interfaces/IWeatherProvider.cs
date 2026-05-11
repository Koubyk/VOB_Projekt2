using Projekt2WeatherAggregator.Domain;

namespace Projekt2WeatherAggregator.Application.Interfaces;

public interface IWeatherProvider
{
    Task<WeatherReport> GetCurrentWeatherAsync(CityCoordinates coordinates, CancellationToken cancellationToken);
}

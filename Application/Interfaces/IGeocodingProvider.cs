using Projekt2WeatherAggregator.Domain;

namespace Projekt2WeatherAggregator.Application.Interfaces;

public interface IGeocodingProvider
{
    Task<CityCoordinates> GetCoordinatesAsync(string cityName, CancellationToken cancellationToken);
}

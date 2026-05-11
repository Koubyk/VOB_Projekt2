namespace Projekt2WeatherAggregator.Domain;

public sealed record CityCoordinates(
    string Name,
    string Country,
    double Latitude,
    double Longitude);

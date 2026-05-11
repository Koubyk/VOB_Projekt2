namespace Projekt2WeatherAggregator.Domain;

public sealed record WeatherSummary(
    int CityCount,
    double AverageTemperature,
    double MinimumTemperature,
    double MaximumTemperature,
    string WarmestCity,
    string ColdestCity);

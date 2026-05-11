namespace Projekt2WeatherAggregator.Domain;

public sealed record WeatherReport(
    string City,
    string Country,
    DateTime Timestamp,
    double TemperatureCelsius,
    double WindSpeedKmH,
    int WeatherCode)
{
    public bool IsWarm(double threshold) => TemperatureCelsius >= threshold;
}

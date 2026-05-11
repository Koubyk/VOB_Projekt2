using System.Text.Json.Serialization;

namespace Projekt2WeatherAggregator.Infrastructure.Dto;

public sealed class WeatherResponseDto
{
    [JsonPropertyName("current")]
    public CurrentWeatherDto? Current { get; set; }
}

public sealed class CurrentWeatherDto
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double Temperature2m { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed10m { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }
}

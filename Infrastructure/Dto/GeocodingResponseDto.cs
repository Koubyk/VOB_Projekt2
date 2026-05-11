using System.Text.Json.Serialization;

namespace Projekt2WeatherAggregator.Infrastructure.Dto;

public sealed class GeocodingResponseDto
{
    [JsonPropertyName("results")]
    public List<GeocodingResultDto>? Results { get; set; }
}

public sealed class GeocodingResultDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

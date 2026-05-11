using System.Text.Json;
using Projekt2WeatherAggregator.Application.Interfaces;
using Projekt2WeatherAggregator.Domain;
using Projekt2WeatherAggregator.Infrastructure.Dto;

namespace Projekt2WeatherAggregator.Infrastructure.Providers;

public sealed class OpenMeteoWeatherProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public OpenMeteoWeatherProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherReport> GetCurrentWeatherAsync(CityCoordinates coordinates, CancellationToken cancellationToken)
    {
        string requestUri = string.Join('&',
            "v1/forecast?current=temperature_2m,wind_speed_10m,weather_code",
            $"latitude={coordinates.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}",
            $"longitude={coordinates.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        WeatherResponseDto? dto = await JsonSerializer.DeserializeAsync<WeatherResponseDto>(stream, _jsonOptions, cancellationToken);

        if (dto?.Current is null)
        {
            throw new InvalidOperationException($"Weather data were not available for city '{coordinates.Name}'.");
        }

        return new WeatherReport(
            coordinates.Name,
            coordinates.Country,
            dto.Current.Time,
            dto.Current.Temperature2m,
            dto.Current.WindSpeed10m,
            dto.Current.WeatherCode);
    }
}

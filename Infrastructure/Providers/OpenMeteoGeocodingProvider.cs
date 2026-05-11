using System.Net;
using System.Text.Json;
using Projekt2WeatherAggregator.Application.Interfaces;
using Projekt2WeatherAggregator.Domain;
using Projekt2WeatherAggregator.Infrastructure.Dto;

namespace Projekt2WeatherAggregator.Infrastructure.Providers;

public sealed class OpenMeteoGeocodingProvider : IGeocodingProvider
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public OpenMeteoGeocodingProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CityCoordinates> GetCoordinatesAsync(string cityName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        {
            throw new ArgumentException("City name cannot be empty.", nameof(cityName));
        }

        string requestUri = $"v1/search?name={WebUtility.UrlEncode(cityName)}&count=1&language=en&format=json";

        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        GeocodingResponseDto? dto = await JsonSerializer.DeserializeAsync<GeocodingResponseDto>(stream, _jsonOptions, cancellationToken);

        GeocodingResultDto? result = dto?.Results?.FirstOrDefault();
        if (result is null || string.IsNullOrWhiteSpace(result.Name) || string.IsNullOrWhiteSpace(result.Country))
        {
            throw new InvalidOperationException($"No coordinates were found for city '{cityName}'.");
        }

        return new CityCoordinates(result.Name, result.Country, result.Latitude, result.Longitude);
    }
}

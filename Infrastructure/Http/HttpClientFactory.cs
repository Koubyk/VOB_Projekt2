namespace Projekt2WeatherAggregator.Infrastructure.Http;

public static class HttpClientFactory
{
    public static HttpClient CreateOpenMeteoClient()
    {
        return new HttpClient
        {
            BaseAddress = new Uri("https://api.open-meteo.com/"),
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public static HttpClient CreateOpenMeteoGeocodingClient()
    {
        return new HttpClient
        {
            BaseAddress = new Uri("https://geocoding-api.open-meteo.com/"),
            Timeout = TimeSpan.FromSeconds(10)
        };
    }
}

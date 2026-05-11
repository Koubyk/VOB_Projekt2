using Projekt2WeatherAggregator.Domain;

namespace Projekt2WeatherAggregator.Presentation;

public static class ConsoleRenderer
{
    public static void PrintHeader()
    {
        Console.WriteLine("=== Weather Aggregator ===");
        Console.WriteLine("Enter one or more city names separated by commas.");
        Console.WriteLine("Example: Prague, Brno, Bratislava, Vienna");
        Console.WriteLine();
    }

    public static void PrintReports(IEnumerable<WeatherReport> reports)
    {
        foreach (WeatherReport report in reports)
        {
            Console.WriteLine(
                $"{report.City}, {report.Country} | {report.Timestamp:yyyy-MM-dd HH:mm} | " +
                $"{report.TemperatureCelsius:F1} °C | Wind {report.WindSpeedKmH:F1} km/h | Code {report.WeatherCode}");
        }
    }

    public static void PrintSummary(WeatherSummary summary)
    {
        Console.WriteLine();
        Console.WriteLine("--- Summary ---");
        Console.WriteLine($"Cities processed: {summary.CityCount}");
        Console.WriteLine($"Average temperature: {summary.AverageTemperature:F1} °C");
        Console.WriteLine($"Minimum temperature: {summary.MinimumTemperature:F1} °C ({summary.ColdestCity})");
        Console.WriteLine($"Maximum temperature: {summary.MaximumTemperature:F1} °C ({summary.WarmestCity})");
    }

    public static void PrintWarmCities(IEnumerable<WeatherReport> reports, double threshold)
    {
        List<WeatherReport> warmCities = reports
            .Where(report => report.IsWarm(threshold))
            .ToList();

        Console.WriteLine();
        Console.WriteLine($"--- Cities with temperature >= {threshold:F1} °C ---");

        if (warmCities.Count == 0)
        {
            Console.WriteLine("No city matched the temperature filter.");
            return;
        }

        foreach (WeatherReport report in warmCities)
        {
            Console.WriteLine($"{report.City}: {report.TemperatureCelsius:F1} °C");
        }
    }
}

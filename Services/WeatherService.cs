using System.Text.Json;
using WeatherDashboard.Models;

namespace WeatherDashboard.Services;

public class WeatherService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IConfiguration _config;

    public WeatherService(IHttpClientFactory httpFactory, IConfiguration config)
    {
        _httpFactory = httpFactory;
        _config = config;
    }

    public async Task<CurrentWeather?> GetCurrentAsync(string city, CancellationToken cancellationToken = default)
    {
        var baseUrl = _config["OpenWeather:BaseUrl"] ?? "https://api.openweathermap.org/data/2.5/";
        var apiKey = _config["OpenWeather:ApiKey"];
        var units = _config["OpenWeather:Units"] ?? "metric";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("OpenWeather API key is not configured. Use user-secrets or environment variables.");
        }

        var url = $"{baseUrl}weather?q={Uri.EscapeDataString(city)}&appid={apiKey}&units={units}";
        var client = _httpFactory.CreateClient();
        using var resp = await client.GetAsync(url, cancellationToken);

        if (!resp.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await resp.Content.ReadAsStringAsync(cancellationToken);
        var dto = JsonSerializer.Deserialize<OpenWeatherResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (dto is null)
        {
            return null;
        }

        return new CurrentWeather
        {
            City = dto.Name ?? city,
            Temperature = dto.Main?.Temp,
            Description = dto.Weather?.FirstOrDefault()?.Description,
            Icon = dto.Weather?.FirstOrDefault()?.Icon,
            CountryCode = dto.Sys?.Country
        };
    }

    // minimal DTOs
    private sealed class OpenWeatherResponse
    {
        public string? Name { get; set; }
        public MainBlock? Main { get; set; }
        public List<WeatherBlock>? Weather { get; set; }
        public SysBlock? Sys { get; set; }
    }
    private sealed class MainBlock { public double? Temp { get; set; } }
    private sealed class WeatherBlock { public string? Description { get; set; } public string? Icon { get; set; } }
    private sealed class SysBlock { public string? Country { get; set; } }
}

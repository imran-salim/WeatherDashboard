using Newtonsoft.Json;
using System.Net;

public class WeatherService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public WeatherService(IConfiguration config, HttpClient httpclient)
    {
        _config = config;
        _httpClient = httpclient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        string? apiKey = _config["OpenWeatherMap:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new Exception("API key is missing from configuration.");
        }
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("City not found. Please check the spelling and try again.");
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("Invalid API key. Please contact support.");
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new Exception("Too many requests. Please try again later.");
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Invalid request. Please check your input.");
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(content);

            if (data == null)
            {
                throw new Exception("Failed to parse weather data from the response.");
            }

            return new WeatherData
            {
                City = city,
                Country = data.sys.country,
                Temperature = data.main.temp,
                Description = data.weather[0].description,
                Icon = data.weather[0].icon,
                Humidity = data.main.humidity,
                WindSpeed = data.wind.speed
            };
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Weather service error: {ex.Message}");
        }
    }
}
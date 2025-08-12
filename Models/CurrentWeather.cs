namespace WeatherDashboard.Models;

public sealed class CurrentWeather
{
    public string? City { get; set; }
    public double? Temperature { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? CountryCode { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.Services;

namespace WeatherDashboard.Controllers;

public class WeatherController : Controller
{
    private readonly WeatherService _service;
    private readonly IConfiguration _config;

    public WeatherController(WeatherService service, IConfiguration config)
    {
        _service = service;
        _config = config;
    }

    public async Task<IActionResult> Index(string? city, CancellationToken cancellationToken)
    {
        city ??= _config["OpenWeather:DefaultCity"] ?? "Auckland";
        var model = await _service.GetCurrentAsync(city, cancellationToken);
        return View(model);
    }

    [HttpGet]
    public Task<IActionResult> Search(string city, CancellationToken cancellationToken) => Index(city, cancellationToken);
}

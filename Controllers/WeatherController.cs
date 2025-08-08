using Microsoft.AspNetCore.Mvc;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(string city)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                ViewBag.Error = "Please enter a city name";
                return View("Index");
            }

            var weather = await _weatherService.GetWeatherAsync(city);
            return View("Index", weather);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Index");
        }
    }
}
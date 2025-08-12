# WeatherDashboard (simple ASP.NET Core MVC)

Simple weather lookup using OpenWeatherMap.

## Run locally

```bash
dotnet build

# set your API key (stored outside the repo)
dotnet user-secrets init
dotnet user-secrets set "OpenWeather:ApiKey" "<YOUR_OPENWEATHER_API_KEY>"

dotnet run
```

Open http://localhost:5027/Weather
Try: `?city=Christchurch`

## How it works (quick)

- `WeatherService` calls OpenWeather using `HttpClient` (via `IHttpClientFactory`).
- Config comes from `appsettings.json` + user-secrets for `OpenWeather:ApiKey`.
- MVC Controller renders a simple Razor view.

# WeatherDashboard

Welcome to WeatherDashboard! To use this application, you'll need an API key from a weather data provider.

## Getting an API Key

1. Visit [OpenWeatherMap](https://openweathermap.org/api).
2. Sign up for a free account.
3. After verifying your email, log in and navigate to the "API keys" section in your dashboard.
4. Copy your generated API key.

## Configuration

Add your API key to the application's configuration file `appsettings.json` as such:
```
  "OpenWeatherMap": {
    "ApiKey": "paste your API key here"
  },
```

## Starting the application

1. Enter the following command: 
    `dotnet run`
2. Navigate to localhost with the supplied port number in your web browser
    e.g. `http://localhost:5027`

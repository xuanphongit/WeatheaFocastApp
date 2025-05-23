using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;
using System.Net.Http.Json;

namespace WeatherForecast.Infrastructure.Services;

public class OpenWeatherMapService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly IMemoryCache _cache;
    private readonly ILogger<OpenWeatherMapService> _logger;
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public OpenWeatherMapService(
        HttpClient httpClient, 
        IConfiguration configuration,
        IMemoryCache cache,
        ILogger<OpenWeatherMapService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
        _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new ArgumentNullException("OpenWeatherMap:ApiKey is not configured");
    }

    public async Task<Weather> GetCurrentWeatherAsync(string city, string country)
    {
        try
        {
            var cacheKey = $"weather_{city}_{country}";
            
            if (_cache.TryGetValue(cacheKey, out Weather cachedWeather))
            {
                _logger.LogInformation("Retrieved weather data from cache for {City}, {Country}", city, country);
                return cachedWeather;
            }

            _logger.LogInformation("Fetching weather data for {City}, {Country}", city, country);
            var response = await _httpClient.GetFromJsonAsync<OpenWeatherMapResponse>(
                $"{BaseUrl}/weather?q={city},{country}&appid={_apiKey}&units=metric");

            if (response == null)
                throw new Exception("Failed to get weather data");

            var weather = new Weather
            {
                City = response.Name,
                Country = response.Sys.Country,
                Temperature = response.Main.Temp,
                FeelsLike = response.Main.FeelsLike,
                Humidity = response.Main.Humidity,
                WindSpeed = response.Wind.Speed,
                Description = response.Weather[0].Description,
                Icon = response.Weather[0].Icon,
                Timestamp = DateTime.UtcNow,
                Latitude = response.Coord.Lat,
                Longitude = response.Coord.Lon
            };

            _cache.Set(cacheKey, weather, CacheDuration);
            _logger.LogInformation("Cached weather data for {City}, {Country}", city, country);

            return weather;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather data for {City}, {Country}", city, country);
            throw;
        }
    }

    public async Task<Forecast> GetForecastAsync(string city, string country)
    {
        try
        {
            var cacheKey = $"forecast_{city}_{country}";
            
            if (_cache.TryGetValue(cacheKey, out Forecast cachedForecast))
            {
                _logger.LogInformation("Retrieved forecast data from cache for {City}, {Country}", city, country);
                return cachedForecast;
            }

            _logger.LogInformation("Fetching forecast data for {City}, {Country}", city, country);
            var response = await _httpClient.GetFromJsonAsync<OpenWeatherMapForecastResponse>(
                $"{BaseUrl}/forecast?q={city},{country}&appid={_apiKey}&units=metric");

            if (response == null)
                throw new Exception("Failed to get forecast data");

            var forecast = new Forecast
            {
                City = response.City.Name,
                Country = response.City.Country,
                Date = DateTime.UtcNow,
                List = response.List.Select(item => new ForecastItem
                {
                    DateTime = DateTimeOffset.FromUnixTimeSeconds(item.Dt).DateTime,
                    Temperature = item.Main.Temp,
                    Description = item.Weather[0].Description,
                    Icon = item.Weather[0].Icon
                }).ToList()
            };

            _cache.Set(cacheKey, forecast, CacheDuration);
            _logger.LogInformation("Cached forecast data for {City}, {Country}", city, country);

            return forecast;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting forecast data for {City}, {Country}", city, country);
            throw;
        }
    }
}

public class OpenWeatherMapResponse
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public WindData Wind { get; set; }
    public List<WeatherData> Weather { get; set; }
    public SysData Sys { get; set; }
    public CoordData Coord { get; set; }
}

public class OpenWeatherMapForecastResponse
{
    public CityData City { get; set; }
    public List<ForecastItemData> List { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double Humidity { get; set; }
}

public class WindData
{
    public double Speed { get; set; }
}

public class WeatherData
{
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class SysData
{
    public string Country { get; set; }
}

public class CoordData
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class CityData
{
    public string Name { get; set; }
    public string Country { get; set; }
}

public class ForecastItemData
{
    public long Dt { get; set; }
    public MainData Main { get; set; }
    public WindData Wind { get; set; }
    public List<WeatherData> Weather { get; set; }
    public RainData Rain { get; set; }
    public CloudsData Clouds { get; set; }
}

public class RainData
{
    public double? ThreeHour { get; set; }
}

public class CloudsData
{
    public int All { get; set; }
} 
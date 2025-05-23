using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.Interfaces;

public interface IWeatherService
{
    Task<Weather> GetCurrentWeatherAsync(string city, string country);
    Task<Forecast> GetForecastAsync(string city, string country);
} 
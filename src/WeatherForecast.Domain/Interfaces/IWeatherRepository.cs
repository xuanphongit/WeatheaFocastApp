using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Weather> GetCurrentWeatherAsync(string city);
        Task<Weather> GetWeatherByCoordinatesAsync(double latitude, double longitude);
        Task<IEnumerable<Forecast>> GetForecastAsync(string city, int days);
        Task SaveWeatherAsync(Weather weather);
        Task SaveForecastAsync(IEnumerable<Forecast> forecasts);
    }
} 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;
// using WeatherForecast.Infrastructure.Data;
using WeatherForecast.Infrastructure.Services;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly OpenWeatherMapService _weatherService;

        public WeatherRepository(OpenWeatherMapService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<Weather> GetCurrentWeatherAsync(string city, string country)
            => await _weatherService.GetCurrentWeatherAsync(city, country);

        public async Task<Forecast> GetForecastAsync(string city, string country)
            => await _weatherService.GetForecastAsync(city, country);

        public Task<Weather> GetWeatherByCoordinatesAsync(double latitude, double longitude)
            => throw new NotImplementedException();

        public Task<IEnumerable<Forecast>> GetForecastAsync(string city, int days)
            => throw new NotImplementedException();

        public Task SaveWeatherAsync(Weather weather)
            => Task.CompletedTask;

        public Task SaveForecastAsync(IEnumerable<Forecast> forecasts)
            => Task.CompletedTask;
    }
} 
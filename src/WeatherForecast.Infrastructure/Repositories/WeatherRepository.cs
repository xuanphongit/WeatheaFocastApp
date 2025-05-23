using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        public async Task<Weather> GetCurrentWeatherAsync(string city)
        {
            // TODO: Implement OpenWeatherMap API integration
            throw new NotImplementedException();
        }

        public async Task<Weather> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            // TODO: Implement OpenWeatherMap API integration
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Forecast>> GetForecastAsync(string city, int days)
        {
            // TODO: Implement OpenWeatherMap API integration
            throw new NotImplementedException();
        }

        public async Task SaveWeatherAsync(Weather weather)
        {
            // TODO: Implement database storage
            throw new NotImplementedException();
        }

        public async Task SaveForecastAsync(IEnumerable<Forecast> forecasts)
        {
            // TODO: Implement database storage
            throw new NotImplementedException();
        }
    }
} 
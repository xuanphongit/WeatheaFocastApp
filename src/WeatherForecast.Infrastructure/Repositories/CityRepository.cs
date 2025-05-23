using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        public async Task<City> GetByIdAsync(Guid id)
        {
            // TODO: Implement database query
            throw new NotImplementedException();
        }

        public async Task<City> GetByNameAsync(string name)
        {
            // TODO: Implement database query
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetFavoriteCitiesAsync()
        {
            // TODO: Implement database query
            throw new NotImplementedException();
        }

        public async Task<City> AddAsync(City city)
        {
            // TODO: Implement database insert
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(City city)
        {
            // TODO: Implement database update
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            // TODO: Implement database delete
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // TODO: Implement database check
            throw new NotImplementedException();
        }
    }
} 
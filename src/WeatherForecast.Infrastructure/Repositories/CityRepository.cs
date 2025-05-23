using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        public Task<City> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<City> GetByNameAsync(string name) => throw new NotImplementedException();
        public Task<IEnumerable<City>> GetFavoriteCitiesAsync() => throw new NotImplementedException();
        public Task<City> AddAsync(City city) => throw new NotImplementedException();
        public Task UpdateAsync(City city) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(string name) => throw new NotImplementedException();
        public Task<IEnumerable<City>> GetAllAsync() => Task.FromResult<IEnumerable<City>>(new List<City>());
    }
} 
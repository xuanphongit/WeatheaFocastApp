using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<City> GetByIdAsync(Guid id);
        Task<City> GetByNameAsync(string name);
        Task<IEnumerable<City>> GetFavoriteCitiesAsync();
        Task<City> AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<City>> GetAllAsync();
    }
} 
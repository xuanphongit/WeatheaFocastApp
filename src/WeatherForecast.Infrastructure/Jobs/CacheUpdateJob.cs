using Hangfire;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Infrastructure.Jobs;

public class CacheUpdateJob
{
    private readonly IWeatherService _weatherService;
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<CacheUpdateJob> _logger;

    public CacheUpdateJob(
        IWeatherService weatherService,
        ICityRepository cityRepository,
        ILogger<CacheUpdateJob> logger)
    {
        _weatherService = weatherService;
        _cityRepository = cityRepository;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task UpdateWeatherCache()
    {
        try
        {
            _logger.LogInformation("Starting weather cache update job");
            var cities = await _cityRepository.GetAllAsync();

            foreach (var city in cities)
            {
                try
                {
                    await _weatherService.GetCurrentWeatherAsync(city.Name, city.Country);
                    await _weatherService.GetForecastAsync(city.Name, city.Country);
                    _logger.LogInformation("Updated cache for {City}, {Country}", city.Name, city.Country);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update cache for {City}, {Country}", city.Name, city.Country);
                }
            }

            _logger.LogInformation("Completed weather cache update job");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weather cache update job failed");
            throw;
        }
    }
} 
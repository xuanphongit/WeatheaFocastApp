using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherForecast.Application.Features.Weather.Queries;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(
        IWeatherRepository weatherRepository,
        ICityRepository cityRepository,
        ILogger<WeatherController> logger)
    {
        _weatherRepository = weatherRepository;
        _cityRepository = cityRepository;
        _logger = logger;
    }

    [HttpGet("current/{city}/{country}")]
    public async Task<IActionResult> GetCurrentWeather(string city, string country)
    {
        try
        {
            _logger.LogInformation("Getting current weather for {City}, {Country}", city, country);
            var weather = await _weatherRepository.GetCurrentWeatherAsync(city, country);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current weather for {City}, {Country}", city, country);
            return StatusCode(500, new { error = "Failed to get weather data" });
        }
    }

    [HttpGet("forecast/{city}/{country}")]
    public async Task<IActionResult> GetForecast(string city, string country)
    {
        try
        {
            _logger.LogInformation("Getting forecast for {City}, {Country}", city, country);
            var forecast = await _weatherRepository.GetForecastAsync(city, country);
            return Ok(forecast);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting forecast for {City}, {Country}", city, country);
            return StatusCode(500, new { error = "Failed to get forecast data" });
        }
    }

    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        try
        {
            _logger.LogInformation("Getting list of cities");
            var cities = await _cityRepository.GetAllAsync();
            return Ok(cities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting list of cities");
            return StatusCode(500, new { error = "Failed to get cities" });
        }
    }
} 
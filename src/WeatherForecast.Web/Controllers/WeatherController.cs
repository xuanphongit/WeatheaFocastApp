using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherForecast.Application.Features.Weather.Queries;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Web.Controllers;

/// <summary>
/// Controller for weather-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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

    /// <summary>
    /// Gets the current weather for a specific city and country
    /// </summary>
    /// <param name="city">The name of the city</param>
    /// <param name="country">The country code (e.g., US, UK)</param>
    /// <returns>The current weather information</returns>
    /// <response code="200">Returns the weather information</response>
    /// <response code="500">If there was an internal error</response>
    [HttpGet("current/{city}/{country}")]
    [ProducesResponseType(typeof(Weather), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Gets the weather forecast for a specific city and country
    /// </summary>
    /// <param name="city">The name of the city</param>
    /// <param name="country">The country code (e.g., US, UK)</param>
    /// <returns>The weather forecast information</returns>
    /// <response code="200">Returns the forecast information</response>
    /// <response code="500">If there was an internal error</response>
    [HttpGet("forecast/{city}/{country}")]
    [ProducesResponseType(typeof(Forecast), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Gets a list of all available cities
    /// </summary>
    /// <returns>A list of cities</returns>
    /// <response code="200">Returns the list of cities</response>
    /// <response code="500">If there was an internal error</response>
    [HttpGet("cities")]
    [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Infrastructure.Services;
using Xunit;

namespace WeatherForecast.Tests.Services;

public class OpenWeatherMapServiceTests
{
    private readonly Mock<HttpClient> _httpClientMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly Mock<ILogger<OpenWeatherMapService>> _loggerMock;
    private readonly OpenWeatherMapService _service;
    private const string ApiKey = "test-api-key";

    public OpenWeatherMapServiceTests()
    {
        _httpClientMock = new Mock<HttpClient>();
        _configurationMock = new Mock<IConfiguration>();
        _cacheMock = new Mock<IMemoryCache>();
        _loggerMock = new Mock<ILogger<OpenWeatherMapService>>();

        _configurationMock.Setup(x => x["OpenWeatherMap:ApiKey"]).Returns(ApiKey);

        _service = new OpenWeatherMapService(
            _httpClientMock.Object,
            _configurationMock.Object,
            _cacheMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_WhenCacheHit_ReturnsCachedWeather()
    {
        // Arrange
        var city = "London";
        var country = "UK";
        var expectedWeather = new Weather
        {
            City = city,
            Country = country,
            Temperature = 20,
            Description = "Sunny"
        };

        object cachedWeather = expectedWeather;
        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedWeather))
            .Returns(true);

        // Act
        var result = await _service.GetCurrentWeatherAsync(city, country);

        // Assert
        Assert.Equal(expectedWeather.City, result.City);
        Assert.Equal(expectedWeather.Country, result.Country);
        Assert.Equal(expectedWeather.Temperature, result.Temperature);
        Assert.Equal(expectedWeather.Description, result.Description);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_WhenApiCallFails_ThrowsException()
    {
        // Arrange
        var city = "London";
        var country = "UK";

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
            .Returns(false);

        _httpClientMock.Setup(x => x.GetFromJsonAsync<OpenWeatherMapResponse>(
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException());

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => 
            _service.GetCurrentWeatherAsync(city, country));
    }

    [Fact]
    public async Task GetForecastAsync_WhenCacheHit_ReturnsCachedForecast()
    {
        // Arrange
        var city = "London";
        var country = "UK";
        var expectedForecast = new Forecast
        {
            City = city,
            Country = country,
            Date = DateTime.UtcNow,
            List = new List<ForecastItem>()
        };

        object cachedForecast = expectedForecast;
        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedForecast))
            .Returns(true);

        // Act
        var result = await _service.GetForecastAsync(city, country);

        // Assert
        Assert.Equal(expectedForecast.City, result.City);
        Assert.Equal(expectedForecast.Country, result.Country);
        Assert.Equal(expectedForecast.Date.Date, result.Date.Date);
        Assert.Equal(expectedForecast.List.Count, result.List.Count);
    }

    [Fact]
    public async Task GetForecastAsync_WhenApiCallFails_ThrowsException()
    {
        // Arrange
        var city = "London";
        var country = "UK";

        _cacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
            .Returns(false);

        _httpClientMock.Setup(x => x.GetFromJsonAsync<OpenWeatherMapForecastResponse>(
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException());

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => 
            _service.GetForecastAsync(city, country));
    }
} 
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text.Json;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Infrastructure.Services;
using Xunit;
using Microsoft.Extensions.Logging;

namespace WeatherForecast.Tests.Services;

public class OpenWeatherMapServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IDistributedCache> _cacheMock;
    private readonly Mock<ILogger<OpenWeatherMapService>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly OpenWeatherMapService _service;

    public OpenWeatherMapServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x["OpenWeatherMap:ApiKey"]).Returns("test_api_key");

        _cacheMock = new Mock<IDistributedCache>();
        _loggerMock = new Mock<ILogger<OpenWeatherMapService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.openweathermap.org/")
        };

        _service = new OpenWeatherMapService(
            _configurationMock.Object,
            httpClient,
            _cacheMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_WhenCacheHit_ReturnsCachedData()
    {
        // Arrange
        var city = new City { Name = "London", Country = "UK" };
        var expectedWeather = new WeatherData
        {
            Temperature = 20,
            Description = "Sunny",
            Humidity = 60,
            WindSpeed = 5
        };

        var cachedData = JsonSerializer.Serialize(expectedWeather);
        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(cachedData));

        // Act
        var result = await _service.GetCurrentWeatherAsync(city);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedWeather.Temperature, result.Temperature);
        Assert.Equal(expectedWeather.Description, result.Description);
        _cacheMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_WhenCacheMiss_CallsApiAndCachesResult()
    {
        // Arrange
        var city = new City { Name = "London", Country = "UK" };
        var apiResponse = new
        {
            main = new { temp = 293.15, humidity = 60 },
            weather = new[] { new { description = "Sunny" } },
            wind = new { speed = 5 }
        };

        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[])null);

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(apiResponse))
            });

        // Act
        var result = await _service.GetCurrentWeatherAsync(city);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(20, result.Temperature);
        Assert.Equal("Sunny", result.Description);
        _cacheMock.Verify(x => x.SetAsync(
            It.IsAny<string>(),
            It.IsAny<byte[]>(),
            It.IsAny<DistributedCacheEntryOptions>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact]
    public async Task GetForecastAsync_WhenCacheHit_ReturnsCachedData()
    {
        // Arrange
        var city = new City { Name = "London", Country = "UK" };
        var expectedForecast = new List<WeatherData>
        {
            new() { Temperature = 20, Description = "Sunny" },
            new() { Temperature = 18, Description = "Cloudy" }
        };

        var cachedData = JsonSerializer.Serialize(expectedForecast);
        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(cachedData));

        // Act
        var result = await _service.GetForecastAsync(city);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedForecast[0].Temperature, result.First().Temperature);
        _cacheMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetForecastAsync_WhenApiError_ThrowsException()
    {
        // Arrange
        var city = new City { Name = "London", Country = "UK" };

        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[])null);

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetForecastAsync(city));
    }

    [Fact]
    public async Task GetForecastAsync_ForDaNangTomorrow_ReturnsCorrectData()
    {
        // Arrange
        var city = new City { Name = "Da Nang", Country = "VN" };
        var tomorrow = DateTime.UtcNow.AddDays(1).Date;
        
        var apiResponse = new
        {
            list = new[]
            {
                new
                {
                    dt = ((DateTimeOffset)tomorrow).ToUnixTimeSeconds(),
                    main = new { temp = 303.15, humidity = 75 }, // 30°C
                    weather = new[] { new { description = "Partly Cloudy" } },
                    wind = new { speed = 3.5 },
                    dt_txt = tomorrow.ToString("yyyy-MM-dd HH:mm:ss")
                }
            }
        };

        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[])null);

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.RequestUri.ToString().Contains("Da Nang") &&
                    req.RequestUri.ToString().Contains("VN")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(apiResponse))
            });

        // Act
        var result = await _service.GetForecastAsync(city);

        // Assert
        Assert.NotNull(result);
        var tomorrowForecast = result.FirstOrDefault(f => f.Timestamp.Date == tomorrow);
        Assert.NotNull(tomorrowForecast);
        Assert.Equal(30, tomorrowForecast.Temperature); // 30°C
        Assert.Equal("Partly Cloudy", tomorrowForecast.Description);
        Assert.Equal(75, tomorrowForecast.Humidity);
        Assert.Equal(3.5, tomorrowForecast.WindSpeed);

        // Verify API call
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => 
                req.RequestUri.ToString().Contains("Da Nang") &&
                req.RequestUri.ToString().Contains("VN")),
            ItExpr.IsAny<CancellationToken>()
        );

        // Verify caching
        _cacheMock.Verify(x => x.SetAsync(
            It.IsAny<string>(),
            It.IsAny<byte[]>(),
            It.IsAny<DistributedCacheEntryOptions>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact]
    public async Task GetForecastAsync_ForDaNangTomorrow_WhenCacheHit_ReturnsCachedData()
    {
        // Arrange
        var city = new City { Name = "Da Nang", Country = "VN" };
        var tomorrow = DateTime.UtcNow.AddDays(1).Date;
        
        var cachedForecast = new List<WeatherData>
        {
            new()
            {
                Temperature = 30,
                Description = "Partly Cloudy",
                Humidity = 75,
                WindSpeed = 3.5,
                Timestamp = tomorrow
            }
        };

        var cachedData = JsonSerializer.Serialize(cachedForecast);
        _cacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(cachedData));

        // Act
        var result = await _service.GetForecastAsync(city);

        // Assert
        Assert.NotNull(result);
        var tomorrowForecast = result.FirstOrDefault(f => f.Timestamp.Date == tomorrow);
        Assert.NotNull(tomorrowForecast);
        Assert.Equal(30, tomorrowForecast.Temperature);
        Assert.Equal("Partly Cloudy", tomorrowForecast.Description);
        Assert.Equal(75, tomorrowForecast.Humidity);
        Assert.Equal(3.5, tomorrowForecast.WindSpeed);

        // Verify cache was used
        _cacheMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        
        // Verify API was not called
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Never(),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }
} 
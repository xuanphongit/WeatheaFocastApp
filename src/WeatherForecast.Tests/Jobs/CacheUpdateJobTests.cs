using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Infrastructure.Jobs;
using WeatherForecast.Infrastructure.Repositories;
using WeatherForecast.Infrastructure.Services;
using WeatherForecast.Domain.Interfaces;
using Xunit;

namespace WeatherForecast.Tests.Jobs;

public class CacheUpdateJobTests
{
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<ILogger<CacheUpdateJob>> _loggerMock;
    private readonly CacheUpdateJob _job;

    public CacheUpdateJobTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _loggerMock = new Mock<ILogger<CacheUpdateJob>>();

        _job = new CacheUpdateJob(
            _weatherServiceMock.Object,
            _cityRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task UpdateWeatherCache_WhenCitiesExist_UpdatesAllCities()
    {
        // Arrange
        var cities = new List<City>
        {
            new() { Name = "London", Country = "UK" },
            new() { Name = "Paris", Country = "France" }
        };

        _cityRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(cities);

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Weather());
        _weatherServiceMock.Setup(x => x.GetForecastAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Forecast());

        // Act
        await _job.UpdateWeatherCache();

        // Assert
        _weatherServiceMock.Verify(x => x.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        _weatherServiceMock.Verify(x => x.GetForecastAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Updated cache for")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ),
            Times.Exactly(2)
        );
    }

    [Fact]
    public async Task UpdateWeatherCache_WhenNoCities_LogsWarning()
    {
        // Arrange
        _cityRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<City>());

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Weather());
        _weatherServiceMock.Setup(x => x.GetForecastAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Forecast());

        // Act
        await _job.UpdateWeatherCache();

        // Assert
        _weatherServiceMock.Verify(x => x.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _weatherServiceMock.Verify(x => x.GetForecastAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No cities found")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateWeatherCache_WhenServiceFails_LogsError()
    {
        // Arrange
        var cities = new List<City>
        {
            new() { Name = "London", Country = "UK" }
        };

        _cityRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(cities);

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("API Error"));
        _weatherServiceMock.Setup(x => x.GetForecastAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Forecast());

        // Act
        await _job.UpdateWeatherCache();

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to update cache for")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ),
            Times.Once
        );
    }
} 
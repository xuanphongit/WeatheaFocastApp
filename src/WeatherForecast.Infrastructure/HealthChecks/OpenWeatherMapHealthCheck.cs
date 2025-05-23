using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherForecast.Infrastructure.HealthChecks;

public class OpenWeatherMapHealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherMapHealthCheck(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new ArgumentNullException("OpenWeatherMap:ApiKey is not configured");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q=London,UK&appid={_apiKey}",
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("OpenWeatherMap API is healthy");
            }

            return HealthCheckResult.Unhealthy($"OpenWeatherMap API returned status code {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("OpenWeatherMap API check failed", ex);
        }
    }
} 
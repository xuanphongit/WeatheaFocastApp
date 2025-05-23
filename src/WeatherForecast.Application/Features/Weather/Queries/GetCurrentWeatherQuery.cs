using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Application.Features.Weather.Queries;

public record GetCurrentWeatherQuery(string City, string Country) : IRequest<WeatherForecast.Domain.Entities.Weather>;

public class GetCurrentWeatherQueryHandler : IRequestHandler<GetCurrentWeatherQuery, WeatherForecast.Domain.Entities.Weather>
{
    private readonly IWeatherRepository _weatherRepository;

    public GetCurrentWeatherQueryHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<WeatherForecast.Domain.Entities.Weather> Handle(GetCurrentWeatherQuery request, CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetCurrentWeatherAsync(request.City, request.Country);
    }
} 
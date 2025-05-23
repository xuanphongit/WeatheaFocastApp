using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Application.Features.Weather.Queries
{
    public record GetForecastQuery(string City, string Country) : IRequest<WeatherForecast.Domain.Entities.Forecast>;

    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, WeatherForecast.Domain.Entities.Forecast>
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetForecastQueryHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherForecast.Domain.Entities.Forecast> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.GetForecastAsync(request.City, request.Country);
        }
    }
} 
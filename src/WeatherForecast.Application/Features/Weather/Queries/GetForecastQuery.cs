using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Application.Features.Weather.Queries
{
    public class GetForecastQuery : IRequest<IEnumerable<Forecast>>
    {
        public string City { get; set; }
        public int Days { get; set; } = 5;
    }

    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, IEnumerable<Forecast>>
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetForecastQueryHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<IEnumerable<Forecast>> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.GetForecastAsync(request.City, request.Days);
        }
    }
} 
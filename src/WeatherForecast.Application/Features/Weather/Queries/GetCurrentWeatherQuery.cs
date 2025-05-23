using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Interfaces;

namespace WeatherForecast.Application.Features.Weather.Queries
{
    public class GetCurrentWeatherQuery : IRequest<Weather>
    {
        public string City { get; set; }
    }

    public class GetCurrentWeatherQueryHandler : IRequestHandler<GetCurrentWeatherQuery, Weather>
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetCurrentWeatherQueryHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<Weather> Handle(GetCurrentWeatherQuery request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.GetCurrentWeatherAsync(request.City);
        }
    }
} 
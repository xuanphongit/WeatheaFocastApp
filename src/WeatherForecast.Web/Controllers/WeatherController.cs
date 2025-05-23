using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Features.Weather.Queries;

namespace WeatherForecast.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current/{city}")]
        public async Task<IActionResult> GetCurrentWeather(string city)
        {
            var query = new GetCurrentWeatherQuery { City = city };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("forecast/{city}")]
        public async Task<IActionResult> GetForecast(string city, [FromQuery] int days = 5)
        {
            var query = new GetForecastQuery { City = city, Days = days };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 
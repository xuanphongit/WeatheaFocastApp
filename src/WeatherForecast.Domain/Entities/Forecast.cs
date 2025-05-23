using System;
using System.Collections.Generic;

namespace WeatherForecast.Domain.Entities
{
    public class Forecast
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double Precipitation { get; set; }
        public double CloudCover { get; set; }
        public double UvIndex { get; set; }
        public List<ForecastItem> List { get; set; }
    }

    public class ForecastItem
    {
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
} 
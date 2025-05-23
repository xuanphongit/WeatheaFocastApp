using System;

namespace WeatherForecast.Domain.Entities
{
    public class Weather
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime Timestamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
} 
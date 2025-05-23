using System;
using System.Collections.Generic;

namespace WeatherForecast.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeZone { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime LastUpdated { get; set; }
    }
} 
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Weather>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.Temperature).HasPrecision(5, 2);
                entity.Property(e => e.FeelsLike).HasPrecision(5, 2);
                entity.Property(e => e.Humidity).HasPrecision(5, 2);
                entity.Property(e => e.WindSpeed).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Forecast>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.MinTemperature).HasPrecision(5, 2);
                entity.Property(e => e.MaxTemperature).HasPrecision(5, 2);
                entity.Property(e => e.Humidity).HasPrecision(5, 2);
                entity.Property(e => e.WindSpeed).HasPrecision(5, 2);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.Latitude).HasPrecision(9, 6);
                entity.Property(e => e.Longitude).HasPrecision(9, 6);
            });
        }
    }
} 
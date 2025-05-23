# Active Context - Weather Forecasting App

## Current Focus
Xây dựng ứng dụng dự báo thời tiết từ đầu với .NET 8 và Clean Architecture.

## Immediate Next Steps
1. **Tạo solution structure** với Clean Architecture layers
2. **Setup Domain layer** với core entities và value objects
3. **Implement Application layer** với CQRS patterns
4. **Configure Infrastructure** với EF Core và external APIs
5. **Build Web layer** với MVC controllers và views

## Key Decisions Made
- **Architecture**: Clean Architecture với DDD principles
- **Database**: SQL Server với Entity Framework Core
- **Caching**: Redis cho distributed caching
- **API Integration**: OpenWeatherMap cho weather data
- **Frontend**: ASP.NET Core MVC với Bootstrap 5
- **Testing**: xUnit với comprehensive test coverage

## Current Challenges
- Cần setup OpenWeatherMap API key
- Cần quyết định về authentication strategy (simple hoặc full Identity)
- Database choice: SQL Server vs PostgreSQL

## Dependencies to Address
1. OpenWeatherMap API account và key
2. Docker setup cho Redis
3. Database connection string
4. Logging configuration

## Feature Priority
1. **Phase 1**: Current weather display và basic city search
2. **Phase 2**: 5-day forecast và favorites
3. **Phase 3**: Charts và advanced features
4. **Phase 4**: Notifications và PWA capabilities

## Technical Notes
- Sử dụng .NET 8 minimal APIs cho high performance
- Implement proper error handling và resilience patterns
- Focus vào maintainable và testable code
- Progressive enhancement cho UI features 
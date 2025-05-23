# Weather Forecast Application

## Project Overview
A modern weather forecasting application that provides real-time weather data and forecasts using the OpenWeatherMap API. The application is built with .NET 8 and follows clean architecture principles.

## Core Features
1. Weather Data Retrieval
   - Current weather conditions
   - 5-day weather forecast
   - Support for multiple cities and countries
   - Detailed weather metrics (temperature, humidity, wind speed, etc.)

2. Performance & Reliability
   - Redis caching for improved performance
   - Automatic cache updates via background jobs
   - Rate limiting to prevent API abuse
   - Retry policies for API calls
   - Health monitoring and metrics

3. Security & Authentication
   - JWT-based authentication
   - Role-based authorization
   - Secure API endpoints
   - Protected Hangfire dashboard

4. Monitoring & Observability
   - Prometheus metrics
   - Health checks
   - Detailed logging
   - Error tracking
   - Performance monitoring

## Technical Stack
- .NET 8
- OpenWeatherMap API
- Redis for caching
- Hangfire for background jobs
- Prometheus for metrics
- JWT for authentication
- Serilog for logging

## Project Goals
1. Provide accurate and timely weather information
2. Ensure high performance and reliability
3. Maintain security and data protection
4. Enable easy monitoring and maintenance
5. Support scalability and future enhancements

## Mục tiêu dự án
Xây dựng ứng dụng dự báo thời tiết hiện đại sử dụng .NET 8 với kiến trúc sạch (Clean Architecture) và các pattern tốt nhất của enterprise development.

## Yêu cầu chức năng chính
1. **Hiển thị thời tiết hiện tại** theo vị trí địa lý
2. **Dự báo thời tiết** cho 5-7 ngày tới
3. **Tìm kiếm thành phố** và lưu danh sách yêu thích
4. **Thông báo cảnh báo** thời tiết xấu
5. **Biểu đồ và chart** hiển thị xu hướng thời tiết
6. **Responsive UI** hoạt động tốt trên mọi thiết bị

## Yêu cầu kỹ thuật
- **.NET 8** làm backend framework
- **Clean Architecture** với Domain-Driven Design
- **API RESTful** với OpenAPI/Swagger documentation
- **Entity Framework Core** cho data access
- **SQL Server** hoặc **PostgreSQL** cho database
- **Redis** cho caching
- **AutoMapper** cho object mapping
- **MediatR** cho CQRS pattern
- **FluentValidation** cho validation
- **Serilog** cho logging
- **JWT Authentication** cho bảo mật
- **Docker** containerization
- **Unit Testing** với xUnit và Moq

## Tích hợp bên ngoài
- **OpenWeatherMap API** cho dữ liệu thời tiết
- **Google Maps API** cho geocoding (optional)

## Giao diện người dùng
- **ASP.NET Core MVC** với modern UI
- **Bootstrap 5** hoặc **Tailwind CSS**
- **Chart.js** cho biểu đồ
- **Progressive Web App (PWA)** capabilities

## Tiêu chí thành công
- Ứng dụng load trong vòng 2 giây
- Dữ liệu được cache hiệu quả
- UI responsive và user-friendly
- Code coverage >= 80%
- Tuân thủ SOLID principles
- Dễ maintain và extend 
# Technical Context

## Core Technologies
1. .NET 8
   - ASP.NET Core Web API
   - Entity Framework Core
   - Dependency Injection
   - Async/Await patterns

2. External Services
   - OpenWeatherMap API
   - Redis for caching
   - Prometheus for metrics
   - Hangfire for background jobs

3. Security
   - JWT authentication
   - Role-based authorization
   - Rate limiting
   - HTTPS/TLS

4. Monitoring & Logging
   - Prometheus metrics
   - Serilog logging
   - Health checks
   - Error tracking

## Development Setup
1. Required Tools
   - .NET 8 SDK
   - Docker Desktop
   - Redis
   - Visual Studio 2022 / VS Code

2. Environment Setup
   ```bash
   # Start Redis
   docker run --name redis -p 6379:6379 -d redis

   # Build and run application
   dotnet build
   dotnet run
   ```

3. Configuration
   - appsettings.json: Application settings
   - appsettings.Development.json: Development settings
   - appsettings.Production.json: Production settings

## Dependencies
1. NuGet Packages
   - Microsoft.AspNetCore.Authentication.JwtBearer
   - StackExchange.Redis
   - Hangfire
   - Prometheus.NET
   - Serilog
   - Serilog.Sinks.Console
   - Serilog.Sinks.File

2. Development Dependencies
   - xUnit
   - Moq
   - FluentAssertions
   - Microsoft.NET.Test.Sdk

## Technical Constraints
1. Performance
   - Redis caching
   - Rate limiting
   - Background jobs
   - Optimized API calls

2. Security
   - JWT authentication
   - Role-based access
   - HTTPS required
   - API key protection

3. Monitoring
   - Prometheus metrics
   - Health checks
   - Error tracking
   - Performance monitoring

4. Scalability
   - Distributed caching
   - Background processing
   - Load balancing ready
   - Containerized deployment

## API Documentation
1. Swagger UI
   - /swagger: API documentation
   - Authentication support
   - Request/Response examples
   - Schema definitions

2. Health Checks
   - /health: System health
   - Component status
   - Detailed diagnostics
   - Metrics endpoint

3. Monitoring
   - /metrics: Prometheus metrics
   - Custom metrics
   - Performance data
   - Error rates

## Deployment
1. Docker Support
   - Multi-stage builds
   - Environment configuration
   - Health checks
   - Volume mapping

2. CI/CD Ready
   - GitHub Actions
   - Docker builds
   - Automated testing
   - Deployment scripts

## Technology Stack

### Backend Framework
- **.NET 8** - Latest LTS version với performance improvements
- **ASP.NET Core 8** - Web framework with minimal APIs
- **C# 12** - Latest language features

### Database & ORM
- **SQL Server** hoặc **PostgreSQL** - Primary database
- **Entity Framework Core 8** - ORM với Code-First approach
- **Redis** - Distributed caching và session storage

### Authentication & Authorization
- **JWT Bearer tokens** - Stateless authentication
- **ASP.NET Core Identity** - User management
- **OAuth 2.0** - Third-party authentication (optional)

### API & Communication
- **OpenAPI/Swagger** - API documentation
- **RESTful APIs** - HTTP-based communication
- **HttpClient** - External API calls với Polly resilience

### Architecture Patterns
- **MediatR** - CQRS và Mediator pattern
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **Scrutor** - Assembly scanning for DI

### Logging & Monitoring
- **Serilog** - Structured logging
- **Application Insights** (optional) - Monitoring
- **Health Checks** - System health monitoring

### Testing
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library
- **TestContainers** - Integration testing với containers

### Frontend
- **ASP.NET Core MVC** - Server-side rendering
- **Bootstrap 5** - CSS framework
- **Chart.js** - Data visualization
- **Alpine.js** hoặc **vanilla JS** - Client-side interactivity

### DevOps & Deployment
- **Docker** - Containerization
- **Docker Compose** - Multi-container orchestration
- **GitHub Actions** hoặc **Azure DevOps** - CI/CD pipeline

## Development Environment Setup

### Prerequisites
- **.NET 8 SDK**
- **Docker Desktop**
- **SQL Server Developer Edition** hoặc **PostgreSQL**
- **Redis** (via Docker hoặc local install)
- **Visual Studio 2022** hoặc **VS Code**

### External Services
- **OpenWeatherMap API**
  - Free tier: 1000 calls/day
  - Paid plans available cho production
  - Cần API key cho development

### Configuration Management
- **appsettings.json** - Base configuration
- **appsettings.Development.json** - Dev-specific settings
- **Environment variables** - Sensitive configuration
- **Azure Key Vault** (optional) - Production secrets

### Package References (Key NuGet Packages)
```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" />
<PackageReference Include="MediatR" />
<PackageReference Include="FluentValidation.AspNetCore" />
<PackageReference Include="Serilog.Extensions.Hosting" />
<PackageReference Include="StackExchange.Redis" />
<PackageReference Include="Polly" />
<PackageReference Include="Scrutor" />
``` 
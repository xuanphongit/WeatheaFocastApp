# Technical Context

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
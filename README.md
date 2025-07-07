Flota365.Platform.API

Flota365.Platform.API is a backend solution built on .NET 7+ that follows Domain Driven Design (DDD) patterns for fleet, personnel, and logistics management. The project is organized into independent modules for maintainability and scalability.

Project Structure

The solution uses a modular, DDD-based architecture, splitting each module into well-defined layers:

Flota365.Platform.API/
│
├── Analytics/             # Analytics module
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
├── FleetManagement/       # Fleet management module
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
├── IAM/                   # Identity and Access Management
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
├── Maintenance/           # Maintenance module
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
├── Personnel/             # Personnel management module
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
├── Shared/                # Shared resources and utilities
│   ├── Application/
│   ├── Domain/
│   └── Infrastructure/
├── Program.cs             # Main entry point
├── appsettings.Development.json
├── appsettings.json
└── appsettings.Production.json

Each module includes these layers:
- Application/: Application logic, use cases (CQRS, handlers, services)
- Domain/: Entities, Value Objects, Aggregates, repository interfaces, domain events
- Infrastructure/: Persistence implementations, external services, integration, events
- Interfaces/: API controllers and exposed DTOs (REST entry points)

Main Technologies

- .NET 7+
- Entity Framework Core
- MediatR
- AutoMapper
- JWT (authentication)
- Swagger/OpenAPI
- SQL Server (or any RDBMS)

Installation & Running

1. Clone this repository:
    git clone https://github.com/1ASI0730-2510-4370-G3-Flota365/VSC-Visionaries-API.git
    cd Flota365.Platform.API

2. Restore NuGet packages:
    dotnet restore

3. Build the solution:
    dotnet build

4. Run the API:
    dotnet run --project Flota365.Platform.API

5. Access Swagger docs at:  
   http://localhost:5000/swagger  
   (or as configured)

Contribution

1. Fork the repository.
2. Create a new branch (feature/your-branch-name).
3. Commit your changes.
4. Submit a Pull Request.

Best Practices

- Follow DDD folder and code conventions.
- Apply SOLID principles in domain and application services.
- All new features should include unit tests.

License

This project is owned by the VSC-Visionaries team and is restricted to authorized contributors.

Contact:  
For any questions, contact: soporte@flota365.com

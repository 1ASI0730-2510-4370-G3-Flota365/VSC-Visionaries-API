using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Flota365.Platform.API.Shared.Domain.Model.Events;
using Flota365.Platform.API.Shared.Infrastructure.Events;
using Flota365.Platform.API.Shared.Infrastructure.Middleware;

namespace Flota365.Platform.API.Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            // Core services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Configuration
            services.Configure<ApplicationSettings>(configuration => { });
            services.Configure<DatabaseSettings>(configuration => { });
            services.Configure<CachingSettings>(configuration => { });
            services.Configure<LoggingSettings>(configuration => { });

            return services;
        }

        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add configuration sections
            services.Configure<ApplicationSettings>(configuration.GetSection("Application"));
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            services.Configure<CachingSettings>(configuration.GetSection("Caching"));
            services.Configure<LoggingSettings>(configuration.GetSection("Logging"));

            // Add shared services
            services.AddSharedServices();

            // Add memory cache
            services.AddMemoryCache();

            // Add HTTP context accessor
            services.AddHttpContextAccessor();

            return services;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedMiddleware(this IApplicationBuilder app)
        {
            // Request logging (before other middleware)
            app.UseMiddleware<RequestLoggingMiddleware>();
            
            // Exception handling (should be early in pipeline)
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Use shared middleware
            app.UseSharedMiddleware();

            // Additional infrastructure setup can go here
            
            return app;
        }
    }
}
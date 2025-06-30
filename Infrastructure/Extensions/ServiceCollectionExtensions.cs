using Flota365.API.Domain.Interfaces;
using Flota365.API.Infrastructure.Data.Repositories;
using Flota365.API.Application.Services;

namespace Flota365.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IFleetRepository, FleetRepository>(); // AGREGADO: Faltaba este
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application Services
            services.AddScoped<VehicleService>();
            services.AddScoped<DriverService>();
            services.AddScoped<DashboardService>();

            return services;
        }
    }
}
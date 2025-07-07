using Microsoft.Extensions.DependencyInjection;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Infrastructure.Persistence.EFC.Repositories;

namespace Flota365.Platform.API.FleetManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFleetManagementServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IFleetRepository, FleetRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            
            return services;
        }
    }
}
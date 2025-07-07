using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Flota365.Platform.API.Maintenance.Domain.Model.Commands;
using Flota365.Platform.API.Maintenance.Domain.Model.Queries;
using Flota365.Platform.API.Maintenance.Interfaces.REST.Resources;
using Flota365.Platform.API.Maintenance.Domain.Repositories;


// ServiceCollectionExtensions.cs
namespace Flota365.Platform.API.Maintenance.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMaintenanceServices(this IServiceCollection services)
        {
            // Repositories would be registered here when implemented
            // services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
            // services.AddScoped<IServiceRecordRepository, ServiceRecordRepository>();
            
            return services;
        }
    }
}
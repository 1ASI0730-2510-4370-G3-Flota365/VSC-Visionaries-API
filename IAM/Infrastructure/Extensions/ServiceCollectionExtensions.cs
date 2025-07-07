using Microsoft.Extensions.DependencyInjection;
using Flota365.Platform.API.IAM.Domain.Repositories;
using Flota365.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

namespace Flota365.Platform.API.IAM.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIAMServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
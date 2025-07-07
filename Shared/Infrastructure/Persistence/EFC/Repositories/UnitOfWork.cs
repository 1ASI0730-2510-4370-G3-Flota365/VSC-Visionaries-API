
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Flota365.Platform.API.Shared.Domain.Repositories;
using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.Personnel.Domain.Model.Aggregates;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;

namespace Flota365.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
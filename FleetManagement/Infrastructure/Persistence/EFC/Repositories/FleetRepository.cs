using Microsoft.EntityFrameworkCore;
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.Shared.Infrastructure.Persistence.EFC;

// FleetRepository.cs
namespace Flota365.Platform.API.FleetManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class FleetRepository : IFleetRepository
    {
        private readonly ApplicationDbContext _context;

        public FleetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Fleet?> FindByIdAsync(int id)
        {
            return await _context.Fleets.FindAsync(id);
        }

        public async Task<IEnumerable<Fleet>> ListAsync()
        {
            return await _context.Fleets
                .Include(f => f.Vehicles)
                .ToListAsync();
        }

        public async Task AddAsync(Fleet entity)
        {
            await _context.Fleets.AddAsync(entity);
        }

        public void Update(Fleet entity)
        {
            _context.Fleets.Update(entity);
        }

        public void Remove(Fleet entity)
        {
            _context.Fleets.Remove(entity);
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _context.Fleets.AnyAsync(f => f.Code == code);
        }

        public async Task<Fleet?> FindByCodeAsync(string code)
        {
            return await _context.Fleets
                .Include(f => f.Vehicles)
                .FirstOrDefaultAsync(f => f.Code == code);
        }

        public async Task<IEnumerable<Fleet>> FindByTypeAsync(FleetType type)
        {
            return await _context.Fleets
                .Include(f => f.Vehicles)
                .Where(f => f.Type == type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Fleet>> FindActiveFleetAsync()
        {
            return await _context.Fleets
                .Include(f => f.Vehicles)
                .Where(f => f.IsActive)
                .OrderBy(f => f.Name)
                .ToListAsync();
        }

        public async Task<Fleet?> FindByIdWithVehiclesAsync(int id)
        {
            return await _context.Fleets
                .Include(f => f.Vehicles)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}

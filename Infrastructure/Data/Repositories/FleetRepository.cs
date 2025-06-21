using Microsoft.EntityFrameworkCore;
using Flota365.API.Domain.Entities;
using Flota365.API.Domain.Interfaces;

namespace Flota365.API.Infrastructure.Data.Repositories
{
    public class FleetRepository : IFleetRepository
    {
        private readonly ApplicationDbContext _context;

        public FleetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fleet>> GetAllAsync()
        {
            return await _context.Fleets
                .Include(f => f.Vehicles.Where(v => v.IsActive))
                .Where(f => f.IsActive)
                .ToListAsync();
        }

        public async Task<Fleet?> GetByIdAsync(int id)
        {
            return await _context.Fleets
                .Include(f => f.Vehicles.Where(v => v.IsActive))
                .FirstOrDefaultAsync(f => f.Id == id && f.IsActive);
        }

        public async Task<Fleet> CreateAsync(Fleet fleet)
        {
            // Generate fleet code
            var lastFleet = await _context.Fleets
                .OrderByDescending(f => f.Id)
                .FirstOrDefaultAsync();
            var nextNumber = lastFleet != null ? lastFleet.Id + 1 : 1;
            fleet.Code = $"FL-{nextNumber:D3}";

            _context.Fleets.Add(fleet);
            return fleet;
        }
    }
}
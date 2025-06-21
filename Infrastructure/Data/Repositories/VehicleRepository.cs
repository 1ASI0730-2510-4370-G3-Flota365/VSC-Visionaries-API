using Microsoft.EntityFrameworkCore;
using Flota365.API.Domain.Entities;
using Flota365.API.Domain.Interfaces;
using Flota365.API.Domain.Enums;
using Flota365.API.Infrastructure.Data;

namespace Flota365.API.Infrastructure.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Include(v => v.Driver)
                .Where(v => v.IsActive)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Include(v => v.Driver)
                .FirstOrDefaultAsync(v => v.Id == id && v.IsActive);
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            return vehicle;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            vehicle.UpdatedAt = DateTime.UtcNow;
            _context.Vehicles.Update(vehicle);
            // Para eliminar el warning, usamos Task.CompletedTask
            await Task.CompletedTask;
            return vehicle;
        }

        public async Task DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                vehicle.IsActive = false;
                vehicle.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task<bool> LicensePlateExistsAsync(string licensePlate, int? excludeId = null)
        {
            var query = _context.Vehicles.Where(v => v.LicensePlate == licensePlate && v.IsActive);
            
            if (excludeId.HasValue)
            {
                query = query.Where(v => v.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<int> GetVehiclesInMaintenanceCountAsync()
        {
            return await _context.Vehicles
                .CountAsync(v => v.Status == VehicleStatus.Maintenance && v.IsActive);
        }
    }
}
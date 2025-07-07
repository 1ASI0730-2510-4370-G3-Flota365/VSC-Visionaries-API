using Microsoft.EntityFrameworkCore;
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.Shared.Infrastructure.Persistence.EFC;


// VehicleRepository.cs
namespace Flota365.Platform.API.FleetManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> FindByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<IEnumerable<Vehicle>> ListAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .ToListAsync();
        }

        public async Task AddAsync(Vehicle entity)
        {
            await _context.Vehicles.AddAsync(entity);
        }

        public void Update(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
        }

        public void Remove(Vehicle entity)
        {
            _context.Vehicles.Remove(entity);
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .AnyAsync(v => v.LicensePlate.Value == licensePlate);
        }

        public async Task<Vehicle?> FindByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .FirstOrDefaultAsync(v => v.LicensePlate.Value == licensePlate);
        }

        public async Task<IEnumerable<Vehicle>> FindByFleetIdAsync(int fleetId)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.FleetId == fleetId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> FindByDriverIdAsync(int driverId)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.DriverId == driverId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> FindByStatusAsync(VehicleStatus status)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> FindActiveVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.IsActive && v.Status == VehicleStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> FindVehiclesInMaintenanceAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.Status == VehicleStatus.Maintenance)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> FindVehiclesDueForServiceAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.NextServiceDate.HasValue && v.NextServiceDate.Value <= DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<Vehicle?> FindByIdWithFleetAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> FindUnassignedVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Fleet)
                .Where(v => v.FleetId == null && v.IsActive)
                .ToListAsync();
        }
    }
}
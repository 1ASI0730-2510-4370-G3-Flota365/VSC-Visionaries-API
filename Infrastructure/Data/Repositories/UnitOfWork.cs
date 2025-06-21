using Flota365.API.Domain.Interfaces;

namespace Flota365.API.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IVehicleRepository? _vehicles;
        private IDriverRepository? _drivers;
        private IFleetRepository? _fleets;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IVehicleRepository Vehicles => 
            _vehicles ??= new VehicleRepository(_context);

        public IDriverRepository Drivers => 
            _drivers ??= new DriverRepository(_context);

        public IFleetRepository Fleets => 
            _fleets ??= new FleetRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
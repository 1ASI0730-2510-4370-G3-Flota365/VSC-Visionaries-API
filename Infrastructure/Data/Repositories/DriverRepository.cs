using Microsoft.EntityFrameworkCore;
using Flota365.API.Domain.Entities;
using Flota365.API.Domain.Interfaces;
using Flota365.API.Domain.Enums;
using Flota365.API.Infrastructure.Data;

namespace Flota365.API.Infrastructure.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbContext _context;

        public DriverRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers
                .Include(d => d.Vehicles)
                .Where(d => d.IsActive)
                .OrderBy(d => d.Code)
                .ToListAsync();
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            return await _context.Drivers
                .Include(d => d.Vehicles)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Driver> CreateAsync(Driver driver)
        {
            // Generate driver code
            var lastDriver = await _context.Drivers
                .OrderByDescending(d => d.Id)
                .FirstOrDefaultAsync();
            var nextNumber = lastDriver != null ? lastDriver.Id + 1 : 1;
            driver.Code = $"DR-{nextNumber:D3}";

            await _context.Drivers.AddAsync(driver);
            return driver;
        }

        public async Task<Driver> UpdateAsync(Driver driver)
        {
            driver.UpdatedAt = DateTime.UtcNow;
            _context.Drivers.Update(driver);
            // Para eliminar el warning, usamos Task.CompletedTask
            await Task.CompletedTask;
            return driver;
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                driver.IsActive = false;
                driver.UpdatedAt = DateTime.UtcNow;
            }
        }

        // MÉTODOS FALTANTES IMPLEMENTADOS:

        public async Task<bool> LicenseNumberExistsAsync(string licenseNumber, int? excludeId = null)
        {
            var query = _context.Drivers.Where(d => d.LicenseNumber == licenseNumber && d.IsActive);
            
            if (excludeId.HasValue)
            {
                query = query.Where(d => d.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<int> GetActiveDriversCountAsync()
        {
            return await _context.Drivers
                .CountAsync(d => d.Status == DriverStatus.Active && d.IsActive);
        }

        public async Task<int> GetLicensesExpiringSoonCountAsync()
        {
            return await _context.Drivers
                .CountAsync(d => d.IsActive && d.LicenseExpiryDate <= DateTime.UtcNow.AddDays(30));
        }
    }
}
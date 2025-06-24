using Flota365.API.Domain.Entities;

namespace Flota365.API.Domain.Interfaces
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver?> GetByIdAsync(int id);
        Task<Driver> CreateAsync(Driver driver);
        Task<Driver> UpdateAsync(Driver driver);
        Task DeleteAsync(int id);
        
        Task<bool> LicenseNumberExistsAsync(string licenseNumber, int? excludeId = null);
        Task<int> GetActiveDriversCountAsync();
        Task<int> GetLicensesExpiringSoonCountAsync();
    }
}
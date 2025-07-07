using Flota365.Platform.API.Personnel.Domain.Model.Aggregates;
using Flota365.Platform.API.Personnel.Domain.Model.ValueObjects;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.Personnel.Domain.Repositories
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        Task<bool> ExistsByCodeAsync(string code);
        Task<bool> ExistsByLicenseNumberAsync(string licenseNumber);
        Task<Driver?> FindByCodeAsync(string code);
        Task<Driver?> FindByLicenseNumberAsync(string licenseNumber);
        Task<IEnumerable<Driver>> FindByStatusAsync(DriverStatus status);
        Task<IEnumerable<Driver>> FindActiveDriversAsync();
        Task<IEnumerable<Driver>> FindAvailableDriversAsync();
        Task<IEnumerable<Driver>> FindDriversWithExpiredLicensesAsync();
        Task<IEnumerable<Driver>> FindDriversWithExpiringSoonLicensesAsync(int daysThreshold = 30);
        Task<IEnumerable<Driver>> FindByExperienceLevelAsync(string level);
        Task<bool> HasAssignedVehiclesAsync(int driverId);
        Task<int> GetActiveDriversCountAsync();
    }
}
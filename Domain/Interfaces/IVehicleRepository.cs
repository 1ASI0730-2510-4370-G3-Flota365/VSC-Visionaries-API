using Flota365.API.Domain.Entities;

namespace Flota365.API.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(int id);
        Task<Vehicle> CreateAsync(Vehicle vehicle);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(int id);
        Task<bool> LicensePlateExistsAsync(string licensePlate, int? excludeId = null);
        Task<int> GetVehiclesInMaintenanceCountAsync();
    }
}
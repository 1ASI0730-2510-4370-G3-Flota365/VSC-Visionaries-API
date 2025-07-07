using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;
using Flota365.Platform.API.Shared.Domain.Repositories;

// IVehicleRepository.cs
namespace Flota365.Platform.API.FleetManagement.Domain.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
        Task<Vehicle?> FindByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Vehicle>> FindByFleetIdAsync(int fleetId);
        Task<IEnumerable<Vehicle>> FindByDriverIdAsync(int driverId);
        Task<IEnumerable<Vehicle>> FindByStatusAsync(VehicleStatus status);
        Task<IEnumerable<Vehicle>> FindActiveVehiclesAsync();
        Task<IEnumerable<Vehicle>> FindVehiclesInMaintenanceAsync();
        Task<IEnumerable<Vehicle>> FindVehiclesDueForServiceAsync();
        Task<Vehicle?> FindByIdWithFleetAsync(int id);
        Task<IEnumerable<Vehicle>> FindUnassignedVehiclesAsync();
    }
}
using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;


// Vehicle Queries
namespace Flota365.Platform.API.FleetManagement.Domain.Model.Queries
{
    public record GetAllVehiclesQuery() : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehicleByIdQuery(int VehicleId) : IQuery<VehicleResource?>;
    
    public record GetVehicleByLicensePlateQuery(string LicensePlate) : IQuery<VehicleResource?>;
    
    public record GetVehiclesByFleetIdQuery(int FleetId) : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehiclesByStatusQuery(string Status) : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehiclesByDriverIdQuery(int DriverId) : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetActiveVehiclesQuery() : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehiclesInMaintenanceQuery() : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehiclesDueForServiceQuery() : IQuery<IEnumerable<VehicleResource>>;

    public record GetVehiclesDueForServiceQuery() : IQuery<IEnumerable<VehicleResource>>;
    
    public record GetVehiclesInMaintenanceQuery() : IQuery<IEnumerable<VehicleResource>>;
}
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.Commands;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;

namespace Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform
{
    public static class FleetResourceFromEntityAssembler
    {
        public static FleetResource ToResourceFromEntity(Fleet entity)
        {
            return new FleetResource(
                entity.Id,
                entity.Code,
                entity.Name,
                entity.Description,
                entity.Type.ToString(),
                entity.IsActive,
                entity.Vehicles.Count,
                entity.GetActiveVehicleCount(),
                entity.Vehicles.Count(v => v.Status == VehicleStatus.Maintenance),
                entity.GetPerformanceRate(),
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}

using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Commands
{
    public record AssignVehicleToDriverCommand(
        int VehicleId,
        int DriverId
    ) : ICommand<bool>;
}

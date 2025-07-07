using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Commands
{
    public record AssignVehicleToFleetCommand(
        int VehicleId,
        int FleetId
    ) : ICommand<bool>;
}

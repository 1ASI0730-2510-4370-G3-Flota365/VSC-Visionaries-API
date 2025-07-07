using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;

namespace Flota365.Platform.API.FleetManagement.Domain.Model.Commands
{
    public record CreateFleetCommand(
        string Code,
        string Name,
        string Description,
        FleetType Type
    ) : ICommand<int>;
}

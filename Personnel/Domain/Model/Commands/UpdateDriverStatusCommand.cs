using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Personnel.Domain.Model.ValueObjects;

namespace Flota365.Platform.API.Personnel.Domain.Model.Commands
{
    public record UpdateDriverStatusCommand(
        int DriverId,
        DriverStatus Status
    ) : ICommand<bool>;
}

using Flota365.Platform.API.Shared.Domain.Model;

namespace Flota365.Platform.API.Personnel.Domain.Model.Commands
{
    public record DeactivateDriverCommand(
        int DriverId
    ) : ICommand<bool>;
}

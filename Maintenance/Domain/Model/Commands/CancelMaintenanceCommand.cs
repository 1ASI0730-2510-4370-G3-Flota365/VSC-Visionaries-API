using Flota365.Platform.API.Shared.Domain.Model;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record CancelMaintenanceCommand(
int MaintenanceId,
string Reason
) : ICommand<bool>;
}
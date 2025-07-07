using Flota365.Platform.API.Shared.Domain.Model;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record CompleteMaintenanceCommand(
int MaintenanceId,
decimal ActualCost,
string CompletionNotes
) : ICommand<bool>;
}
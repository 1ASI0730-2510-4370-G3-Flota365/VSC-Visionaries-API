using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record UpdateMaintenanceCommand(
int MaintenanceId,
string Description,
MaintenanceType Type,
decimal EstimatedCost,
DateTime ScheduledDate,
string ServiceProvider,
string Notes,
MaintenanceStatus Status
) : ICommand<bool>;
}
using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record ScheduleMaintenanceCommand(
int VehicleId,
string Description,
MaintenanceType Type,
decimal EstimatedCost,
DateTime ScheduledDate,
string ServiceProvider,
int Priority = 3
) : ICommand<int>;
}
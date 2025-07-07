using Flota365.Platform.API.Shared.Domain.Model;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record RescheduleMaintenanceCommand(
int MaintenanceId,
DateTime NewScheduledDate,
string Reason
) : ICommand<bool>;
}
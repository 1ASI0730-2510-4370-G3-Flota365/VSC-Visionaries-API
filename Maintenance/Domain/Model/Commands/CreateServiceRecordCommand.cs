using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record CreateServiceRecordCommand(
int VehicleId,
ServiceType ServiceType,
string Description,
decimal Cost,
DateTime ServiceDate,
int MileageAtService,
string ServiceProvider,
string TechnicianName,
string PartsUsed,
string Notes
) : ICommand<int>;
}
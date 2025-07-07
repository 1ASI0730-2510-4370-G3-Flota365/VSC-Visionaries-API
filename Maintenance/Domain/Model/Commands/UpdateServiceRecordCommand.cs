using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Commands
{
public record UpdateServiceRecordCommand(
int ServiceRecordId,
string Description,
decimal Cost,
string ServiceProvider,
string TechnicianName,
ServiceQuality Quality,
string PartsUsed,
string Notes
) : ICommand<bool>;
}
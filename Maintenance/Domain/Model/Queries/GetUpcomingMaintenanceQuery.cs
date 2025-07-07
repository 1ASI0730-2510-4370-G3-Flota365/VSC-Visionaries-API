using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Interfaces.REST.Resources;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Queries
{
public record GetUpcomingMaintenanceQuery(int DaysThreshold = 7) : IQuery<IEnumerable<MaintenanceRecordResource>>;
}
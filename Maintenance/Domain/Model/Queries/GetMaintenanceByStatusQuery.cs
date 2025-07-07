using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Interfaces.REST.Resources;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Queries
{
public record GetMaintenanceByStatusQuery(MaintenanceStatus Status) : IQuery<IEnumerable<MaintenanceRecordResource>>;
}
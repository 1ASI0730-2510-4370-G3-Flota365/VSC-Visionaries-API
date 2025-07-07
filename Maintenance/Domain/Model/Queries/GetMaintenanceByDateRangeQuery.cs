using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Interfaces.REST.Resources;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Queries
{
public record GetMaintenanceByDateRangeQuery(DateTime StartDate, DateTime EndDate) : IQuery<IEnumerable<MaintenanceRecordResource>>;
}
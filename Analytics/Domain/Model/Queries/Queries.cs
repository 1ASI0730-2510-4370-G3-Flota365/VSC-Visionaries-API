using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Analytics.Interfaces.REST.Resources;

namespace Flota365.Platform.API.Analytics.Domain.Model.Queries
{
    public record GetDashboardStatsQuery() : IQuery<DashboardStatsResource>;
    
    public record GetFleetSummaryQuery() : IQuery<FleetSummaryResource>;
    
    public record GetActiveVehiclesQuery() : IQuery<IEnumerable<ActiveVehicleResource>>;
}
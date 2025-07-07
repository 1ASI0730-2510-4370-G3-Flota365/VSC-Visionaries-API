using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;

// Fleet Queries
namespace Flota365.Platform.API.FleetManagement.Domain.Model.Queries
{
    public record GetAllFleetsQuery() : IQuery<IEnumerable<FleetResource>>;
    
    public record GetFleetByIdQuery(int FleetId) : IQuery<FleetResource?>;
    
    public record GetActiveFleetQuery() : IQuery<IEnumerable<FleetResource>>;
    
    public record GetFleetByTypeQuery(string Type) : IQuery<IEnumerable<FleetResource>>;
    
}
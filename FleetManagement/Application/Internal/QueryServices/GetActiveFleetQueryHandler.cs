using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// GetActiveFleetQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetActiveFleetQueryHandler : IRequestHandler<GetActiveFleetQuery, IEnumerable<FleetResource>>
    {
        private readonly IFleetRepository _fleetRepository;

        public GetActiveFleetQueryHandler(IFleetRepository fleetRepository)
        {
            _fleetRepository = fleetRepository;
        }

        public async Task<IEnumerable<FleetResource>> Handle(GetActiveFleetQuery request, CancellationToken cancellationToken)
        {
            var fleets = await _fleetRepository.FindActiveFleetAsync();
            return fleets.Select(FleetResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}

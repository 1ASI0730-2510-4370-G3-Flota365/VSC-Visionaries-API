using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;

// GetFleetByTypeQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetFleetByTypeQueryHandler : IRequestHandler<GetFleetByTypeQuery, IEnumerable<FleetResource>>
    {
        private readonly IFleetRepository _fleetRepository;

        public GetFleetByTypeQueryHandler(IFleetRepository fleetRepository)
        {
            _fleetRepository = fleetRepository;
        }

        public async Task<IEnumerable<FleetResource>> Handle(GetFleetByTypeQuery request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<FleetType>(request.Type, true, out var fleetType))
                return Enumerable.Empty<FleetResource>();

            var fleets = await _fleetRepository.FindByTypeAsync(fleetType);
            return fleets.Select(FleetResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}

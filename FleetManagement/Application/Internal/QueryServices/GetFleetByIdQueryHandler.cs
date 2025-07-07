using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// GetFleetByIdQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetFleetByIdQueryHandler : IRequestHandler<GetFleetByIdQuery, FleetResource?>
    {
        private readonly IFleetRepository _fleetRepository;

        public GetFleetByIdQueryHandler(IFleetRepository fleetRepository)
        {
            _fleetRepository = fleetRepository;
        }

        public async Task<FleetResource?> Handle(GetFleetByIdQuery request, CancellationToken cancellationToken)
        {
            var fleet = await _fleetRepository.FindByIdWithVehiclesAsync(request.FleetId);
            return fleet != null ? FleetResourceFromEntityAssembler.ToResourceFromEntity(fleet) : null;
        }
    }
}

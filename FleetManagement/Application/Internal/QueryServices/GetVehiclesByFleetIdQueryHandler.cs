using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// GetVehiclesByFleetIdQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetVehiclesByFleetIdQueryHandler : IRequestHandler<GetVehiclesByFleetIdQuery, IEnumerable<VehicleResource>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesByFleetIdQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleResource>> Handle(GetVehiclesByFleetIdQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.FindByFleetIdAsync(request.FleetId);
            return vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}

using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// GetVehicleByIdQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleResource?>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResource?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.FindByIdWithFleetAsync(request.VehicleId);
            return vehicle != null ? VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle) : null;
        }
    }
}

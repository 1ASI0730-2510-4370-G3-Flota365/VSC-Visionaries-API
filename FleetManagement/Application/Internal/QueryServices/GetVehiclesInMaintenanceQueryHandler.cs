using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetVehiclesInMaintenanceQueryHandler : IRequestHandler<GetVehiclesInMaintenanceQuery, IEnumerable<VehicleResource>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesInMaintenanceQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleResource>> Handle(GetVehiclesInMaintenanceQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.FindVehiclesInMaintenanceAsync();
            return vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}
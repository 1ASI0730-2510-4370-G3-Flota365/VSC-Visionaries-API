using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetVehiclesDueForServiceQueryHandler : IRequestHandler<GetVehiclesDueForServiceQuery, IEnumerable<VehicleResource>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesDueForServiceQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleResource>> Handle(GetVehiclesDueForServiceQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.FindVehiclesDueForServiceAsync();
            return vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        }
    }
}
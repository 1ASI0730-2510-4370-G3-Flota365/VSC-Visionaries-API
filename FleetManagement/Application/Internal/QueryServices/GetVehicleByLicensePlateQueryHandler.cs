using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Queries;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Interfaces.REST.Transform;

// GetVehicleByLicensePlateQueryHandler.cs
namespace Flota365.Platform.API.FleetManagement.Application.Internal.QueryServices
{
    public class GetVehicleByLicensePlateQueryHandler : IRequestHandler<GetVehicleByLicensePlateQuery, VehicleResource?>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleByLicensePlateQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResource?> Handle(GetVehicleByLicensePlateQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.FindByLicensePlateAsync(request.LicensePlate);
            return vehicle != null ? VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle) : null;
        }
    }
}

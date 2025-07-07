using MediatR;
using Flota365.Platform.API.Analytics.Domain.Model.Queries;
using Flota365.Platform.API.Analytics.Interfaces.REST.Resources;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;

namespace Flota365.Platform.API.Analytics.Application.Internal.QueryServices
{
    public class GetActiveVehiclesQueryHandler : IRequestHandler<GetActiveVehiclesQuery, IEnumerable<ActiveVehicleResource>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetActiveVehiclesQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<ActiveVehicleResource>> Handle(GetActiveVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.FindActiveVehiclesAsync();
            
            return vehicles.Take(10).Select(v => new ActiveVehicleResource(
                Id: v.Id,
                LicensePlate: v.LicensePlate.Value,
                Model: $"{v.Brand} {v.Model} {v.Year}",
                DriverName: "No asignado",
                Status: v.Status.ToString(),
                FleetName: v.Fleet?.Name ?? "Sin flota",
                LastUpdate: v.UpdatedAt
            ));
        }
    }
}
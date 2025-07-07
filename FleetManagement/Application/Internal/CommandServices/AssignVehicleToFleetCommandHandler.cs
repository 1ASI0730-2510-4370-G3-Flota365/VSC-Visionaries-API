using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Commands;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.FleetManagement.Application.Internal.CommandServices
{
    public class AssignVehicleToFleetCommandHandler : IRequestHandler<AssignVehicleToFleetCommand, bool>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignVehicleToFleetCommandHandler(IVehicleRepository vehicleRepository, IFleetRepository fleetRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _fleetRepository = fleetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AssignVehicleToFleetCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(request.VehicleId);
            if (vehicle == null) return false;

            var fleet = await _fleetRepository.FindByIdAsync(request.FleetId);
            if (fleet == null || !fleet.IsActive)
                throw new InvalidOperationException("Fleet not found or inactive");

            vehicle.AssignToFleet(request.FleetId);
            _vehicleRepository.Update(vehicle);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

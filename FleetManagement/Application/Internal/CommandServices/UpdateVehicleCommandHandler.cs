using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Commands;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.FleetManagement.Application.Internal.CommandServices
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(request.VehicleId);
            if (vehicle == null) return false;

            var existingVehicle = await _vehicleRepository.FindByLicensePlateAsync(request.LicensePlate);
            if (existingVehicle != null && existingVehicle.Id != request.VehicleId)
                throw new InvalidOperationException("License plate already exists");

            vehicle.UpdateVehicleInfo(request.Brand, request.Model, request.Year);
            vehicle.UpdateMileage(request.Mileage);

            if (request.FleetId.HasValue)
                vehicle.AssignToFleet(request.FleetId.Value);
            else
                vehicle.RemoveFromFleet();

            if (request.DriverId.HasValue)
                vehicle.AssignToDriver(request.DriverId.Value);
            else
                vehicle.RemoveFromDriver();

            if (request.NextServiceDate.HasValue)
                vehicle.ScheduleService(request.NextServiceDate.Value);

            _vehicleRepository.Update(vehicle);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

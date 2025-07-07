using MediatR;
using Flota365.Platform.API.FleetManagement.Domain.Model.Commands;
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.FleetManagement.Application.Internal.CommandServices
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (await _vehicleRepository.ExistsByLicensePlateAsync(request.LicensePlate))
                throw new InvalidOperationException("License plate already exists");

            var vehicle = new Vehicle(
                request.LicensePlate,
                request.Brand,
                request.Model,
                request.Year,
                request.Mileage,
                request.FleetId,
                request.DriverId
            );

            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();

            return vehicle.Id;
        }
    }
}

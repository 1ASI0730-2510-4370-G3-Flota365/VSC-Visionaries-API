namespace Flota365.Platform.API.Personnel.Application.Internal.CommandServices
{
    public class UpdateDriverStatusCommandHandler : IRequestHandler<UpdateDriverStatusCommand, bool>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDriverStatusCommandHandler(IDriverRepository driverRepository, IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateDriverStatusCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindByIdAsync(request.DriverId);
            if (driver == null) return false;

            switch (request.Status)
            {
                case Domain.Model.ValueObjects.DriverStatus.Available:
                    driver.SetAvailable(); break;
                case Domain.Model.ValueObjects.DriverStatus.OnRoute:
                    driver.SetOnRoute(); break;
                case Domain.Model.ValueObjects.DriverStatus.OnBreak:
                    driver.SetOnBreak(); break;
                case Domain.Model.ValueObjects.DriverStatus.Suspended:
                    driver.Suspend("Status updated"); break;
                case Domain.Model.ValueObjects.DriverStatus.Inactive:
                    driver.Deactivate(); break;
            }

            _driverRepository.Update(driver);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

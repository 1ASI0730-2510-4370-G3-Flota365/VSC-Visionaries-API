using MediatR;
using Flota365.Platform.API.Maintenance.Domain.Model.Commands;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
using Flota365.Platform.API.Maintenance.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.Maintenance.Application.Internal.CommandServices
{
    public class RescheduleMaintenanceCommandHandler : IRequestHandler<RescheduleMaintenanceCommand, bool>
    {
        private readonly IMaintenanceRecordRepository _maintenanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RescheduleMaintenanceCommandHandler(IMaintenanceRecordRepository maintenanceRepository, IUnitOfWork unitOfWork)
        {
            _maintenanceRepository = maintenanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RescheduleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _maintenanceRepository.FindByIdAsync(request.MaintenanceId);
            if (maintenance == null) return false;

            maintenance.RescheduleService(request.NewScheduledDate, request.Reason);
            _maintenanceRepository.Update(maintenance);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

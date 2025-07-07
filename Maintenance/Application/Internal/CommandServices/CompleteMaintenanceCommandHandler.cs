using MediatR;
using Flota365.Platform.API.Maintenance.Domain.Model.Commands;
using Flota365.Platform.API.Maintenance.Domain.Model.Aggregates;
using Flota365.Platform.API.Maintenance.Domain.Repositories;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.Maintenance.Application.Internal.CommandServices
{
    public class CompleteMaintenanceCommandHandler : IRequestHandler<CompleteMaintenanceCommand, bool>
    {
        private readonly IMaintenanceRecordRepository _maintenanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteMaintenanceCommandHandler(IMaintenanceRecordRepository maintenanceRepository, IUnitOfWork unitOfWork)
        {
            _maintenanceRepository = maintenanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CompleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _maintenanceRepository.FindByIdAsync(request.MaintenanceId);
            if (maintenance == null) return false;

            maintenance.ValidateForCompletion();
            maintenance.CompleteMaintenance(request.ActualCost, request.CompletionNotes);

            _maintenanceRepository.Update(maintenance);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

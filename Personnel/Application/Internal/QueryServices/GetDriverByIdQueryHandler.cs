using MediatR;
using Flota365.Platform.API.Personnel.Domain.Model.Queries;
using Flota365.Platform.API.Personnel.Domain.Repositories;
using Flota365.Platform.API.Personnel.Interfaces.REST.Resources;
using Flota365.Platform.API.Personnel.Interfaces.REST.Transform;

namespace Flota365.Platform.API.Personnel.Application.Internal.QueryServices
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverResource?>
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverByIdQueryHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<DriverResource?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.FindByIdAsync(request.DriverId);
            return driver != null ? DriverResourceFromEntityAssembler.ToResourceFromEntity(driver) : null;
        }
    }
}
using Flota365.Platform.API.FleetManagement.Domain.Model.Aggregates;
using Flota365.Platform.API.FleetManagement.Domain.Model.ValueObjects;
using Flota365.Platform.API.Shared.Domain.Repositories;

// IFleetRepository.cs
namespace Flota365.Platform.API.FleetManagement.Domain.Repositories
{
    public interface IFleetRepository : IBaseRepository<Fleet>
    {
        Task<bool> ExistsByCodeAsync(string code);
        Task<Fleet?> FindByCodeAsync(string code);
        Task<IEnumerable<Fleet>> FindByTypeAsync(FleetType type);
        Task<IEnumerable<Fleet>> FindActiveFleetAsync();
        Task<Fleet?> FindByIdWithVehiclesAsync(int id);
    }
}

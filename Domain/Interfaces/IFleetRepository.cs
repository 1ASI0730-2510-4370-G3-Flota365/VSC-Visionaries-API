using Flota365.API.Domain.Entities;

namespace Flota365.API.Domain.Interfaces
{
    public interface IFleetRepository
    {
        Task<IEnumerable<Fleet>> GetAllAsync();
        Task<Fleet?> GetByIdAsync(int id);
        Task<Fleet> CreateAsync(Fleet fleet);
    }
}
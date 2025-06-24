namespace Flota365.API.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository Vehicles { get; }
        IDriverRepository Drivers { get; }
        IFleetRepository Fleets { get; }
        Task<int> SaveChangesAsync();
    }
}
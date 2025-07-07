namespace Flota365.Platform.API.Shared.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.Shared.Domain.Repositories;

namespace Flota365.Platform.API.IAM.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IEnumerable<User>> FindActiveUsersAsync();
        Task<User?> FindByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;

namespace Flota365.Platform.API.IAM.Domain.Model.Queries
{
    public record GetActiveUsersQuery() : IQuery<IEnumerable<UserResource>>;
}
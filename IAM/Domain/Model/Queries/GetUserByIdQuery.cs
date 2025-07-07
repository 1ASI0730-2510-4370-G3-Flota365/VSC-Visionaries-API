using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;

// GetUserByIdQuery.cs
namespace Flota365.Platform.API.IAM.Domain.Model.Queries
{
    public record GetUserByIdQuery(int UserId) : IQuery<UserResource?>;
}
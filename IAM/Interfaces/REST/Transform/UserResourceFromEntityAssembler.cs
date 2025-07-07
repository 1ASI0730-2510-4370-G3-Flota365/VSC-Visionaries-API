using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;

// UserResourceFromEntityAssembler.cs
namespace Flota365.Platform.API.IAM.Interfaces.REST.Transform
{
    public static class UserResourceFromEntityAssembler
    {
        public static UserResource ToResourceFromEntity(User entity)
        {
            return new UserResource(
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.Email,
                entity.Role,
                entity.IsActive,
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}
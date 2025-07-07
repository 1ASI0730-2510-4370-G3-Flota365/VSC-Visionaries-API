using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;


// UpdateProfileCommandFromResourceAssembler.cs
namespace Flota365.Platform.API.IAM.Interfaces.REST.Transform
{
    public static class UpdateProfileCommandFromResourceAssembler
    {
        public static UpdateUserProfileCommand ToCommandFromResource(int userId, UpdateProfileResource resource)
        {
            return new UpdateUserProfileCommand(
                userId,
                resource.FirstName,
                resource.LastName,
                resource.Email
            );
        }
    }
}

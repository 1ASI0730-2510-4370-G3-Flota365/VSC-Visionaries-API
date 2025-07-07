using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;


// SignUpCommandFromResourceAssembler.cs
namespace Flota365.Platform.API.IAM.Interfaces.REST.Transform
{
    public static class SignUpCommandFromResourceAssembler
    {
        public static SignUpUserCommand ToCommandFromResource(SignUpResource resource)
        {
            return new SignUpUserCommand(
                resource.FirstName,
                resource.LastName,
                resource.Email,
                resource.Password,
                resource.Role
            );
        }
    }
}

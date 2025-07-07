using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;



// SignInCommandFromResourceAssembler.cs
namespace Flota365.Platform.API.IAM.Interfaces.REST.Transform
{
    public static class SignInCommandFromResourceAssembler
    {
        public static SignInUserCommand ToCommandFromResource(SignInResource resource)
        {
            return new SignInUserCommand(
                resource.Email,
                resource.Password
            );
        }
    }
}


using Flota365.Platform.API.IAM.Domain.Model.Aggregates;
using Flota365.Platform.API.IAM.Domain.Model.Commands;
using Flota365.Platform.API.IAM.Interfaces.REST.Resources;


// ChangePasswordCommandFromResourceAssembler.cs
namespace Flota365.Platform.API.IAM.Interfaces.REST.Transform
{
    public static class ChangePasswordCommandFromResourceAssembler
    {
        public static ChangeUserPasswordCommand ToCommandFromResource(int userId, ChangePasswordResource resource)
        {
            return new ChangeUserPasswordCommand(
                userId,
                resource.CurrentPassword,
                resource.NewPassword
            );
        }
    }
}
using Flota365.Platform.API.Shared.Domain.Model;

// SignUpUserCommand.cs
namespace Flota365.Platform.API.IAM.Domain.Model.Commands
{
    public record SignUpUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Role
    ) : ICommand<int>;
}

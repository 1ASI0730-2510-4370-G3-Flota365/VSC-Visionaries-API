using Flota365.Platform.API.Shared.Domain.Model;

// SignInUserCommand.cs
namespace Flota365.Platform.API.IAM.Domain.Model.Commands
{
    public record SignInUserCommand(
        string Email,
        string Password
    ) : ICommand<int>;
}